using Discord.Commands;
using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MinigameDiscordBot.Timers
{
    class TimerContainer : ModuleBase
    {
        DiscordSocketClient _client;
        GeobieBands _bands;
        GeobieResetTimer geobieTimer;

        public TimerContainer(IServiceProvider services)
        {
            _client = services.GetService<DiscordSocketClient>();
            _bands = services.GetService<GeobieBands>();
            geobieTimer = new GeobieResetTimer(_client, _bands); //start the timer
        }
    }
}
