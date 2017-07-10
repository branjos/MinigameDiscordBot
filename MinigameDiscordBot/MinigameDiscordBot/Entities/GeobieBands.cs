using MinigameDiscordBot.Util;
using MinigamesDiscordBot;
using System;
using System.Collections.Generic;
using System.IO;

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

            skill1.Clear();
            skill2.Clear();
            skill3.Clear();

            for(int i = 0; i < users.Count; ++i)
            {
                users[i].CurrentScouter = false;
                users[i].NumCurrentScouts = 0;
            }

            return "Information cleared.";
        }

        //clears all of the users total caps 
        public string ClearInfoMonthly()
        {
            for(int i = 0; i < users.Count; ++i)
            {
                users[i].NumberOfScouts = 0;
            }
            return "Info cleared monthly.";
        }

        //adds a world the the list with the user who entered it
        public string AddWorld(string username, int world, string skill)
        {

            if (world > 0 && world <= 138)
            {

                //set up user to compare to the list
                GeobieUser u = new GeobieUser(username);
                GeobieWorld w = new GeobieWorld(world, username);

                if (!CheckIfWorldIsUsed(w))
                {
                    if (!w.IsFreeWorld)
                    {
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
                        return "That world is not availble.";
                    }
                }
                else
                {
                    return "World is already registered.";
                }
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
                if (users[i].CurrentScouter && users[i].NumCurrentScouts <= 1) 
                {
                    output += users[i].Username + "\n";
                }
                else if(users[i].CurrentScouter && users[i].NumCurrentScouts > 1)
                {
                    output += users[i].Username;
                    for(int j = 1; j < users[i].NumCurrentScouts; j++)
                    {
                        output += "*";
                    }
                    output += "\n";
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

        //outputs users total scouts
        public List<string> OutputTotalScouts()
        {
            List<string> output = new List<string>();

            OutputFormatting f = new OutputFormatting("User", 15, "Scouts", 7);

            for(int i = 0; i < users.Count; ++i)
            {
                if (f.output.Length >= 1900)
                {
                    f.insertEnd();
                    output.Add(f.output);
                    f = new OutputFormatting("Name", 16, "Status", 10);
                }

                f.addLine(users[i].Username, users[i].NumberOfScouts.ToString());

                if(i + 1 == users.Count)
                {
                    f.insertEnd();
                    output.Add(f.output);
                }
            }

            return output;
        }

        //the output for text files. Will mark line separators with a ;
        public string OutputForTextFile()
        {
            string output = "";
            output += skills[0] + ": ";
            for (int i = 0; i < skill1.Count; ++i)
            {
                if (i == 0)
                {
                    output += skill1[i];
                }
                else
                {
                    output += ", " + skill1[i];
                }
            }

            output += ";";

            output += skills[1];

            for (int i = 0; i < skill2.Count; ++i)
            {
                if (i == 0)
                {
                    output += skill2[i];
                }
                else
                {
                    output += ", " + skill2[i];
                }
            }

            output += ";";

            output += skills[2];

            for (int i = 0; i < skill3.Count; ++i)
            {
                if (i == 0)
                {
                    output += skill3[i];
                }
                else
                {
                    output += ", " + skill3[i];
                }
            }

            output += ";Scouters: ;";

            for(int i = 0; i < users.Count; ++i)
            {
                output += users[i].Username + ";";
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
                    if (u.Username.ToLower() == user.Username.ToLower() && !user.CurrentScouter)
                    {
                        user.NumberOfScouts++;
                        user.NumCurrentScouts++;
                        user.CurrentScouter = true;
                        WriteToFile();
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
                WriteToFile();
            }
        }

        //removes a scout from a user
        private void RemoveScoutFromUser(string username)
        {
            //loops through all users
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
                        WriteToFile();
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


        //writes GeobieUser data to a file
        private void WriteToFile()
        {
            string filepath = Config.FILEPATH + "\\Geobie\\Users.txt";

            var file = File.Create(filepath);
            StreamWriter sr = new StreamWriter(file);

            for(int i = 0; i < users.Count; ++i)
            {
                sr.WriteLine(users[i].Username + "," + users[i].NumberOfScouts);
            }

            sr.Dispose();
            file.Dispose();
        }

        //reads geobie user data from file on startup
        private void ReadFromFile()
        {
            string filepath = Config.FILEPATH + "\\Geobie\\Users.txt";

            var file = File.OpenRead(filepath);
            StreamReader sr = new StreamReader(file);

            string line = "";

            while((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split(',');

                GeobieUser u = new GeobieUser(values[0]);
                u.NumberOfScouts = Convert.ToInt32(values[1]);

                users.Add(u);
            }

            sr.Dispose();
            file.Dispose();
        }
    }
}
