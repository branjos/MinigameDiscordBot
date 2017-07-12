using MinigamesDiscordBot;
using System;
using System.IO;

namespace MinigameDiscordBot.Util
{
    class GeobieOutputFileWriter
    {
        string _info = "";
        int _iteration = 0;

        public GeobieOutputFileWriter(string info, int i)
        {
            _info = info;
            _iteration = i;

            WriteInfoToFile();
        }

        //writes the data to the file
        private void WriteInfoToFile()
        {
            DateTime now = DateTime.Now;
            string path = Config.FILEPATH + "Outputs\\" + now.Month + now.Day + "." + _iteration;

            var file = File.Create(path);
            var sw = new StreamWriter(file);

            string[] csv = _info.Split(';');

            for (int i = 0; i < csv.Length; ++i)
            {
                sw.WriteLine(csv[i]);
            }
        }
    }
}
