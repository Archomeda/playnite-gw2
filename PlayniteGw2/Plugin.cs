using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;

namespace PlayniteGw2
{
    public interface ILibraryPlugin465 : ILibraryPlugin
    {
        bool IsClientInstalled { get; }
    }

    public class Plugin : ILibraryPlugin465
    {
        private readonly IPlayniteAPI api;

        public static Guid PluginId => Guid.Parse("FF5DEC28-B6B7-42A6-9816-E756A6F01DDE");

        public Plugin(IPlayniteAPI api)
        {
            this.api = api;
        }

        public void Dispose() { }

        public Guid Id => PluginId;

        public bool IsClientInstalled => true;

        public string Name => "Guild Wars 2";

        public string LibraryIcon { get; }

        public ILibraryClient Client { get; }

        public ISettings GetSettings(bool firstRunSettings) =>
            new Settings(this, this.api);

        public UserControl GetSettingsView(bool firstRunView) =>
            new SettingsView(this.api);

        public IEnumerable<Game> GetGames()
        {
            var settings = (Settings)this.GetSettings(false);
            return settings.GuildWars2Accounts.Select(a => a.ToPlayniteGame(settings));
        }

        public IGameController GetGameController(Game game) =>
            new GuildWars2GameController(this.api, (Settings)this.GetSettings(false), game);

        public ILibraryMetadataProvider GetMetadataDownloader() =>
            new GuildWars2MetadataProvider();
    }
}
