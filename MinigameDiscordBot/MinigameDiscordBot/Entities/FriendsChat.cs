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
                        output = "User had already been seen.";
                    }
                }
            }

            if (!found)
            {
                User u = new User();
                u.Username = username;
                users.Add(u);
                output = "User created.";
            }

            return output;
        }



    }
}
