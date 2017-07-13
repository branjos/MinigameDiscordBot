using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinigamesDiscordBot.CommandModules
{
    class GeobieBandsModule : ModuleBase
    {
        GeobieBands _bands;
        DiscordSocketClient _client;
        string help = "**Geobiebands Commands**\n";

        public GeobieBandsModule(GeobieBands bands, DiscordSocketClient client)
        {
            _bands = bands;
            _client = client;

            help += "`_s <world> <a, f, w> <username>` Registers a scout to the proper world and skills.\n";
            help += "`_Dead <world>` Make a world dead.\n";
            help += "`_RemoveWorld <world>` Removes a world from the list.\n";
        }

        [Command("s")]
        public async Task Scout(int world, string skill, [Remainder]string username)
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if(role.Name == "Geobies" || role.Name == "Goobies" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }
            if (roleFound)
            {
                await ReplyAsync(_bands.AddWorld(username, world, skill));
                await UpdateTextChannel();
            }
            else
            {
                await ReplyAsync("You do not have permission to use this!");
            }
                
        }

        [Command("ClearInfo")]
        public async Task PrintScouts()
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if (role.Name == "Geobies" || role.Name == "Goobies" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }
            if (roleFound)
            {
                await ReplyAsync(_bands.ClearInfo());
                await UpdateTextChannel();
            }
            else
            {
                await ReplyAsync("You do not have permission to use this!");
            }           
        }

        [Command("ClearInfoMonthly")]
        public async Task ClearInfoMonthly()
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if (role.Name == "Geobies" || role.Name == "Goobies" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }
            if (roleFound)
            {
                await ReplyAsync(_bands.ClearInfoMonthly());
            }
            else
            {
                await ReplyAsync("You do not have permission to use this!");
            }
        }

        [Command("Refresh")]
        public async Task RefreshInfo()
        {
            await UpdateTextChannel();
        }

        [Command("Dead")]
        public async Task MarkWorldAsDead(int world)
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if (role.Name == "Geobies" || role.Name == "Goobies" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }
            if (roleFound)
            {
                await ReplyAsync(_bands.MarkAsDead(world));
                await UpdateTextChannel();
            }
            else
            {
                await ReplyAsync("You do not have permission to use this!");
            }
        }

        [Command("RemoveWorld")]
        public async Task RemoveWorld(int worldNum)
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if (role.Name == "Geobies" || role.Name == "Goobies" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }
            if (roleFound)
            {
                await ReplyAsync(_bands.RemoveWorld(worldNum));
                await UpdateTextChannel();
            }
            else
            {
                await ReplyAsync("You do not have permission to use this!");
            }
        }

        [Command("OutputTotals")]
        public async Task OutputMonthlyTotals()
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if (role.Name == "Geobies" || role.Name == "Goobies" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }
            if (roleFound)
            {
                List<string> output = _bands.OutputTotalScouts();
                for (int i = 0; i < output.Count; ++i)
                {
                    await ReplyAsync(output[i]);
                }
            }
            else
            {
                await ReplyAsync("You do not have permission to use this!");
            }
        }


        private async Task UpdateTextChannel()
        {
            SocketGuild guild = _client.GetGuild(Config.SERVER_ID_MINIGAMES);//loads server info
            SocketTextChannel channel = guild.GetTextChannel(Config.MINIGAMES_GEOBIE_CHANNEL); //loads channel info

            //delete any and all previous messages
            var messages = await channel.GetMessagesAsync(100).Flatten();
            await channel.DeleteMessagesAsync(messages);

            //give output
            await channel.SendMessageAsync(help);
            await channel.SendMessageAsync(_bands.GetOutput());
        }
    }
}
