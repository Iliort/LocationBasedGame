using System.Collections.Generic;
using System.Runtime.InteropServices;
using Android.Drm;
using Java.Util.Jar;

namespace Quest
{
    public class Team
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }

        public Team()
        {
            Name = "";
            Id = -1;
            Password = "1111"; 
            Score = 0;
        }

        public Team(string name)
        {
            Name = name;
            Id = -1;
            Password = "1111";
            Score = 0;
        }
    }
}