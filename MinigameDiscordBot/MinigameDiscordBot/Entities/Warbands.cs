using System.Collections.Generic;
using System.Linq;

namespace MinigameDiscordBot.Entities
{
    class Warbands
    {
        List<WarbandsWorld> dwfWorlds;
        List<WarbandsWorld> elmWorlds;
        List<WarbandsWorld> rdiWorlds;
        string[] worlds;

        public Warbands()
        {
            dwfWorlds = new List<WarbandsWorld>();
            elmWorlds = new List<WarbandsWorld>();
            rdiWorlds = new List<WarbandsWorld>();
            worlds = new string[3];

            ClearList();
        }

        //adds a world to the correct list
        public string AddWorld(int num, string loc)
        {
            WarbandsWorld w = new WarbandsWorld(num);

            if(loc == "dwf")
            {
                if (!w.IsFreeWorld)
                {
                    dwfWorlds.Add(w);
                    return "World " + num + " added as a " + loc + "world.";
                }
                else
                {
                    return "World " + num + " is an invalid world.";
                }
            }
            else if(loc == "elm")
            {
                if (!w.IsFreeWorld)
                {
                    elmWorlds.Add(w);
                    return "World " + num + " added as a " + loc + "world.";
                }
                else
                {
                    return "World " + num + " is an invalid world.";
                }
            }
            else if(loc == "rdi")
            {
                if (!w.IsFreeWorld)
                {
                    rdiWorlds.Add(w);
                    return "World " + num + " added as a " + loc + "world.";
                }
                else
                {
                    return "World " + num + " is an invalid world.";
                }
            }
            else
            {
                return "Invalid location.";
            }
        }

        //changes a world's status to status
        public string ChageWorldStatus(int world, string status)
        {
            WarbandsWorld w = new WarbandsWorld(world);
            string output = "";

            if (status == "beaming" || status == "dead" || status == "broken")
            {
                if (dwfWorlds.Contains(w))
                {
                    for (int i = 0; i < dwfWorlds.Count; ++i)
                    {
                        if (dwfWorlds[i].Equals(w))
                        {
                            dwfWorlds[i].Status = status;
                            output = "World " + world + " updated.";
                        }
                    }
                }
                else if (elmWorlds.Contains(w))
                {
                    for (int i = 0; i < elmWorlds.Count; ++i)
                    {
                        if (elmWorlds[i].Equals(w))
                        {
                            elmWorlds[i].Status = status;
                            output = "World " + world + " updated.";
                        }
                    }
                }
                else if (rdiWorlds.Contains(w))
                {
                    for (int i = 0; i < rdiWorlds.Count; ++i)
                    {
                        if (rdiWorlds[i].Equals(w))
                        {
                            rdiWorlds[i].Status = status;
                            output = "World " + world + " updated.";
                        }
                    }
                }
                else
                {
                    output = "World " + world + " is not an active world.";
                }
            }
            else
            {
                output = "Invalid status.";
            }

            return output;
        }

        //clears all lists
        public string ClearList()
        {
            dwfWorlds.Clear();
            elmWorlds.Clear();
            rdiWorlds.Clear();
            worlds[0] = "";
            worlds[1] = "";
            worlds[2] = "";
            return "Worlds cleared";
        }

        public string GetOutput()
        {
            string output = "";

            worlds[0] = ""; worlds[1] = ""; worlds[2] = "";

            List<WarbandsWorld> dwfsorted = dwfWorlds.OrderBy(o => o.WorldNum).ToList();
            List<WarbandsWorld> elmsorted = elmWorlds.OrderBy(o => o.WorldNum).ToList();
            List<WarbandsWorld> rdisorted = rdiWorlds.OrderBy(o => o.WorldNum).ToList();

            output += "Beaming - **99**, Broken - 99, Dead - ~~99~~\n\n";

            for(int i = 0; i < dwfsorted.Count; ++i)
            {
                if(worlds[0] == "")
                {
                    worlds[0] += OutputWorldWithStatus(dwfsorted[i]);
                }
                else
                {
                    worlds[0] += ", " + OutputWorldWithStatus(dwfsorted[i]);
                }
            }
            for (int i = 0; i < elmsorted.Count; ++i)
            {
                if (worlds[1] == "")
                {
                    worlds[1] += OutputWorldWithStatus(elmsorted[i]);
                }
                else
                {
                    worlds[1] += ", " + OutputWorldWithStatus(elmsorted[i]);
                }
            }
            for (int i = 0; i < rdisorted.Count; ++i)
            {
                if (worlds[2] == "")
                {
                    worlds[2] += OutputWorldWithStatus(rdisorted[i]);
                }
                else
                {
                    worlds[2] += ", " + OutputWorldWithStatus(rdisorted[i]);
                }
            }

            output += "DWF: " + worlds[0] + "\n";
            output += "ELM: " + worlds[1] + "\n";
            output += "RDI: " + worlds[2] + "\n";

            return output;
        }

        //checks if the world is registered - returns true/false
        public bool CheckIfWorldRegistered(int worldNum)
        {
            WarbandsWorld w = new WarbandsWorld(worldNum);

            if(dwfWorlds.Contains(w) || elmWorlds.Contains(w) || rdiWorlds.Contains(w))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string OutputWorldWithStatus(WarbandsWorld w)
        {
            string output = "";

            if (w.Status == "beaming")
            {
                output = "**" + w.WorldNum + "**";
            }
            else if(w.Status == "broken")
            {
                output = w.WorldNum.ToString();
            }
            else if(w.Status == "dead")
            {
                output = "~~" + w.WorldNum + "~~";
            }

            return output;
        }
    }
}
