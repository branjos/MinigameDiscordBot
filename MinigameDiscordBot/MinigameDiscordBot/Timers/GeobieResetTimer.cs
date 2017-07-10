using Discord;
using Discord.WebSocket;
using MinigameDiscordBot.Entities;
using MinigamesDiscordBot;
using System;
using System.IO;
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
            Console.WriteLine("Timer started.");
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
            WriteInfoToFile();
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
            DateTime first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 30, 0);
            DateTime second = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 30, 0);

            if (DateTime.Now < first)
            {
                Console.WriteLine("Now is < first by: " + (first - DateTime.Now).ToString());
                return first - DateTime.Now;
            }
            else
            {
                Console.WriteLine("Now is > first by: " + (second - DateTime.Now).ToString());
                return second - DateTime.Now;
            }
        }

        //writes the data to the file
        private void WriteInfoToFile()
        {
            DateTime now = DateTime.Now;
            string path = Config.FILEPATH + "Outputs\\" + now.Month + now.Day + "." + dailyIteration;

            var file = File.Create(path);
            var sw = new StreamWriter(file);

            string output = _bands.OutputForTextFile();

            string[] csv = output.Split(';');

            for(int i = 0; i < csv.Length; ++i)
            {
                sw.WriteLine(csv[i]);
            }
        }

        //updates the text channel
        private async Task UpdateTextChannel()
        {
            SocketGuild guild = _client.GetGuild(Config.SERVER_ID);//loads server info
            SocketTextChannel channel = guild.GetTextChannel(Config.GEOBIE_CHANNEL); //loads channel info

            //delete any and all previous messages
            var messages = await channel.GetMessagesAsync(100).Flatten();
            await channel.DeleteMessagesAsync(messages);

            //give output
            await channel.SendMessageAsync(_bands.GetOutput());
        }
    }
}
