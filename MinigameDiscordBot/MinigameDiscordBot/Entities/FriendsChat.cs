using MinigameDiscordBot.Util;
using MinigamesDiscordBot;
using MinigamesDiscordBot.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinigameDiscordBot.Entities
{
    class FriendsChat
    {
        List<User> users = new List<User>();

        public FriendsChat()
        {
            ReadFromFile();
        }

        public string See(string username)
        {
            string output = "";
            bool found = false;

            for (int i = 0; i < users.Count; ++i)
            {
                if(users[i].Username.ToLower() == username.ToLower())
                {
                    if (!users[i].SeenToday)
                    {
                        users[i].TimesSeen++;
                        users[i].SeenToday = true;
                        found = true;
                        output = "User marked as seen";
                        WriteToFile();
                    }
                    else
                    {
                        found = true;
                        output = "User had already been seen.";
                    }
                }
            }

            if (!found)
            {
                User u = new User();
                u.Username = username;
                u.TimesSeen++;
                u.SeenToday = true;
                users.Add(u);
                output = "User created.";
                WriteToFile();
            }

            return output;
        }

        public string Print()
        {
            OutputFormatting f = new OutputFormatting("Username", 15, "Times seen", 10);

            for(int i = 0; i < users.Count; ++i)
            {
                f.addLine(users[i].Username, users[i].TimesSeen.ToString());
            }
            f.insertEnd();

            return f.output;
        }

        public string PrintPromotions()
        {
            OutputFormatting f = new OutputFormatting("Promotions", 15, "", 0);

            for(int i = 0; i < users.Count; ++i)
            {
                if(users[i].TimesSeen >= Config.FC_PROMOTION_LIMIT)
                {
                    f.addLine(users[i].Username, "");
                }
            }

            f.insertEnd();
            return f.output;
        }

        public string ClearList()
        {
            users.Clear();
            WriteToFile();
            return "List Cleared";
        }

        public string DailyReset()
        {
            foreach (User u in users)
            {
                u.SeenToday = false;
            }
            WriteToFile();
            return "Daily reset complete.";
        }

        private void ReadFromFile()
        {
            string filepath = Config.FILEPATH + "\\FcUsers.txt";

            var file = File.OpenRead(filepath);
            StreamReader sr = new StreamReader(file);

            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split(',');

                User u = new User(values[0], Convert.ToBoolean(values[2]), Convert.ToInt32(values[1]));

                users.Add(u);
            }

            sr.Dispose();
            file.Dispose();
        }
        private void WriteToFile()
        {
            string filepath = Config.FILEPATH + "\\FcUsers.txt";

            var file = File.Create(filepath);
            StreamWriter sr = new StreamWriter(file);

            for (int i = 0; i < users.Count; ++i)
            {
                sr.WriteLine(users[i].Username + "," + users[i].TimesSeen + "," + users[i].SeenToday);
            }

            sr.Dispose();
            file.Dispose();
        }
    }
}
