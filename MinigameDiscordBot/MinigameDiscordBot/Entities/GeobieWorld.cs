using System;

namespace MinigameDiscordBot.Entities
{
    class GeobieWorld : IEquatable<GeobieWorld>
    {
        public int WorldNum { get; }
        public bool IsDead { get; set; }
        public string ScoutName { get; }
        public bool IsFreeWorld { get; }
        private int[] freeWorlds = { 3, 7, 8, 11, 17, 19, 20, 29, 33, 34, 38, 41, 43, 57, 61, 80,
            81, 108, 120, 135, 136, 141, 13, 93, 94, 109, 110, 111, 112, 113, 125, 126, 127, 128,
            129, 130, 131, 132, 133, 47, 55, 75, 101, 102, 118, 121, 122 };

        public GeobieWorld(int worldNum)
        {
            WorldNum = worldNum;
        }

        public GeobieWorld(int worldNum, string scout)
        {
            WorldNum = worldNum;
            IsDead = false;
            IsFreeWorld = false;
            ScoutName = scout;
            for(int i = 0; i < freeWorlds.Length; ++i)
            {
                if(freeWorlds[i] == WorldNum)
                {
                    IsFreeWorld = true;
                    break;
                }
            }
        }

        public bool Equals(GeobieWorld other)
        {
            if(other == null) { return false; }
            if(other.WorldNum == WorldNum) { return true; }
            else { return false; }
        }
    }
}
