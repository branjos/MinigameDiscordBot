﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MinigamesDiscordBot.Entities;
using System;
using System.Threading.Tasks;

namespace MinigamesDiscordBot.CommandModules
{
    [Group("cws")]
    class CastleWarsModule : ModuleBase
    {

        CastleWarsGame _game;
        DiscordSocketClient _client;

        //constructor - when the object is created this is called
        public CastleWarsModule(DiscordSocketClient client, CastleWarsGame game)
        {
            //pulls needed info from the serviceCollection in the main program
            _game = game;
            _client = client;
        }


        [Command("Hello")]
        public async Task HelloWorld()
        {
            await ReplyAsync("Hello, " + Context.User.Username.ToString());
        }

        [Command("StartGames")]
        public async Task Start()
        {
            //getting the user on the server to see their roles
            SocketGuild s = _client.GetGuild(Config.SERVER_ID);
            SocketGuildUser u = s.GetUser(Context.User.Id);

            bool roleFound = false; //to output correct message

            //loopsing through roles to see if they have the correct one
            foreach (SocketRole role in u.Roles)
            {
                if(role.Name == "Castle Wars Coordinator" || s.Owner.Id == Context.User.Id)
                {
                    _game.ClearGame(); //clears previous lists 
                    _game.GameController = Context.User.ToString(); //sets the controller to whoever used this command
                    _game.startTime = DateTime.Now;
                    _game.GameGoing = true;
                    roleFound = true;
                    await UpdateTextChannel();
                }
            }
            if (roleFound)
            {
                await ReplyAsync("Game is started.");
            }
            else
            {
                await ReplyAsync("User does not have the proper role to coordinate games.");
            }
            
        }

        [Command("AddP")]
        public async Task AddPerm(string side, [Remainder]string username)
        {
            string output = "You are not the game coordinator or a server owner.";

            if (_game.GameController == Context.User.ToString() || Context.Guild.OwnerId == Context.User.Id)
            {
                output = _game.AddPerm(username.ToLower(), side);
                await UpdateTextChannel();
            }
            
            await ReplyAsync("You are not the game coordinator or a server owner.");         
        }

        [Command("AddR")] //for add rotations
        public async Task AddRotarions([Remainder]string username)
        {
            string output = "You are not the game coordinator or a server owner.";

            if (_game.GameController == Context.User.ToString() || Context.Guild.OwnerId == Context.User.Id)
            {
                output = _game.AddRotating(username);
                await UpdateTextChannel();
            }
            await ReplyAsync(output);
        }

        [Command("Remove")]
        public async Task RemoveUser([Remainder]string username)
        {
            string output = "You are not the game coordinator or a server owner.";

            if (_game.GameController == Context.User.ToString() || Context.Guild.OwnerId == Context.User.Id)
            {
                output = _game.RemoveUser(username);
                await UpdateTextChannel();
            }

            await ReplyAsync(output);
        }

        [Command("NewRound")]
        public async Task NewRound()
        {
            string output = "You are not the game coordinator or a server owner.";

            if (_game.GameController == Context.User.ToString() || Context.Guild.OwnerId == Context.User.Id)
            {
                output = _game.NewRound();
                await UpdateTextChannel();
            }

            await ReplyAsync(output);
        }

        [Command("StopGames")]
        public async Task StopGames()
        {
            if (_game.GameController == Context.User.ToString() || Context.Guild.OwnerId == Context.User.Id)
            {
                _game.ClearGame();
                _game.GameGoing = false;
                await UpdateTextChannel();
            }
            else
            {
                await ReplyAsync("You are not the game coordinator or a server owner.");
            }
        }

        [Command("ReloadOutput")]
        public async Task ReoadOutput()
        {
            if (_game.GameController == Context.User.ToString() || Context.Guild.OwnerId == Context.User.Id)
            {
                await UpdateTextChannel();
            }
            else
            {
                await ReplyAsync("You are not the game coordinator or a server owner.");
            }
        }
        
        private async Task UpdateTextChannel()
        {
            SocketGuild guild = _client.GetGuild(Config.CWS_CHANNEL);//loads server info
            SocketTextChannel channel = guild.GetTextChannel(Config.CWS_CHANNEL); //loads channel info

            //delete any and all previous messages
            var messages = await channel.GetMessagesAsync(100).Flatten();
            await channel.DeleteMessagesAsync(messages);

            //give output
            await channel.SendMessageAsync(_game.OutputTable());
        }
    }
}
