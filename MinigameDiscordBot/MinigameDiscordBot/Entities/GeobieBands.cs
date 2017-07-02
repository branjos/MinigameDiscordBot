using System;
using System.Collections.Generic;

namespace MinigameDiscordBot.Entities
{
    class GeobieBands
    {
        List<GeobieUser> users = new List<GeobieUser>();

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

        //clears the info from all areas
        public string ClearInfo()
        {
            for(int i = 0; i < worlds.Length; ++i)
            {
                worlds[i] = "";
            }
            for(int i = 0; i < users.Count; ++i)
            {
                users[i].CurrentScouter = false;
            }

            return "Information cleared.";
        }

        //adds a world the the list with the user who entered it
        public string AddWorld(string username, int world, string skill)
        {

            if (world > 0 && world <= 138)
            {

                //set up user to compare to the list
                GeobieUser u = new GeobieUser(username);
                GeobieWorld w = new GeobieWorld(world, username);

                string output = ""; //output

                switch (skill.ToLower())
                {
                    case "a":
                        AddScoutToUSer(u);
                        AddWorldToSkill("agil", w);
                        output = u.Username + " added as a scout.";
                        break;
                    case "f":
                        AddScoutToUSer(u);
                        AddWorldToSkill("farm", w);
                        output = u.Username + " added as a scout.";
                        break;
                    case "w":
                        AddScoutToUSer(u);
                        AddWorldToSkill("hunt", w);
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

            worlds[0] = "";
            worlds[1] = "";
            worlds[2] = "";

            for(int i = 0; i < skill1.Count; ++i)
            {
                if(i == 0)
                {
                    if (!skill1[i].IsDead)
                    {
                        worlds[0] += "**" + skill1[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[0] += "~~" + skill1[i].WorldNum.ToString() + "~~";
                    }
                }
                else
                {
                    if (!skill1[i].IsDead)
                    {
                        worlds[0] += ", **" + skill1[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[0] += ", ~~" + skill1[i].WorldNum.ToString() + "~~";
                    }
                }
            }
            for (int i = 0; i < skill2.Count; ++i)
            {
                if (i == 0)
                {
                    if (!skill2[i].IsDead)
                    {
                        worlds[1] += "**" + skill2[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[1] += "~~" + skill2[i].WorldNum.ToString() + "~~";
                    }
                }
                else
                {
                    if (!skill2[i].IsDead)
                    {
                        worlds[1] += ", **" + skill2[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[1] += ", ~~" + skill2[i].WorldNum.ToString() + "~~";
                    }
                }
            }
            for (int i = 0; i < skill3.Count; ++i)
            {
                if (i == 0)
                {
                    if (!skill3[i].IsDead)
                    {
                        worlds[2] += "**" + skill3[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[2] += "~~" + skill3[i].WorldNum.ToString() + "~~";
                    }
                }
                else
                {
                    if (!skill3[i].IsDead)
                    {
                        worlds[2] += ", **" + skill3[i].WorldNum.ToString() + "**";
                    }
                    else
                    {
                        worlds[2] += ", ~~" + skill3[i].WorldNum.ToString() + "~~";
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
            for(int i = 0; i < users.Count; ++i)
            {
                if (users[i].CurrentScouter)
                {
                    output += users[i].Username + "\n";
                }
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

        //removes a world from the list
        public string RemoveWorld(int world)
        {
            GeobieWorld w = new GeobieWorld(world);

            string output = "World " + world + " removed.";

            if (skill1.Contains(w))
            {
                for(int i = 0; i < skill1.Count; ++i)
                {
                    if (w.Equals(skill1[i]))
                    {
                        GeobieUser u = new GeobieUser(skill1[i].ScoutName);
                        if (users.Contains(u))
                        {
                            RemoveScoutFromUser(u.Username);
                        }
                    }
                }
                skill1.Remove(w);
            }
            else if (skill2.Contains(w))
            {
                for (int i = 0; i < skill2.Count; ++i)
                {
                    if (w.Equals(skill2[i]))
                    {
                        GeobieUser u = new GeobieUser(skill2[i].ScoutName);
                        if (users.Contains(u))
                        {
                            RemoveScoutFromUser(u.Username);
                        }
                    }
                }
                skill2.Remove(w);
            }
            else if (skill3.Contains(w))
            {
                for (int i = 0; i < skill3.Count; ++i)
                {
                    if (w.Equals(skill3[i]))
                    {
                        GeobieUser u = new GeobieUser(skill3[i].ScoutName);
                        if (users.Contains(u))
                        {
                            RemoveScoutFromUser(u.Username);
                        }
                    }
                }
                skill3.Remove(w);
            }
            else
            {
                output = "World not found.";
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
                    if (u.Username == user.Username && !user.CurrentScouter)
                    {
                        user.NumberOfScouts++;
                        user.NumCurrentScouts++;
                        user.CurrentScouter = true;
                    }
                    else
                    {
                        user.NumCurrentScouts++;
                    }
                }
            }
            else
            {
                users.Add(u);
                u.NumberOfScouts++;
                u.NumCurrentScouts++;
                u.CurrentScouter = true;
            }
        }

        //removes a scout from a user
        private void RemoveScoutFromUser(string username)
        {
            //loops through al users
            for (int i = 0; i < users.Count; ++i)
            {
                //if user is found
                if (users[i].Username == username)
                {
                    //if current scouts is greater than 1
                    if (users[i].NumCurrentScouts > 1)
                    {
                        users[i].NumCurrentScouts--;
                    }
                    //else reduce current scouts to 0, make the user not a current scouter, and reduce their total scouts
                    else
                    {
                        users[i].NumCurrentScouts = 0;
                        users[i].CurrentScouter = false;
                        users[i].NumberOfScouts--;
                    }
                    break;
                }
            }
        }

        //adds a world number to a skill set
        private void AddWorldToSkill(string skill, GeobieWorld world)
        {
            switch (skill)
            {
                case "agil":
                    if (!CheckIfWorldIsUsed(world))
                    {
                        skill1.Add(world);
                    }                   
                    break;
                case "farm":
                    if (!CheckIfWorldIsUsed(world))
                    {
                        skill2.Add(world);
                    }
                    break;
                case "hunt":
                    if (!CheckIfWorldIsUsed(world))
                    {
                        skill3.Add(world);
                    }
                    break;
            }
        }
        
        //returns false if world is not used, true if used
        private bool CheckIfWorldIsUsed(GeobieWorld w)
        {
            bool found = false;

            for (int i = 0; i < skill1.Count; ++i)
            {
                if(skill1[i].Equals(w))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                for (int i = 0; i < skill2.Count; ++i)
                {
                    if (skill2[i].Equals(w))
                    {
                        found = true;
                        break;
                    }
                }
            }
            if (!found)
            {
                for (int i = 0; i < skill3.Count; ++i)
                {
                    if (skill3[i].Equals(w))
                    {
                        found = true;
                        break;
                    }
                }
            }

            return found;
        }

        private void WriteToFile()
        {

        }

        private void ReadFromFile()
        {

        }
    }
}
