using System;
using System.Collections.Generic;
using System.IO;


namespace MinigamesDiscordBot
{
    class Config
    {
        public static string VERSION = "1.0.0";
        public static string BOT_TOKEN = ""; //bot's token
        public static ulong SERVER_ID_MINIGAMES = 0; //servers unique id
        public static ulong SERVER_ID_WARBANDS = 0;
        public static ulong MINIGAMES_CWS_CHANNEL = 0; //output channel for cws
        public static ulong MINIGAMES_GEOBIE_CHANNEL = 0; //output for geobiebands
        public static ulong MINIGAMES_WARBANDS_CHANNEL = 0;
        public static ulong WARBANDS_WARBANDS_CHANNEL = 0;
        public static int FC_PROMOTION_LIMIT = 0;
        public static List<ulong> ADMIN_ID = new List<ulong>();

        public static string FILEPATH = "C:\\Minigames\\";

        public static ulong MINIGAMES_SERVER_ID { get; internal set; }

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
                    SERVER_ID_MINIGAMES = Convert.ToUInt64(csv[1]);
                    SERVER_ID_WARBANDS = Convert.ToUInt64(csv[2]);
                    MINIGAMES_CWS_CHANNEL = Convert.ToUInt64(csv[3]);
                    MINIGAMES_GEOBIE_CHANNEL = Convert.ToUInt64(csv[4]);
                    MINIGAMES_WARBANDS_CHANNEL = Convert.ToUInt64(csv[5]);
                    WARBANDS_WARBANDS_CHANNEL = Convert.ToUInt64(csv[6]);
                    FC_PROMOTION_LIMIT = Convert.ToInt32(csv[7]);
                    string[] adminids = csv[8].Split(';');
                    for(int i = 0; i < adminids.Length; ++i)
                    {
                        ADMIN_ID.Add(Convert.ToUInt64(adminids[i]));
                    }
                }
            }
            sr.Dispose(); //IMPORTANT - must dispose the stream
            file.Dispose(); //IMPORTANT - must dispose the file

            Console.WriteLine("Config Loaded.");
        }

        public static void UpdateTextFile()
        {
            string path = "C:\\Minigames\\config.txt";

            var file = File.Create(path);
            var sw = new StreamWriter(file);

            string idoutput = "";
            foreach(ulong id in ADMIN_ID)
            {
                if(idoutput == "")
                {
                    idoutput += id;
                }
                else
                {
                    idoutput += ";" + id;
                }
            }

            sw.WriteLine(BOT_TOKEN + "," + SERVER_ID_MINIGAMES + "," + SERVER_ID_WARBANDS + "," +
                MINIGAMES_CWS_CHANNEL + "," + MINIGAMES_GEOBIE_CHANNEL + "," + MINIGAMES_WARBANDS_CHANNEL + "," +
                WARBANDS_WARBANDS_CHANNEL + "," + FC_PROMOTION_LIMIT + "," + idoutput);

            sw.Dispose();
            file.Dispose();
        }
    }
}
