using System;


namespace MinigamesDiscordBot.Util
{
    class CastleWarsOutputTable
    {
        public string output { get; set; }
        private const int col1size = 20;
        private const int col2size = 20;
        private int extraSpaces = 0;

        public CastleWarsOutputTable()
        {
            extraSpaces = 1;
            output = "```\n";
            output += "\n+" + insertDashes(col1size + col2size + extraSpaces) + "+";
            output += "\n|" + addToLine("Saradomin", col1size) + addToLine("Zamorak", col2size);
            output += "\n+" + insertDashes(col1size + col2size + extraSpaces) + "+";
        }

        public void insertEnd()
        {
            output += "\n+" + insertDashes(col1size + col2size + extraSpaces) + "+";
            output += "```";
        }

        private string insertSpaces(int numSpaces)
        {
            string temp = "";
            for (int i = 0; i < numSpaces; ++i)
            {
                temp += " ";
            }
            return temp;
        }

        private string insertDashes(int numDashes)
        {
            string temp = "";
            for (int i = 0; i < numDashes; ++i)
            {
                temp += "-";
            }
            return temp;
        }

        private string addToLine(string item, int numSpots)
        {
            string temp = "";

            if (item.ToString().Length <= numSpots)
            {
                int length = item.ToString().Length;

                if (length - numSpots % 2 == 0)
                {
                    temp += insertSpaces((numSpots - length) / 2);
                    temp += item.ToString();
                    temp += insertSpaces((numSpots - length) / 2) + "|";
                }
                else
                {
                    temp += insertSpaces((int)Math.Floor((decimal)(numSpots - length) / 2));
                    temp += item.ToString();
                    temp += insertSpaces((int)Math.Ceiling((decimal)(numSpots - length) / 2)) + "|";
                }
            }
            return temp;
        }

        public void addLine(string item1, string item2)
        {
            output += "\n|";
            output += addToLine(item1, col1size);
            output += addToLine(item2, col2size);
        }
    }
}
