using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mikepenz.MaterialDrawer;
using Mikepenz.MaterialDrawer.Models;
using Mikepenz.MaterialDrawer.Models.Interfaces;
using Newtonsoft.Json;
using Quest.Activities;

using Environment = System.Environment;
using Path = System.IO.Path;
using StringBuilder = System.Text.StringBuilder;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Quest
{
    static class Tools
    {
        public static Activity Activity = new Activity();
        public static string ServerAdress = "http://192.168.2.31/Quest";
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            
            return imageBitmap;
        }

        public static void SetDrawer(Activity activity, Toolbar toolbar)
        {
            Activity = activity;

            PrimaryDrawerItem item1 = new PrimaryDrawerItem();
            item1.WithIdentifier(1);
            item1.WithName(Resource.String.Tasks);
            item1.WithSelectable(false);
            item1.WithIcon(Resource.Drawable.ic_assignment_black_24dp);
            
            PrimaryDrawerItem item2 = new PrimaryDrawerItem();
            item2.WithIdentifier(2);
            item2.WithName(Resource.String.Messages);
            item2.WithSelectable(false);
            item2.WithIcon(Resource.Drawable.ic_message_black_24dp);

            PrimaryDrawerItem item3 = new PrimaryDrawerItem();
            item3.WithIdentifier(3);
            item3.WithName(Resource.String.Settings);
            item3.WithSelectable(false);
            item3.WithIcon(Resource.Drawable.ic_settings_black_24dp);

            PrimaryDrawerItem item4 = new PrimaryDrawerItem();
            item4.WithIdentifier(4);
            item4.WithName(Resource.String.LogOut);
            item4.WithSelectable(false);
            item4.WithIcon(Resource.Drawable.ic_exit_to_app_black_24dp);

            ProfileDrawerItem profile = new ProfileDrawerItem();
            profile.WithIdentifier(0);
            profile.WithName(Globals.MyProfile.Name);
            profile.WithNameShown(true);
            profile.WithIcon(Resource.Drawable.test);
            profile.WithEmail(Globals.MyProfile.Team.Name);
            profile.WithTextColor(Color.Black);
            profile.WithSelectable(false);

            var drawer = new DrawerBuilder()
                .WithActivity(activity)
                .WithToolbar(toolbar)
                .AddDrawerItems(profile, item1, item2, new DividerDrawerItem(), item3, item4)
                .WithOnDrawerItemClickListener(new MyDrawerListener())
                .Build();
        }

        public static string HashString(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder output = new StringBuilder();
            foreach (var i in data)
            {
                output.Append(i.ToString("x2").ToLower());
            }
            return output.ToString();
        }

        public static string GetWebResponse(string request)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ServerAdress + "/" + request);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string respstr = reader.ReadToEnd();
            return respstr;
        }

        public static void SignIn(string email, string hashedPwd)
        {
            string respstr = GetWebResponse("LoginHandler.ashx?Type=SignIn"
                + "&Email=" + email + "&PWDHashed=" + hashedPwd);

            if (respstr == "WrongCredentials")
            {
                throw new Exception("WrongCredentials");
            }
            else
            {
                string[] output = respstr.Split('\t');
                Globals.MyProfile = new Profile();
                Globals.MyProfile.Id = int.Parse(output[0]);
                Globals.MyProfile.Name = output[1];
                Globals.MyProfile.HashedPassword = output[2];
                Globals.MyProfile.Email = output[3];
                if (output[4] != "")
                {
                    Globals.MyProfile.Team.Id = int.Parse(output[4]);
                    string[] resp = Tools.GetWebResponse("TeamInfoHandler.ashx?Type=Get&ID=" + output[4]).Split('\t');
                    Globals.MyProfile.Team.Name = resp[1];
                    Globals.MyProfile.Team.Password = resp[2];
                    Globals.MyProfile.Team.Score = int.Parse(resp[3]);
                }
                WriteToFile("Credentials.json", JsonConvert.SerializeObject(Globals.MyProfile));
                
            }
           
        }

        public static void SetTeam(string name, string pwd)
        {
            string response = GetWebResponse($"LoginHandler.ashx?Type=SetTeam&Name={name}&PWD={pwd}&UserID={Globals.MyProfile.Id}");
            if (response == "WrongCredentials") throw new Exception("WrongCredentials");
            string[] output = response.Split('\t').ToArray();
            Globals.MyProfile.Team = new Team();
            Globals.MyProfile.Team.Id = int.Parse(output[0]);
            Globals.MyProfile.Team.Name = output[1];
            Globals.MyProfile.Team.Password = output[2];
            Globals.MyProfile.Team.Score = int.Parse(output[3]);
        }

        public static string[] GetMembers(int teamId)
        {
            return
                GetWebResponse($"TeamInfoHandler.ashx?Type=GetMembers&ID={teamId}")
                    .Split(new char[] {'\t'}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void WriteToFile(string filename, string text)
        {
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), filename), text);
        }

        public static bool FileExists(string filename)
        {
            return File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), filename));
        }

        public static void DeleteFile(string filename)
        {
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), filename));
        }

        public static string ReadFromFile(string filename)
        {
            return
                File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), filename));
        }
    }

    public class MyDrawerListener : Java.Lang.Object, Drawer.IOnDrawerItemClickListener
    {
        public bool OnItemClick(View view, int position, IDrawerItem drawerItem)
        {
            switch (drawerItem.Identifier)
            {
                case 0:
                    Tools.Activity.StartActivity(typeof(ProfileViewActivity));
                    break;
                case 1:
                    Tools.Activity.StartActivity(typeof(TaskListActivity));
                    break;
                case 2:
                    Tools.Activity.StartActivity(typeof(MessagesListActivity));
                    break;
                case 4:
                    AlertDialog.Builder builder = new AlertDialog.Builder(Tools.Activity);
                    builder.SetMessage(Resource.String.SureLogOut);
                    builder.SetNegativeButton(Resource.String.No, delegate { });
                    builder.SetPositiveButton(Resource.String.Yes, delegate
                    {
                        Tools.DeleteFile("Credentials.json");
                        Globals.MyProfile = new Profile();
                        Tools.Activity.StartActivity(typeof(SignInActivity));
                    });
                    builder.Show();
                    break;
                    
            }
            return false;
        }
    }

}