using MinigamesDiscordBot;
using System;
using System.IO;


namespace MinigameDiscordBot.Util
{
    class Logger : IDisposable
    {
        string path = Config.FILEPATH + "\\Log.txt";

        FileStream file;
        StreamWriter sr;

        public Logger()
        {
            file = File.Open(path, FileMode.Append);
            sr = new StreamWriter(file);
        }

        public void Dispose()
        {
            sr.Dispose();
            file.Dispose();
        }

        public void Log(string text)
        {
            sr.WriteLine("[" + DateTime.Now + "] - " + text);
        }


    }
}
