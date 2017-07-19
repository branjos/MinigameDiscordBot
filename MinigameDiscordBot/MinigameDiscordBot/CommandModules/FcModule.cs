using Discord.Commands;
using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using MinigamesDiscordBot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MinigameDiscordBot.CommandModules
{
    class FcModule : ModuleBase
    {
        DiscordSocketClient _client;
        FriendsChat _fc;

        public FcModule(DiscordSocketClient client, FriendsChat fc)
        {
            _client = client;
            _fc = fc;
        }

        [Command("see")]
        public async Task See([Remainder] string username)
        {
            if (IsAdmin(Context.User.Id))
            {
                await ReplyAsync(_fc.See(username));
            }
            else
            {
                await ReplyAsync("You do not have permission to do this.");
            }
        }

        [Command("PrintList")]
        public async Task PrintList()
        {
            if (IsAdmin(Context.User.Id))
            {
                await ReplyAsync(_fc.Print());
            }
            else
            {
                await ReplyAsync("You do not have permission to do this.");
            }
        }

        [Command("Promotions")]
        public async Task Promotion()
        {
            if (IsAdmin(Context.User.Id))
            {
                await ReplyAsync(_fc.PrintPromotions());
            }
            else
            {
                await ReplyAsync("You do not have permission to do this.");
            }
        }

        [Command("ClearFC")]
        public async Task ClearFC()
        {
            if (IsAdmin(Context.User.Id))
            {
                await ReplyAsync(_fc.ClearList());
            }
            else
            {
                await ReplyAsync("You do not have permission to do this.");
            }
        }

        private bool IsAdmin(ulong id)
        {
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if (role.Name == "Admin" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }

            return roleFound;
        }
    }
}
