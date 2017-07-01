using System;

namespace MinigameDiscordBot.Entities
{
    class GeobieWorld : IEquatable<GeobieWorld>
    {
        public int WorldNum { get; set; }
        public bool IsDead { get; set; }

        public GeobieWorld(int worldNum)
        {
            WorldNum = worldNum;
            IsDead = false;
        }

        public bool Equals(GeobieWorld other)
        {
            if(other == null) { return false; }
            if(other.WorldNum == WorldNum) { return true; }
            else { return false; }
        }
    }
}
