using Discord.Commands;
using System.Threading.Tasks;
using MinigamesDiscordBot;

namespace MinigameDiscordBot.CommandModules
{
    [Group("settings")]
    class SettingsModule : ModuleBase
    {
        [Command("warbandsserver")]
        public async Task UpdateWarbandsServer(ulong id)
        {
            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                Config.SERVER_ID_WARBANDS = id;
                //logger
                await ReplyAsync("Id updated.");
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }

        [Command("MinigamesServer")]
        public async Task UpdateMinigamesServer(ulong id)
        {
            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                Config.SERVER_ID_MINIGAMES = id;
                //logger
                await ReplyAsync("Id updated.");
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }

        [Command("MCastlewarsChannel")]
        public async Task UpdateCwsChannel(ulong id)
        {
            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                Config.MINIGAMES_CWS_CHANNEL = id;
                //logger
                await ReplyAsync("Id updated.");
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }

        [Command("MGeobieChannel")]
        public async Task UpdateGeobieChannel(ulong id)
        {
            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                Config.MINIGAMES_GEOBIE_CHANNEL = id;
                //logger
                await ReplyAsync("Id updated.");
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }

        [Command("MWarbandsChannel")]
        public async Task UpdateMWarbandsChannel(ulong id)
        {
            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                Config.MINIGAMES_WARBANDS_CHANNEL = id;
                //logger
                await ReplyAsync("Id updated.");
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }

        [Command("WWarbandsChannel")]
        public async Task UpdateWWarbandsChannel(ulong id)
        {
            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                Config.WARBANDS_WARBANDS_CHANNEL = id;
                //logger
                await ReplyAsync("Id updated.");
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }

        [Command("AdAdminId")]
        public async Task AddAdmin(ulong id)
        {
            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                if (Config.ADMIN_ID.Contains(id))
                {
                    await ReplyAsync("That user already is an admin.");
                }
                else
                {
                    Config.ADMIN_ID.Add(id);
                    //logger
                    await ReplyAsync("Id added.");
                }
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }

        [Command("RemoveAdminId")]
        public async Task RemoveAdminId(ulong id)
        {
            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                if (!Config.ADMIN_ID.Contains(id))
                {
                    await ReplyAsync("That user was not an admin.");
                }
                else
                {
                    Config.ADMIN_ID.Remove(id);
                    //logger
                    await ReplyAsync("Id removed.");
                }
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }
    }
}
