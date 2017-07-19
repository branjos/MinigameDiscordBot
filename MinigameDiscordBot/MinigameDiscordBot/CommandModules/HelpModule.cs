using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace MinigamesDiscordBot.CommandModules
{
    class HelpModule : ModuleBase
    {
        DiscordSocketClient _client;

        public HelpModule(DiscordSocketClient client)
        {
            _client = client;
        }

        [Command("help")]
        public async Task Help()
        {
            bool isAdmin = false;

            SocketGuildUser user = _client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id);
            foreach(SocketRole role in user.Roles)
            {
                if(role.Name == "Admin")
                {
                    isAdmin = true;
                }
            }

            //await ReplyAsync(output);
            var eb = new EmbedBuilder();
            var eb2 = new EmbedBuilder();
            var eb3 = new EmbedBuilder();
            var eb4 = new EmbedBuilder();
            var adminEmbed = new EmbedBuilder();
            var settingsEmbed = new EmbedBuilder();

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
            GeobieHelpCommandField.Value = "`-s <world> <type> <user>`\n`-dead <world>`\n`-removeworld <world>`";
            GeobieHelpCommandField.WithIsInline(true);
            EmbedFieldBuilder GeobieHelpDescriptionField = new EmbedFieldBuilder();
            GeobieHelpDescriptionField.Name = "Description";
            GeobieHelpDescriptionField.IsInline = true;
            GeobieHelpDescriptionField.Value = "Registers a scout. Types: a, f, w.\nMark a world dead.\nRemoves a world from the list.";

            //warbands commands
            EmbedFieldBuilder WarbandsHelpCommandField = new EmbedFieldBuilder();
            WarbandsHelpCommandField.Name = "Warbands Commands";
            WarbandsHelpCommandField.Value = "`-w <world> <type>`\n`-ClearWarbands`";
            WarbandsHelpCommandField.WithIsInline(true);
            EmbedFieldBuilder WarbandsHelpDescriptionField = new EmbedFieldBuilder();
            WarbandsHelpDescriptionField.Name = "Description";
            WarbandsHelpDescriptionField.IsInline = true;
            WarbandsHelpDescriptionField.Value = "Adds or edits a world.\nClears the current list.";

            //admin embed
            EmbedFieldBuilder GeobieAdminCommandField = new EmbedFieldBuilder();
            GeobieAdminCommandField.Name = "Goebiebands Commands";
            GeobieAdminCommandField.Value = "`-clearinfo`\n`-outputtotals`";
            GeobieAdminCommandField.WithIsInline(true);
            EmbedFieldBuilder GeobieAdminDescriptionField = new EmbedFieldBuilder();
            GeobieAdminDescriptionField.Name = "Description";
            GeobieAdminDescriptionField.IsInline = true;
            GeobieAdminDescriptionField.Value = "Clears the current Goebiebands info.\nPrints out total number of scouts per user.";

            EmbedFieldBuilder FCAdminCommands = new EmbedFieldBuilder();
            FCAdminCommands.Name = "FC Commands";
            FCAdminCommands.Value = "`-See <username>`\n`-printlist`\n`-promotions`\n`-clearfc`";
            FCAdminCommands.WithIsInline(true);
            EmbedFieldBuilder FCAdminDescription = new EmbedFieldBuilder();
            FCAdminDescription.Name = "Description";
            FCAdminDescription.IsInline = true;
            FCAdminDescription.Value = "`Marks the user as seen`\n`Prints the list`\n`Prints promotions`\n`Clears all info`";

            //settings commands
            EmbedFieldBuilder SettingsCommandField = new EmbedFieldBuilder();
            SettingsCommandField.Name = "Settings Commands";
            SettingsCommandField.Value = "`-settings warbandsserver <ser id>`\n`-settings minigamesserver <ser id>`\n" +
                "`-settings mcastlewarschannel <ch id>`\n`-settings mgeobiechannel <ch id>`\n`-settings " +
                "mwarbandschannel <ch id>`\n`-settings wwarbandschannel <ch id>`\n`-settings addadminid <user id>`\n" +
                "`-settings removeadminid <user id>`\n`-settings SetFcPromotionLimit <num>`";
            SettingsCommandField.WithIsInline(true);
            EmbedFieldBuilder SettingsDescriptionField = new EmbedFieldBuilder();
            SettingsDescriptionField.Name = "Description";
            SettingsDescriptionField.IsInline = true;
            SettingsDescriptionField.Value = "`Sets the warbands server`\n`Sets the minigames server`\n`Sets the minigames cws channel`\n" +
                "`Sets the minigames geobie channel`\n`Sets the minigames wbs channel`\n`Sets the warbands wbs channel`\n" +
                "`Adds a bot admin`\n`Removes a bot admin`\n`Sets number of sees needed for promotion`";

            //add embed fields to the builders
            eb.AddField(GeneralHelpCommandField);
            eb.AddField(GeneralHelpDescriptionField);
            eb2.AddField(CoordHelpCommandField);
            eb2.AddField(CoordHelpDescriptionField);
            eb3.AddField(GeobieHelpCommandField);
            eb3.AddField(GeobieHelpDescriptionField);
            eb4.AddField(WarbandsHelpCommandField);
            eb4.AddField(WarbandsHelpDescriptionField);
            adminEmbed.AddField(GeobieAdminCommandField);
            adminEmbed.AddField(GeobieAdminDescriptionField);
            adminEmbed.AddField(FCAdminCommands);
            adminEmbed.AddField(FCAdminDescription);
            settingsEmbed.AddField(SettingsCommandField);
            settingsEmbed.AddField(SettingsDescriptionField);

            await ReplyAsync("", false, eb);
            await ReplyAsync("", false, eb2);
            await ReplyAsync("", false, eb3);
            await ReplyAsync("", false, eb4);

            if (isAdmin)
            {
                IDMChannel userChannel = await Context.User.CreateDMChannelAsync();
                await userChannel.SendMessageAsync("", false, adminEmbed);
            }

            if (Config.ADMIN_ID.Contains(Context.User.Id))
            {
                IDMChannel userChannel = await Context.User.CreateDMChannelAsync();
                await userChannel.SendMessageAsync("", false, settingsEmbed);
            }
        }

        [Command("About")]
        public async Task About()
        {
            EmbedBuilder eb = new EmbedBuilder();

            EmbedFieldBuilder description = new EmbedFieldBuilder();
            description.Name = "Minigames FC Discord Bot";
            description.Value = "This bot is built to help maintain the Minigames FC and its information.";

            EmbedFieldBuilder features = new EmbedFieldBuilder();
            features.Name = "Notable Features";
            features.Value = "-Geobiebands tracking\n-Castlewars tracking\n-Warbands scouting\n-FC management";

            EmbedFieldBuilder dev = new EmbedFieldBuilder();
            dev.Name = "Bot Developer";
            dev.Value = "RSN: Branjos";

            EmbedFieldBuilder cont = new EmbedFieldBuilder();
            cont.Name = "Contributors";
            cont.Value = "RSN: Minigames\nRSN: Bepo\nRSN: xMiley";

            EmbedFooterBuilder foot = new EmbedFooterBuilder();
            foot.Text = "Version: 1.0.0 | Made using .NET Core and Discord.Net 1.0 rc-2";

            eb.AddField(description);
            eb.AddField(features);
            eb.AddField(dev);
            eb.AddField(cont);
            eb.Footer = foot;

            await ReplyAsync("", false, eb);
        }
    }
}
