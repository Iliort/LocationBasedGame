using Android.Content.Res;
using Android.Graphics;
using Quest.Activities;

namespace Quest
{
    public class Profile
    {
        private Team _team;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public Team Team
        {
            get { return _team; }
            set
            {
                _team = value;
            }
        }

        public Profile()
        {
            Name = "";
            Email = "";
            HashedPassword = "";
            Team = new Team();
            Id = -1;
        }

        public Profile(string name, string email, string hashedPassword)
        {
            Name = name;
            Email = email;
            HashedPassword = hashedPassword;
            Team = new Team();
            Id = -1;
        }

    }

}