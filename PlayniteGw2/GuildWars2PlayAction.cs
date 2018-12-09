using Playnite.SDK.Models;

namespace PlayniteGw2
{
    internal class GuildWars2PlayAction : GameAction
    {
        private readonly GuildWars2AccountData accountData;

        public GuildWars2PlayAction(GuildWars2AccountData accountData, Settings settings)
        {
            this.accountData = accountData;
            this.IsHandledByPlugin = true;
            this.Name = "Guild Wars 2";
            this.Path = accountData.ResolvePath(settings);
            this.Arguments = accountData.ResolveArguments(settings);
        }
    }
}
