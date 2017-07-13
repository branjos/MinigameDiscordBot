using Discord.Commands;
using System.Threading.Tasks;
using MinigamesDiscordBot;
using MinigameDiscordBot.Util;

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
                Config.UpdateTextFile();
                Logger l = new Logger();
                l.Log(Context.User.ToString() + " updated Warbands Server Id");
                l.Dispose();
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
                Config.UpdateTextFile();
                Logger l = new Logger();
                l.Log(Context.User.ToString() + " updated Minigames Server Id");
                l.Dispose();
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
                Config.UpdateTextFile();
                Logger l = new Logger();
                l.Log(Context.User.ToString() + " updated Minigames Castlewars Channel Id");
                l.Dispose();
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
                Config.UpdateTextFile();
                Logger l = new Logger();
                l.Log(Context.User.ToString() + " updated Minigames Geobie Channel Id");
                l.Dispose();
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
                Config.UpdateTextFile();
                Logger l = new Logger();
                l.Log(Context.User.ToString() + " updated Minigames Warbands Channel Id");
                l.Dispose();
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
                Config.UpdateTextFile();
                Logger l = new Logger();
                l.Log(Context.User.ToString() + " updated Warbands' Warbands Channel Id");
                l.Dispose();
                await ReplyAsync("Id updated.");
            }
            else
            {
                await ReplyAsync("You do not have permission to use this command.");
            }
        }

        [Command("AddAdminId")]
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
                    Config.UpdateTextFile();
                    Logger l = new Logger();
                    l.Log(Context.User.ToString() + " added an admin(" + id + ")");
                    l.Dispose();
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
                    Config.UpdateTextFile();
                    Logger l = new Logger();
                    l.Log(Context.User.ToString() + " removed an admin(" + id + ")");
                    l.Dispose();
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
