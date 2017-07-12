using System;
using System.Collections.Generic;
using System.Text;

namespace MinigameDiscordBot.Entities
{
    class Warbands
    {
        List<WarbandsWorld> dwfWorlds;
        List<WarbandsWorld> elmWorlds;
        List<WarbandsWorld> rdiWorlds;

        public Warbands()
        {
            dwfWorlds = new List<WarbandsWorld>();
            elmWorlds = new List<WarbandsWorld>();
            rdiWorlds = new List<WarbandsWorld>();
        }

        public string AddWorld(int num, string loc)
        {
            WarbandsWorld w = new WarbandsWorld(num);

            if(loc == "dwf")
            {
                dwfWorlds.Add(w);
                return "World " + num + " added as a " + loc + "world.";
            }
            else if(loc == "elm")
            {
                elmWorlds.Add(w);
                return "World " + num + " added as a " + loc + "world.";
            }
            else if(loc == "rdi")
            {
                rdiWorlds.Add(w);
                return "World " + num + " added as a " + loc + "world.";
            }
            else
            {
                return "Invalid location.";
            }
        }
    }
}
