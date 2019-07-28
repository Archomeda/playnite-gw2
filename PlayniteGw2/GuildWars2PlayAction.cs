using Playnite.SDK.Models;

namespace PlayniteGw2
{
    internal class GuildWars2PlayAction : GameAction
    {
        public GuildWars2PlayAction(GuildWars2AccountData accountData, Settings settings)
        {
            this.IsHandledByPlugin = true;
            this.Name = "Guild Wars 2";
            this.Path = accountData.ResolvePath(settings);
            this.Arguments = accountData.ResolveArguments(settings);
        }
    }
}
