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
            

            //await ReplyAsync(output);
            var eb = new EmbedBuilder();
            var eb2 = new EmbedBuilder();
            var eb3 = new EmbedBuilder();
            var eb4 = new EmbedBuilder();

            //general commands
            EmbedFieldBuilder GeneralHelpCommandField = new EmbedFieldBuilder();
            GeneralHelpCommandField.Name = "General Help";
            GeneralHelpCommandField.Value = "`-Help`\n`-About`\n`-Spotlight`";
            GeneralHelpCommandField.WithIsInline(true);
            EmbedFieldBuilder GeneralHelpDescriptionField = new EmbedFieldBuilder();
            GeneralHelpDescriptionField.Name = "Description";
            GeneralHelpDescriptionField.IsInline = true;
            GeneralHelpDescriptionField.Value = "This command.\nGeneral information about this bot.\nMinigame Spotlight information.";

            //coord commands
            EmbedFieldBuilder CoordHelpCommandField = new EmbedFieldBuilder();
            CoordHelpCommandField.Name = "Coordination Commands";
            CoordHelpCommandField.Value = "`-StartGames`\n`-ChangeCoord <user#1111>`\n`-AddP <s, z> <username>`\n" +
                "`-AddR <username>`\n`-Remove <username>`\n`-NewRound`\n`-StopGames`";
            CoordHelpCommandField.WithIsInline(true);
            EmbedFieldBuilder CoordHelpDescriptionField = new EmbedFieldBuilder();
            CoordHelpDescriptionField.Name = "Description";
            CoordHelpDescriptionField.IsInline = true;
            CoordHelpDescriptionField.Value = "Starts games of Castle Wars.\nChanges the coordinator of the game.\n" +
                "Adds a perm member to either team.\nAdds a rotating member to available team.\nRemoves a user from the games.\n" +
                "Use this at the start of each round to switch teams.\nThis closes the games.";

            //geobie commands
            EmbedFieldBuilder GeobieHelpCommandField = new EmbedFieldBuilder();
            GeobieHelpCommandField.Name = "Goebiebands Commands";
            GeobieHelpCommandField.Value = "`-s <world> <type> <user>`\n`-dead <world>`\n`-removeworld <world>`\n`-clearinfo`\n`-outputtotals`";
            GeobieHelpCommandField.WithIsInline(true);
            EmbedFieldBuilder GeobieHelpDescriptionField = new EmbedFieldBuilder();
            GeobieHelpDescriptionField.Name = "Description";
            GeobieHelpDescriptionField.IsInline = true;
            GeobieHelpDescriptionField.Value = "Registers a scout. Types: a, f, w.\nMark a world dead.\nRemoves a world from the list.\nClears the current Goebiebands info.\nPrints out total number of scouts per user.";

            //warbands commands
            EmbedFieldBuilder WarbandsHelpCommandField = new EmbedFieldBuilder();
            WarbandsHelpCommandField.Name = "Warbands Commands";
            WarbandsHelpCommandField.Value = "`-w <world> <type>`\n`-ClearWarbands`";
            WarbandsHelpCommandField.WithIsInline(true);
            EmbedFieldBuilder WarbandsHelpDescriptionField = new EmbedFieldBuilder();
            WarbandsHelpDescriptionField.Name = "Description";
            WarbandsHelpDescriptionField.IsInline = true;
            WarbandsHelpDescriptionField.Value = "Adds or edits a world.\nClears the current list.";

            eb.AddField(GeneralHelpCommandField);
            eb.AddField(GeneralHelpDescriptionField);
            eb2.AddField(CoordHelpCommandField);
            eb2.AddField(CoordHelpDescriptionField);
            eb3.AddField(GeobieHelpCommandField);
            eb3.AddField(GeobieHelpDescriptionField);
            eb4.AddField(WarbandsHelpCommandField);
            eb4.AddField(WarbandsHelpDescriptionField);

            await ReplyAsync("", false, eb);
            await ReplyAsync("", false, eb2);
            await ReplyAsync("", false, eb3);
            await ReplyAsync("", false, eb4);
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
