using MinigameDiscordBot.Util;
using MinigamesDiscordBot;
using MinigamesDiscordBot.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinigameDiscordBot.Entities
{
    class FriendsChat
    {
        List<User> users = new List<User>();

        public FriendsChat()
        {
            //read from file

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
            return "List Cleared";
        }

        public void DailyReset()
        {
            foreach (User u in users)
            {
                u.SeenToday = false;
            }
        }

    }
}
