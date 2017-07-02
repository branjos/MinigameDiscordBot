using System;

namespace MinigameDiscordBot.Entities
{
    class GeobieWorld : IEquatable<GeobieWorld>
    {
        public int WorldNum { get; set; }
        public bool IsDead { get; set; }
        public string ScoutName { get; set; }

        public GeobieWorld(int worldNum)
        {
            WorldNum = worldNum;
        }

        public GeobieWorld(int worldNum, string scout)
        {
            WorldNum = worldNum;
            IsDead = false;
            ScoutName = scout;
        }

        public bool Equals(GeobieWorld other)
        {
            if(other == null) { return false; }
            if(other.WorldNum == WorldNum) { return true; }
            else { return false; }
        }
    }
}
