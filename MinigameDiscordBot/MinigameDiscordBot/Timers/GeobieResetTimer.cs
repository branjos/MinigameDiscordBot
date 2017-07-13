using Discord;
using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using MinigameDiscordBot.Util;
using MinigamesDiscordBot;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MinigameDiscordBot.Timers
{
    class GeobieResetTimer
    {

        GeobieBands _bands;
        DiscordSocketClient _client;
        Timer t;
        TimeSpan timespan;
        bool start = false;
        int dailyIteration = 1; //which time it is of geobie bands

        public GeobieResetTimer(DiscordSocketClient client, GeobieBands bands)
        {
            _bands = bands;
            _client = client;
            //Console.WriteLine("Timer started.");
            StartTimer();
        }

        public void StartTimer()
        {
            t = new Timer((e) =>
            {

                if (start)
                {
                    DoUpdateAsync();
                    timespan.Add(TimeSpan.FromHours(12));
                }
                else
                {
                    timespan = GetTimeToNextGeobie();
                }

                start = true;

            }, null, TimeSpan.Zero, timespan);
        }

        //updates all info
        private async void DoUpdateAsync()
        {
            GeobieOutputFileWriter file = new GeobieOutputFileWriter(_bands.OutputForTextFile(), dailyIteration);
            _bands.ClearInfo();
            await UpdateTextChannel();
            if (dailyIteration >= 2)
            {
                dailyIteration = 1;
            }
            else
            {
                dailyIteration++;
            }
        }

        //gets the time until next geobiebands
        private TimeSpan GetTimeToNextGeobie()
        {
            var britTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

            DateTime nowInUTC = TimeZoneInfo.ConvertTime(DateTime.Now, britTimeZone);
            DateTime first;

            if(nowInUTC > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 23, 30, 0))
            {
                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 30, 0);
            }
            else
            {
                first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 23, 30, 0);
            }

            DateTime second = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 30, 0);

            if (nowInUTC < first)
            {
                //Console.WriteLine("Now is < first by: " + (first - nowInUTC).ToString());
                return first - nowInUTC;
            }
            else if (nowInUTC < second && nowInUTC > first)
            {
                //Console.WriteLine("Now is > first by: " + (second - nowInUTC).ToString());
                return second - nowInUTC;
            }
            else
            {
                return first - nowInUTC;
            }
        }       

        //updates the text channel
        private async Task UpdateTextChannel()
        {
            SocketGuild guild = _client.GetGuild(Config.MINIGAMES_SERVER_ID);//loads server info
            SocketTextChannel channel = guild.GetTextChannel(Config.MINIGAMES_GEOBIE_CHANNEL); //loads channel info

            //delete any and all previous messages
            var messages = await channel.GetMessagesAsync(100).Flatten();
            await channel.DeleteMessagesAsync(messages);

            //give output
            await channel.SendMessageAsync(_bands.GetOutput());
        }
    }
}
