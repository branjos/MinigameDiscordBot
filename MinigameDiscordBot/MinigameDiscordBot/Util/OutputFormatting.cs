using System;

namespace MinigameDiscordBot.Util
{
    class OutputFormatting
    {
        public string output { get; set; }
        private int col1size = 0;
        private int col2size = 0;
        private int col3size = 0;
        private int col4size = 0;
        private int col5size = 0;
        private int extraSpaces = 0;

        //if 2 columns
        public OutputFormatting(string col1, int size1, string col2, int size2)
        {
            col1size = size1;
            col2size = size2;
            extraSpaces = 1;
            output = "```\n";
            output += "\n+" + insertDashes(size1 + size2 + extraSpaces) + "+";
            output += "\n|" + addToLine(col1, size1) + addToLine(col2, size2);
            output += "\n+" + insertDashes(size1 + size2 + extraSpaces) + "+";
        }
        //if 3 columns
        public OutputFormatting(string col1, int size1, string col2, int size2,
            string col3, int size3)
        {
            col1size = size1;
            col2size = size2;
            col3size = size3;
            extraSpaces = 2;
            output = "```\n";
            output += "\n+" + insertDashes(size1 + size2 + size3 + extraSpaces) + "+";
            output += "\n|" + addToLine(col1, size1) + addToLine(col2, size2) + addToLine(col3, size3);
            output += "\n+" + insertDashes(size1 + size2 + size3 + extraSpaces) + "+";
        }
        //if 4 columns
        public OutputFormatting(string col1, int size1, string col2, int size2,
            string col3, int size3, string col4, int size4)
        {
            col1size = size1;
            col2size = size2;
            col3size = size3;
            col4size = size4;
            extraSpaces = 3;
            output = "```\n";
            output += "\n+" + insertDashes(size1 + size2 + size3 + size4 + extraSpaces) + "+";
            output += "\n|" + addToLine(col1, size1) + addToLine(col2, size2) +
                addToLine(col3, size3) + addToLine(col4, size4);
            output += "\n+" + insertDashes(size1 + size2 + size3 + size4 + extraSpaces) + "+";
        }
        //if 5 columns
        public OutputFormatting(string col1, int size1, string col2, int size2,
            string col3, int size3, string col4, int size4, string col5, int size5)
        {
            col1size = size1;
            col2size = size2;
            col3size = size3;
            col4size = size4;
            col5size = size5;
            extraSpaces = 4;
            output = "```\n";
            output += "\n+" + insertDashes(size1 + size2 + size3 + size4 + size5 + extraSpaces) + "+";
            output += "\n|" + addToLine(col1, size1) + addToLine(col2, size2) +
                addToLine(col3, size3) + addToLine(col4, size4) + addToLine(col5, size5);
            output += "\n+" + insertDashes(size1 + size2 + size3 + size4 + size5 + extraSpaces) + "+";
        }

        public void insertEnd()
        {
            output += "\n+" + insertDashes(col1size + col2size + col3size + col4size + col5size + extraSpaces) + "+";
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
        public void addLine(string item1, string item2, string item3)
        {
            output += "\n|";
            output += addToLine(item1, col1size);
            output += addToLine(item2, col2size);
            output += addToLine(item3, col3size);
        }
        public void addLine(string item1, string item2, string item3, string item4)
        {
            output += "\n|";
            output += addToLine(item1, col1size);
            output += addToLine(item2, col2size);
            output += addToLine(item3, col3size);
            output += addToLine(item4, col4size);
        }
        public void addLine(string item1, string item2, string item3, string item4, string item5)
        {
            output += "\n|";
            output += addToLine(item1, col1size);
            output += addToLine(item2, col2size);
            output += addToLine(item3, col3size);
            output += addToLine(item4, col4size);
            output += addToLine(item5, col5size);
        }
    }
}
