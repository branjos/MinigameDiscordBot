using System;
using System.Collections.Generic;

namespace MinigameDiscordBot.Entities
{
    class GeobieBands
    {
        List<GeobieUser> users = new List<GeobieUser>();
        List<string> currentScouters = new List<string>();

        string[] skills = { "Agility/Crafting: ", "Farming/Herblore: ", "Hunter/Woodcutting: "};
        List<int> skill1 = new List<int>();
        List<int> skill2 = new List<int>();
        List<int> skill3 = new List<int>();
        string[] worlds = new string[3];

        DateTime LastUpdated = DateTime.Now;

        public GeobieBands()
        {
            ReadFromFile();
        }

        public string ClearInfo()
        {
            for(int i = 0; i < worlds.Length; ++i)
            {
                worlds[i] = "";
            }
            currentScouters.Clear();

            return "Information cleared.";
        }

        //adds a world the the list with the user who entered it
        public string AddWorld(string username, int world, string skill)
        {

            if (world > 0 && world <= 138)
            {

                //set up user to compare to the list
                GeobieUser u = new GeobieUser(username);

                string output = ""; //output

                switch (skill.ToLower())
                {
                    case "a":
                        AddScoutToUSer(u);
                        if (!currentScouters.Contains(u.Username))
                        {
                            currentScouters.Add(u.Username);
                        }
                        AddWorldToSkill("agil", world);
                        output = u.Username + " added as a scout.";
                        break;
                    case "f":
                        AddScoutToUSer(u);
                        if (!currentScouters.Contains(u.Username))
                        {
                            currentScouters.Add(u.Username);
                        }
                        AddWorldToSkill("farm", world);
                        output = u.Username + " added as a scout.";
                        break;
                    case "w":
                        AddScoutToUSer(u);
                        if (!currentScouters.Contains(u.Username))
                        {
                            currentScouters.Add(u.Username);
                        }
                        AddWorldToSkill("hunt", world);
                        output = u.Username + " added as a scout.";
                        break;
                    default:
                        output = "Incorrect skills.";
                        break;
                }

                LastUpdated = DateTime.Now;

                return output;
            }
            else
            {
                return "Invalid world.";
            }
        }
        
        //returns the output for geobiebands
        public string GetOutput()
        {
            skill1.Sort();
            skill2.Sort();
            skill3.Sort();

            worlds[0] = "";
            worlds[1] = "";
            worlds[2] = "";

            for(int i = 0; i < skill1.Count; ++i)
            {
                if(i == 0)
                {
                    worlds[0] += skill1[i].ToString();
                }
                else
                {
                    worlds[0] += ", " + skill1[i].ToString();
                }
            }
            for (int i = 0; i < skill2.Count; ++i)
            {
                if (i == 0)
                {
                    worlds[1] += skill2[i].ToString();
                }
                else
                {
                    worlds[1] += ", " + skill2[i].ToString();
                }
            }
            for (int i = 0; i < skill3.Count; ++i)
            {
                if (i == 0)
                {
                    worlds[2] += skill3[i].ToString();
                }
                else
                {
                    worlds[2] += ", " + skill3[i].ToString();
                }
            }

            //beginning of output
            string output = "";
            output += "**Worlds** \n";
            for(int i = 0; i < worlds.Length; ++i)
            {
                output += skills[i] + worlds[i] + "\n";
            }

            output += "\n";
            output += "**Scouters** \n";
            for(int i = 0; i < currentScouters.Count; ++i)
            {
                output += currentScouters[i] + "\n";
            }

            return output;
        }

        //check if user is on the list
        private bool IsUserRegistered(GeobieUser user)
        {
            if (users.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //adds one scout to a user
        private void AddScoutToUSer(GeobieUser u)
        {
            if (IsUserRegistered(u))
            {
                foreach (GeobieUser user in users)
                {
                    if (u.Username == user.Username)
                    {
                        u.NumberOfScouts++;
                    }
                }
            }
            else
            {
                users.Add(u);
                u.NumberOfScouts++;
            }
        }

        private void AddWorldToSkill(string skill, int world)
        {
            switch (skill)
            {
                case "agil":
                    skill1.Add(world);
                    break;
                case "farm":
                    skill2.Add(world);
                    break;
                case "hunt":
                    skill3.Add(world);
                    break;
            }
        }

        private void WriteToFile()
        {

        }

        private void ReadFromFile()
        {

        }
    }
}
