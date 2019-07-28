using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using PlayniteGw2.Proxy;

namespace PlayniteGw2
{
    public class Plugin : LibraryPlugin
    {
        private readonly IPlayniteAPI api;

        public static Guid PluginId => Guid.Parse("FF5DEC28-B6B7-42A6-9816-E756A6F01DDE");

        public Plugin(IPlayniteAPI api) : base(api)
        {
            this.api = api;
        }

        public override Guid Id => PluginId;

        public bool IsClientInstalled => true;

        public override string Name => "Guild Wars 2";

        public override ISettings GetSettings(bool firstRunSettings) =>
            new Settings(this, this.api);

        public override UserControl GetSettingsView(bool firstRunView) =>
            new SettingsView(this.api);

        public override IEnumerable<GameInfo> GetGames()
        {
            var settings = (Settings)this.GetSettings(false);
            return settings.GuildWars2Accounts.Select(a => a.ToPlayniteGameInfo(settings));
        }

        public override IGameController GetGameController(Game game) =>
            new GuildWars2GameController(this.api, (Settings)this.GetSettings(false), game);

        public override LibraryMetadataProvider GetMetadataDownloader() =>
            new IGDBMetadataProviderProxy("Guild Wars 2");
    }
}
