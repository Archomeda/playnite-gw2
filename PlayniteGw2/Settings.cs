using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Playnite.SDK;

namespace PlayniteGw2
{
    public class Settings : ObservableObject, ISettings
    {
        private readonly IPlayniteAPI api;
        private readonly Plugin plugin;
        private string defaultPath = string.Empty;
        private string defaultArguments = string.Empty;

        public Settings() { }

        public Settings(Plugin plugin, IPlayniteAPI api)
        {
            this.api = api;
            this.plugin = plugin;

            var savedSettings = plugin.LoadPluginSettings<Settings>();
            if (savedSettings != null)
            {
                this.GuildWars2Accounts = savedSettings.GuildWars2Accounts;
                this.DefaultPath = savedSettings.DefaultPath;
                this.DefaultArguments = savedSettings.DefaultArguments;
            }
        }

        public string DefaultPath
        {
            get => this.defaultPath;
            set
            {
                if (this.defaultPath != value)
                {
                    this.defaultPath = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DefaultArguments
        {
            get => this.defaultArguments;
            set
            {
                if (this.defaultArguments != value)
                {
                    this.defaultArguments = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICollection<GuildWars2AccountData> GuildWars2Accounts { get; set; } = new ObservableCollection<GuildWars2AccountData>();

        public void BeginEdit()
        {
            // Method intentionally left empty.
        }

        public void CancelEdit()
        {
            // Method intentionally left empty.
        }

        public void EndEdit()
        {
            this.plugin.SavePluginSettings(this);

            var addedGames = new HashSet<string>(this.GuildWars2Accounts.Select(a => a.InternalId.ToString()));
            var games = this.api.Database.Games
                .Where(g => addedGames.Contains(g.GameId))
                .ToDictionary(g => g.GameId, g => g);

            foreach (var account in this.GuildWars2Accounts)
            {
                if (games.TryGetValue(account.InternalId.ToString(), out var game))
                    this.api.Database.Games.Update(account.UpdatePlayniteGame(game, this));
                else
                {
                    game = account.ToPlayniteGame(this);
                    this.api.Database.Games.Add(game);
                }
            }

            var pluginGames = this.api.Database.Games.Where(g => g.PluginId == Plugin.PluginId);
            foreach (var game in pluginGames)
            {
                if (!addedGames.Contains(game.GameId))
                    this.api.Database.Games.Remove(game);
            }
        }

        public bool VerifySettings(out List<string> errors)
        {
            errors = new List<string>();
            if (!Validation.IsValidGw2Executable(this.DefaultPath))
                errors.Add("The default Guild Wars 2 path is not a valid executable.");

            foreach (var account in this.GuildWars2Accounts)
            {
                string accountName = !string.IsNullOrEmpty(account.Name) ? $"Guild Wars 2 account '{account.Name}'" : "a Guild Wars 2 account";
                if (!Validation.IsValidAccountId(account.Id))
                    errors.Add($"The ID for {accountName} is not valid. It cannot be empty and only alphanumeric characters are allowed.");
                if (!Validation.IsValidGw2Executable(account.ExecutablePath, false))
                    errors.Add($"The path for {accountName} is not a valid executable.");
            }
            return errors.Count == 0;
        }
    }
}
