using System;
using System.Collections.Generic;
using System.Text;

namespace MinigameDiscordBot.Entities
{
    class WarbandsWorld : IEquatable<WarbandsWorld>
    {
        public int WorldNum { get; }
        public string Status { get; set; }
        public bool IsFreeWorld { get; }
        private int[] freeWorlds = { 3, 7, 8, 11, 17, 19, 20, 29, 33, 34, 38, 41, 43, 57, 61, 80,
            81, 108, 120, 135, 136, 141, 13, 93, 94, 109, 110, 111, 112, 113, 125, 126, 127, 128,
            129, 130, 131, 132, 133, 47, 55, 75, 101, 102, 118, 121, 122 };

        public WarbandsWorld(int worldnum)
        {
            WorldNum = worldnum;
            Status = "beaming";
            IsFreeWorld = false;
            for (int i = 0; i < freeWorlds.Length; ++i)
            {
                if (freeWorlds[i] == WorldNum)
                {
                    IsFreeWorld = true;
                    break;
                }
            }
        }

        public bool Equals(WarbandsWorld other)
        {
            if (other == null) { return false; }
            if (other.WorldNum == WorldNum) { return true; }
            else { return false; }
        }
    }
}
