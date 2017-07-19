using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MinigameDiscordBot.Timers
{
    class DailyReset
    {
        DiscordSocketClient _client;
        FriendsChat _fc;
        TimeSpan timespan;
        bool start = false;
        Timer t;

        public DailyReset(DiscordSocketClient client, FriendsChat fc)
        {
            _client = client;
            _fc = fc;
            StartTimer();
        }

        public void StartTimer()
        {
            t = new Timer((e) =>
            {

                if (start)
                {
                    DoUpdate();
                    timespan.Add(TimeSpan.FromHours(24));
                }
                else
                {
                    timespan = GetTimeToNextReset();
                }

                start = true;

            }, null, TimeSpan.Zero, timespan);
        }

        private TimeSpan GetTimeToNextReset()
        {
            var britTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

            DateTime nowInUTC = TimeZoneInfo.ConvertTime(DateTime.Now, britTimeZone);


            DateTime reset = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(1);
            Console.WriteLine("Time until reset: " + (reset - nowInUTC).ToString());

            return reset - nowInUTC;
        }

        //updates data
        private void DoUpdate()
        {
            _fc.DailyReset();
        }

    }
}
