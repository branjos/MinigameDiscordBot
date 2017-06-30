using System;

namespace MinigamesDiscordBot.Entities
{
    class CwsUser : IEquatable<CwsUser>
    {
        public string Username { get; set; }
        public int ConsecutiveWins { get; set; }

        public CwsUser(string username)
        {
            Username = username;
        }

        //implementing IEquateable<T> interface
        public bool Equals(CwsUser other)
        {
            if (other == null) return false;

            if (Username == other.Username)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
