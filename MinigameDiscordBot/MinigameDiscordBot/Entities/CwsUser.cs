

namespace MinigamesDiscordBot.Entities
{
    class CwsUser
    {
        public string Username { get; set; }
        public int ConsecutiveWins { get; set; }

        public CwsUser(string username)
        {
            Username = username;
        }
    }
}
