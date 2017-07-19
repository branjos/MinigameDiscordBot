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
        FriendsChat _fc;
        DailyReset dailyTimer; 

        public TimerContainer(IServiceProvider services)
        {
            _client = services.GetService<DiscordSocketClient>();
            _bands = services.GetService<GeobieBands>();
            _fc = services.GetService<FriendsChat>();
            geobieTimer = new GeobieResetTimer(_client, _bands); //start the timer
            dailyTimer = new DailyReset(_client, _fc);
        }
    }
}
