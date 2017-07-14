using Discord;
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
            output += "`-help` This command.\n";
            output += "`-about` Information about this bot.\n";
            output += "`-Spotlight` Not implemented yet.\n\n";

            output += "**Coordination Commands**\n";
            output += "`-StartGames` Starts games of Castle Wars\n";
            output += "`-ChangeCoord <user#1111>` Changes the coordinator of the game.\n";
            output += "`-AddP <s, z> <username>` Adds a perm member to either team.\n";
            output += "`-AddR <username>` Adds a rotating member to available team.\n";
            output += "`-Remove <username>` Removes a user from the games.\n";
            output += "`-NewRound` Use this at the start of each round to switch teams.\n";
            output += "`-StopGames` This closes the games.\n\n";

            output += "**Geobiebands Commands**\n";
            output += "`-s <world> <a, f, w> <username>` Registers a scout to the proper world and skills.\n";
            output += "`-Dead <world>` Make a world dead.\n";
            output += "`-RemoveWorld <world>` Removes a world from the list.\n";
            output += "`-ClearInfo` Clears the current geobiebands info.\n";
            output += "`-OutputTotals` Prints out total number of scouts per user.\n";
            output += "`-ClearInfoMonthly` Clears user total scouts for the month.\n";

            //await ReplyAsync(output);
            var eb = new EmbedBuilder();
            eb.WithDescription(output);
            await ReplyAsync("", false, eb);
        }

        [Command("help")]
        public async Task HelpA(string a)
        {

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
