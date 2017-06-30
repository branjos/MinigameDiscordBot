
using System;

namespace MinigameDiscordBot.Entities
{
    class GeobieUser : System.IEquatable<GeobieUser>
    {
        public string Username { get; set; }
        public int NumberOfScouts { get; set; }

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
