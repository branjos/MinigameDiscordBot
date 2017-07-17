using MinigamesDiscordBot;
using System;
using System.IO;

namespace MinigameDiscordBot.Util
{
    class GeobieOutputFileWriter
    {
        string _info = "";
        int _iteration = 0;

        public GeobieOutputFileWriter(string info)
        {
            _info = info;

            WriteInfoToFile();
        }

        //writes the data to the file
        private void WriteInfoToFile()
        {
            string path = Config.FILEPATH + "Geobie\\Outputs\\" + DateTime.Now.Day + " - 1.txt";

            if (File.Exists(path))
            {
                path = Config.FILEPATH + "Geobie\\Outputs\\" + DateTime.Now.Day + " - 2.txt";
            }


            var file = File.Create(path);
            var sw = new StreamWriter(file);

            sw.WriteLine(DateTime.Now);

            string[] csv = _info.Split(';');

            for (int i = 0; i < csv.Length; ++i)
            {
                sw.WriteLine(csv[i]);
            }

            sw.Dispose();
            file.Dispose();
        }
    }
}
