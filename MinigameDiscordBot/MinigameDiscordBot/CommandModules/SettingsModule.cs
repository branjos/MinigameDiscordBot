using Discord.Commands;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinigameDiscordBot.CommandModules
{
    [Group("settings")]
    class SettingsModule : ModuleBase
    {
        [Command("warbandsserver")]
        public async Task UpdateWarbandsServer(ulong id)
        {

        }
    }
}
