using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using System.Threading.Tasks;

namespace MinigamesDiscordBot.CommandModules
{
    class GeobieBandsModule : ModuleBase
    {
        GeobieBands _bands;
        DiscordSocketClient _client;

        public GeobieBandsModule(GeobieBands bands, DiscordSocketClient client)
        {
            _bands = bands;
            _client = client;
        }

        [Command("s")]
        public async Task Scout(int world, string skill, [Remainder]string username)
        {
            await ReplyAsync(_bands.AddWorld(username, world, skill));
            await UpdateTextChannel();
        }

        [Command("ClearInfo")]
        public async Task PrintScouts()
        {
            await ReplyAsync(_bands.ClearInfo());
            await UpdateTextChannel();
        }

        [Command("Refresh")]
        public async Task RefreshInfo()
        {
            await UpdateTextChannel();
        }

        [Command("Dead")]
        public async Task MarkWorldAsDead(int world)
        {
            await ReplyAsync(_bands.MarkAsDead(world));
            await UpdateTextChannel();
        }


        private async Task UpdateTextChannel()
        {
            SocketGuild guild = _client.GetGuild(Config.SERVER_ID);//loads server info
            SocketTextChannel channel = guild.GetTextChannel(Config.GEOBIE_CHANNEL); //loads channel info

            //delete any and all previous messages
            var messages = await channel.GetMessagesAsync(100).Flatten();
            await channel.DeleteMessagesAsync(messages);

            //give output
            await channel.SendMessageAsync(_bands.GetOutput());
        }
    }
}
