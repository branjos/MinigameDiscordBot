using Discord.Commands;
using System.Threading.Tasks;

namespace MinigamesDiscordBot.CommandModules
{
    class HelpModule : ModuleBase
    {
        [Command("help")]
        public async Task Help()
        {
            string output = "";
            output += "`_StartGames` Starts games of Castle Wars\n";
            output += "`_AddP <sara, zam> <username>` Adds a perm member to either team.\n";
            output += "`_AddR <username>` Adds a rotating member to available team.\n";
            output += "`_Remove <username>` Removes a user from the games.\n";
            output += "`_NewRound` Use this at the start of each round to switch teams.\n";
            output += "`_StopGames` This closes the games.";

            await ReplyAsync(output);
        }
    }
}
