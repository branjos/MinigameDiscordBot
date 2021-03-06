﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinigamesDiscordBot.CommandModules
{
    class GeobieBandsModule : ModuleBase
    {
        GeobieBands _bands;
        DiscordSocketClient _client;
        EmbedBuilder eb1 = new EmbedBuilder();


        public GeobieBandsModule(GeobieBands bands, DiscordSocketClient client)
        {
            _bands = bands;
            _client = client;

            EmbedFieldBuilder GeobieHelpCommandField = new EmbedFieldBuilder();
            GeobieHelpCommandField.Name = "Goebiebands Commands";
            GeobieHelpCommandField.Value = "`-s <world> <type> <user>`\n`-dead <world>`\n`-removeworld <world>`";
            GeobieHelpCommandField.WithIsInline(true);
            EmbedFieldBuilder GeobieHelpDescriptionField = new EmbedFieldBuilder();
            GeobieHelpDescriptionField.Name = "Description";
            GeobieHelpDescriptionField.IsInline = true;
            GeobieHelpDescriptionField.Value = "Registers a scout. Types: a, f, w.\nMark a world dead.\nRemoves a world from the list.";
            EmbedFieldBuilder worlds = new EmbedFieldBuilder();
            worlds.Name = "World Information";
            worlds.Value = "Early spawn worlds: 12, 14, 15, 30, 37, 49, 50, 51, 65, 83, 84\n" +
                "Early spawn worlds spawn at: 02 and die at: 22\n" +
                "All other worlds spawn at: 05 and die at: 25";


            eb1.AddField(GeobieHelpCommandField);
            eb1.AddField(GeobieHelpDescriptionField);
            eb1.AddField(worlds);
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

                switch (skill)
                {
                    case "a":
                        skill = "Agility/Crafting";
                        break;
                    case "f":
                        skill = "Farming/Herblore";
                        break;
                    case "w":
                        skill = "Hunter/Woodutting";
                        break;
                }
                await UpdateTextChannel("[" + Context.User.ToString() + "] - "+ username + " scouted world as " + skill + ": " + world);
            }
            else
            {
                await ReplyAsync("You do not have permission to use this!");
            }
                
        }

        [Command("ClearInfo")]
        public async Task ClearScouts()
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if (role.Name == "Admin" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }
            if (roleFound)
            {
                await ReplyAsync(_bands.ClearInfo());
                await UpdateTextChannel("[" + Context.User.ToString() + "] - " + " Imformation cleared.");
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
                if (role.Name == "Admin" || s.Owner.Id == Context.User.Id)
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
                await UpdateTextChannel("[" + Context.User.ToString() + "] - Marked world as dead: " + world);
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
                await UpdateTextChannel("[" + Context.User.ToString() + "] " +  " removed world " + worldNum);
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

        [Command("PrintEmbed")]
        public async Task PrintEmbed()
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID_MINIGAMES);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if (role.Name == "Admin" || s.Owner.Id == Context.User.Id)
                {
                    roleFound = true;
                }
            }
            if (roleFound)
            {
                await ReplyAsync("", false, eb1);
            }
            else
            {
                await ReplyAsync("You do not have permission to use this!");
            }
        }


        private async Task UpdateTextChannel(string message)
        {
            SocketGuild guild = _client.GetGuild(Config.SERVER_ID_MINIGAMES);//loads server info
            SocketTextChannel channel = guild.GetTextChannel(Config.MINIGAMES_GEOBIE_CHANNEL); //loads channel info
            SocketTextChannel log = guild.GetTextChannel(Config.MINIGAMES_LOG_CHANNEL);

            //delete any and all previous messages
            var messages = await channel.GetMessagesAsync(100).Flatten();
            List<IMessage> list = messages.ToList<IMessage>();
            list.RemoveAt(list.Count - 1);
            await channel.DeleteMessagesAsync(list);

            //give output
            //await channel.SendMessageAsync("", false, eb1);
            await channel.SendMessageAsync(_bands.GetOutput());
            await log.SendMessageAsync(message);
        }
    }
}
