using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinigamesDiscordBot.CommandModules
{
    class GeobieBandsModule : ModuleBase
    {
        List<string> usernames = new List<string>();

        [Command("s")]
        public async Task Scout([Remainder]string username)
        {
            string[] user = username.Split('-');
            usernames.Add(user[0]);
            await ReplyAsync(user[0] + " has been registered as a scout.");
        }

        [Command("PrintScouts")]
        public async Task PrintScouts()
        {
            string output = "```\n";
            for(int i = 0; i < usernames.Count; ++i)
            {
                output += usernames[i] + "\n";
            }
            output += "```";

            await ReplyAsync(output);
        }
    }
}
