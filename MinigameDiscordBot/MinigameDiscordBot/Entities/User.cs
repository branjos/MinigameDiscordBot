
namespace MinigamesDiscordBot.Entities
{
    class User
    {
        public string Username { get; set; }
        public bool SeenToday { get; set; }
        public int TimesSeen { get; set; }

        public User()
        {
            SeenToday = false;
            TimesSeen = 0;
        }

        public User(string username, bool seen, int times)
        {
            Username = username;
            SeenToday = seen;
            TimesSeen = times;
        }

    }
}
