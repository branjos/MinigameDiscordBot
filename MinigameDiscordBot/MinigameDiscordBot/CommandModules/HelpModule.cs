using Discord.Commands;
using System.Threading.Tasks;

namespace MinigamesDiscordBot.CommandModules
{
    class HelpModule : ModuleBase
    {
        [Command("help")]
        public async Task Help()
        {
            string output = "**General Commands**\n";
            output += "`_help` This command.\n";
            output += "`_about` Information about this bot.\n";
            output += "`_Spotlight` Not implemented yet.\n\n";

            output += "**Coordination Commands**\n";
            output += "`_StartGames` Starts games of Castle Wars\n";
            output += "`_AddP <sara, zam> <username>` Adds a perm member to either team.\n";
            output += "`_AddR <username>` Adds a rotating member to available team.\n";
            output += "`_Remove <username>` Removes a user from the games.\n";
            output += "`_NewRound` Use this at the start of each round to switch teams.\n";
            output += "`_StopGames` This closes the games.\n\n";

            output += "**Geobiebands Commands**\n";
            output += "`_s <world> <a, f, w> <username>` Registers a scout to the proper world and skills.\n";
            output += "`_Dead <world>` Make a world dead.\n";
            output += "`_RemoveWorld <world>` Removes a world from the list.\n";
            output += "`_ClearInfo` Clears the current geobiebands info.\n";
            output += "`_OutputTotals` Prints out total number of scouts per user.\n";
            output += "`_ClearInfoMonthly` Clears user total scouts for the month.\n";

            await ReplyAsync(output);
        }

        [Command("About")]
        public async Task About()
        {
            string output = "**Minigames FC Discord Bot** \n\n";
            output += "This bot is built to help maintain the Minigames FC and its information.\n\n";

            output += "**Notable Features**\n";
            output += "-Geobiebands tracking and user management\n";
            output += "-Castlewars tracking \n";
            output += "-FC management \n\n";

            output += "**Bot Developer:** \n";
            output += "RSN: Branjos\n\n";

            output += "**Contributors:**\n";
            output += "RSN: Minigames\n";
            output += "RSN: Bepo\n";
            output += "RSN: xMiley\n";

            output += "**Report a bug or request an enhancement**\n";
            output += "Please shout out to me using @bot dev\n\n";

            await ReplyAsync(output);
        }
    }
}
