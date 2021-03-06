﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using MinigamesDiscordBot;
using System.Threading.Tasks;

namespace MinigameDiscordBot.CommandModules
{
    class WarbandsModule : ModuleBase
    {
        Warbands _bands;
        DiscordSocketClient _client;

        EmbedBuilder eb = new EmbedBuilder();

        public WarbandsModule(Warbands bands, DiscordSocketClient client)
        {
            _bands = bands;
            _client = client;

            EmbedFieldBuilder WarbandsHelpCommandField = new EmbedFieldBuilder();
            WarbandsHelpCommandField.Name = "Warbands Commands";
            WarbandsHelpCommandField.Value = "`-w <world> <type>`\n`-ClearWarbands`";
            WarbandsHelpCommandField.WithIsInline(true);
            EmbedFieldBuilder WarbandsHelpDescriptionField = new EmbedFieldBuilder();
            WarbandsHelpDescriptionField.Name = "Description";
            WarbandsHelpDescriptionField.IsInline = true;
            WarbandsHelpDescriptionField.Value = "Adds or edits a world, values: dwf, elm, rdi, broken, dead.\nClears the current list.";

            eb.AddField(WarbandsHelpCommandField);
            eb.AddField(WarbandsHelpDescriptionField);
        }

        [Command("w")]
        public async Task Insert(int world, string type)
        {
            bool roleFound = false;
            string output = "";         

            
                if(type.ToLower() == "dwf" || type.ToLower() == "elm" || type.ToLower() == "rdi")
                {
                    output = _bands.AddWorld(world, type.ToLower());
                }
                else if(type.ToLower() == "dead" || type.ToLower() == "broken")
                {
                    output = _bands.ChageWorldStatus(world, type.ToLower());
                }
                else
                {
                    output = "Invalid input. Please use command: `-w <world number> <location or status>`";
                }
            

            await UpdateChannel();
            await ReplyAsync(output);
        }

        [Command("ClearWarbands")]
        public async Task ClearOutput()
        {
            bool roleFound = false;
            string output = "";

            output = _bands.ClearList();
            

            await UpdateChannel();
            await ReplyAsync(output);
        }



        private async Task UpdateChannel()
        {
            SocketGuild geobieGuild = _client.GetGuild(Config.SERVER_ID_MINIGAMES);//loads server info
            SocketTextChannel minigamesChannel = geobieGuild.GetTextChannel(Config.MINIGAMES_WARBANDS_CHANNEL); //loads channel info

            SocketGuild warbandsGuild = _client.GetGuild(Config.SERVER_ID_WARBANDS);
            SocketTextChannel warbandsChannel = warbandsGuild.GetTextChannel(Config.WARBANDS_WARBANDS_CHANNEL);

            //delete any and all previous messages
            var messages1 = await minigamesChannel.GetMessagesAsync(100).Flatten();
            await minigamesChannel.DeleteMessagesAsync(messages1);

                var messages2 = await warbandsChannel.GetMessagesAsync(100).Flatten();
                await warbandsChannel.DeleteMessagesAsync(messages2);

            //give output
            await minigamesChannel.SendMessageAsync("", false, eb);
            await minigamesChannel.SendMessageAsync(_bands.GetOutput());
            await warbandsChannel.SendMessageAsync("", false, eb);
            await warbandsChannel.SendMessageAsync(_bands.GetOutput());
        }
    }
}
