using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using MinigameDiscordBot.Entities;
using MinigameDiscordBot.Timers;
using MinigamesDiscordBot.CommandModules;
using MinigamesDiscordBot.Entities;
using System;
using System.Threading.Tasks;

namespace MinigamesDiscordBot
{
    class Program
    {
        private readonly DiscordSocketClient _client;

        // Keep the CommandService and IServiceCollection around for use with commands.
        private readonly IServiceCollection _map = new ServiceCollection();
        private readonly CommandService _commands = new CommandService();

        // Program entry point
        static void Main(string[] args)
        {
            // Call the Program constructor, followed by the 
            // MainAsync method and wait until it finishes (which should be never).
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private Program()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                // How much logging do you want to see?
                LogLevel = LogSeverity.Info,

                // If you or another service needs to do anything with messages
                // (eg. checking Reactions, checking the content of edited/deleted messages),
                // you must set the MessageCacheSize. You may adjust the number as needed.
                //MessageCacheSize = 50,
            });
        }

        // Example of a logging handler. This can be re-used by addons
        // that ask for a Func<LogMessage, Task>.
        private static Task Logger(LogMessage message)
        {
            var cc = Console.ForegroundColor;
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message}");
            Console.ForegroundColor = cc;

            return Task.CompletedTask;
        }

        private async Task MainAsync()
        {
            Config.GetConfigInfo();

            // Subscribe the logging handler.
            _client.Log += Logger;

            // Centralize the logic for commands into a seperate method.
            await InitCommands();

            // Login and connect.
            await _client.LoginAsync(TokenType.Bot, Config.BOT_TOKEN);
            await _client.StartAsync();

            // Wait infinitely so your bot actually stays connected.
            await Task.Delay(-1);
        }

        private IServiceProvider _services;

        private async Task InitCommands()
        {
            // Repeat this for all the service classes
            // and other dependencies that your commands might need.
            _map.AddSingleton(new CastleWarsGame());
            _map.AddSingleton(new GeobieBands());
            _map.AddSingleton(_client);


            // When all your required services are in the collection, build the container.
            _services = _map.BuildServiceProvider();

            // add Modules manually 
            await _commands.AddModuleAsync<CastleWarsModule>();
            await _commands.AddModuleAsync<GeobieBandsModule>();
            await _commands.AddModuleAsync<HelpModule>();

            //add timers
            TimerContainer timers = new TimerContainer(_services);

            // Subscribe a handler to see if a message invokes a command.
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            // Bail out if it's a System Message.
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;

            // Replace the '!' with whatever character
            // you want to prefix your commands with.
            if (msg.HasCharPrefix('_', ref pos))
            {
                // Create a Command Context.
                var context = new SocketCommandContext(_client, msg);

                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed succesfully).
                var result = await _commands.ExecuteAsync(context, pos, _services);

                // Uncomment the following lines if you want the bot
                // to send a message if it failed (not advised for most situations).
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
