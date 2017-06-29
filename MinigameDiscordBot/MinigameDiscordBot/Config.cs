using System;
using System.IO;


namespace MinigamesDiscordBot
{
    class Config
    {
        public static string BOT_TOKEN = ""; //bot's token
        public static ulong SERVER_ID = 0; //servers unique id
        public static ulong CWS_CHANNEL = 0; //output channel for cws

        //read the contents of config.txt
        public static void GetConfigInfo()
        {
            string path = "C:\\Minigames\\config.txt";

            var file = File.OpenRead(path); //opens file
            var sr = new StreamReader(file); //opens streamreader to read the file

            string line = "";

            //assigns next line to line variable and if it is not null (empty) keeps going
            while ((line = sr.ReadLine()) != null)
            {
                if (line != null) //checks again to see if line is null
                {
                    string[] csv = line.Split(',');
                    BOT_TOKEN = csv[0];
                    SERVER_ID = Convert.ToUInt64(csv[1]);
                    CWS_CHANNEL = Convert.ToUInt64(csv[2]);
                }
            }
            sr.Dispose(); //IMPORTANT - must dispose the stream
            file.Dispose(); //IMPORTANT - must dispose the file

            Console.WriteLine("Config Loaded.");
        }
    }
}
