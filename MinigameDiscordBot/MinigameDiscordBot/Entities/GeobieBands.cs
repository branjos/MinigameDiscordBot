using System;
using System.Collections.Generic;
using System.Linq;

namespace MinigameDiscordBot.Entities
{
    class GeobieBands
    {
        List<GeobieUser> users = new List<GeobieUser>();
        List<string> currentScouters = new List<string>();

        string[] skills = { "Agility/Crafting: ", "Farming/Herblore: ", "Hunter/Woodcutting: "};
        List<GeobieWorld> skill1 = new List<GeobieWorld>();
        List<GeobieWorld> skill2 = new List<GeobieWorld>();
        List<GeobieWorld> skill3 = new List<GeobieWorld>();
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
            List<GeobieWorld> newSkill1 = skill1.OrderBy(o => o.WorldNum).ToList();
            List<GeobieWorld> newSkill2 = skill2.OrderBy(o => o.WorldNum).ToList();
            List<GeobieWorld> newSkill3 = skill3.OrderBy(o => o.WorldNum).ToList();

            worlds[0] = "";
            worlds[1] = "";
            worlds[2] = "";

            for(int i = 0; i < newSkill1.Count; ++i)
            {
                if(i == 0)
                {
                    if (!newSkill1[i].IsDead)
                    {
                        worlds[0] += "**" + newSkill1[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[0] += "~~" + newSkill1[i].WorldNum.ToString() + "~~";
                    }
                }
                else
                {
                    if (!newSkill1[i].IsDead)
                    {
                        worlds[0] += ", **" + newSkill1[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[0] += "~~" + newSkill1[i].WorldNum.ToString() + "~~";
                    }
                }
            }
            for (int i = 0; i < newSkill2.Count; ++i)
            {
                if (i == 0)
                {
                    if (!newSkill2[i].IsDead)
                    {
                        worlds[1] += "**" + newSkill2[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[1] += "~~" + newSkill2[i].WorldNum.ToString() + "~~";
                    }
                }
                else
                {
                    if (!newSkill2[i].IsDead)
                    {
                        worlds[1] += ", **" + newSkill2[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[1] += "~~" + newSkill2[i].WorldNum.ToString() + "~~";
                    }
                }
            }
            for (int i = 0; i < newSkill3.Count; ++i)
            {
                if (i == 0)
                {
                    if (!newSkill3[i].IsDead)
                    {
                        worlds[2] += "**" + newSkill3[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[2] += "~~" + newSkill3[i].WorldNum.ToString() + "~~";
                    }
                }
                else
                {
                    if (!newSkill3[i].IsDead)
                    {
                        worlds[2] += ", **" + newSkill3[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[2] += "~~" + newSkill3[i].WorldNum.ToString() + "~~";
                    }
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

        //marks world as dead
        public string MarkAsDead(int worldNum)
        {
            GeobieWorld w = new GeobieWorld(worldNum);
            string output = "World not found.";

            if (skill1.Contains(w))
            {
                for (int i = 0; i < skill1.Count; ++i)
                {
                    if (skill1[i].WorldNum == worldNum)
                    {
                        skill1[i].IsDead = true;
                        output = worldNum.ToString() + " has been marked as dead.";
                    }
                }
            }
            else if (skill2.Contains(w))
            {
                for (int i = 0; i < skill2.Count; ++i)
                {
                    if (skill2[i].WorldNum == worldNum)
                    {
                        skill2[i].IsDead = true;
                        output = worldNum.ToString() + " has been marked as dead.";
                    }
                }
            }
            else if (skill3.Contains(w))
            {
                for (int i = 0; i < skill3.Count; ++i)
                {
                    if (skill3[i].WorldNum == worldNum)
                    {
                        skill3[i].IsDead = true;
                        output = worldNum.ToString() + " has been marked as dead.";
                    }
                }
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
                    GeobieWorld w = new GeobieWorld(world);
                    skill1.Add(w);
                    break;
                case "farm":
                    GeobieWorld x = new GeobieWorld(world);
                    skill2.Add(x);
                    break;
                case "hunt":
                    GeobieWorld y = new GeobieWorld(world);
                    skill3.Add(y);
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
