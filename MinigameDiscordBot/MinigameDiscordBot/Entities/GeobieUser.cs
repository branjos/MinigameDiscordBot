
using System;

namespace MinigameDiscordBot.Entities
{
    class GeobieUser : IEquatable<GeobieUser>
    {
        public string Username { get; set; }
        public int NumberOfScouts { get; set; }
        public bool CurrentScouter { get; set; }
        public int NumCurrentScouts { get; set; }

        public GeobieUser(string username)
        {
            Username = username;
            NumberOfScouts = 0;
        }

        public bool Equals(GeobieUser other)
        {
            if (other == null) { return false; }
            if (other.Username == Username) { return true; }
            else { return false; }
        }


    }
}
