
namespace MinigamesDiscordBot.Entities
{
    class User
    {
        public string Username { get; set; }
        public enum FcRank { Friend = 0, Recruit = 1, Corporal = 2, Sergeant = 3, Lieutenant = 4, Captain = 5, General = 6,
            Channel_Owner = 7}
        public bool SeenToday { get; set; }
        public int TimesSeen { get; set; }

    }
}
