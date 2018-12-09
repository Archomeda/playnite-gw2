using Playnite.SDK.Metadata;
using Playnite.SDK.Models;
using PlayniteGw2.Proxy;

namespace PlayniteGw2
{
    internal class GuildWars2MetadataProvider : IGDBMetadataProviderProxy
    {
        public override GameMetadata GetMetadata(Game game)
        {
            var searchGame = new Game("Guild Wars 2");
            var playniteMetadata = base.GetMetadata(searchGame);
            if (playniteMetadata.IsEmpty)
                return playniteMetadata;

            playniteMetadata.GameData.Name = game.Name;
            playniteMetadata.GameData.Id = game.Id;
            playniteMetadata.GameData.GameId = game.GameId;
            playniteMetadata.GameData.InstallDirectory = game.InstallDirectory;
            playniteMetadata.GameData.IsInstalled = game.IsInstalled;
            playniteMetadata.GameData.Icon = game.Icon;
            playniteMetadata.GameData.PlayAction = game.PlayAction;
            playniteMetadata.GameData.PluginId = game.PluginId;
            return playniteMetadata;
        }
    }
}
