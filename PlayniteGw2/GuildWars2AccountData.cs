using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Playnite.SDK.Models;

namespace PlayniteGw2
{
    public class GuildWars2AccountData : ObservableObject
    {
        private Guid internalId = Guid.NewGuid();
        private string id = string.Empty;
        private string name = string.Empty;
        private string executablePath = string.Empty;
        private string arguments = string.Empty;

        public Guid InternalId
        {
            get => this.internalId;
            set
            {
                if (this.internalId != value)
                {
                    this.internalId = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Id
        {
            get => this.id;
            set
            {
                string newValue = value ?? string.Empty;
                if (this.id != newValue)
                {
                    this.id = newValue;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => this.name;
            set
            {
                string newValue = value ?? string.Empty;
                if (this.name != newValue)
                {
                    this.name = newValue;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ExecutablePath
        {
            get => this.executablePath;
            set
            {
                string newValue = value ?? string.Empty;
                if (this.executablePath != newValue)
                {
                    this.executablePath = newValue;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Arguments
        {
            get => this.arguments;
            set
            {
                string newValue = value ?? string.Empty;
                if (this.arguments != newValue)
                {
                    this.arguments = newValue;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ResolvePath(Settings settings) =>
            !string.IsNullOrEmpty(this.ExecutablePath) ? this.ExecutablePath : settings.DefaultPath;

        public string ResolveArguments(Settings settings) =>
            !string.IsNullOrEmpty(this.Arguments) ? this.Arguments : settings.DefaultArguments;

        public GameInfo ToPlayniteGameInfo(Settings settings)
        {
            return new GameInfo
            {
                GameId = this.InternalId.ToString(),
                Name = $"Guild Wars 2 - {this.Name}",
                IsInstalled = true,
                Icon = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "gw2.png"),
                InstallDirectory = Path.GetDirectoryName(this.ResolvePath(settings)),
                PlayAction = new GuildWars2PlayAction(this, settings)
            };
        }

        public Game ToPlayniteGame(Settings settings)
        {
            return new Game
            {
                GameId = this.InternalId.ToString(),
                Name = $"Guild Wars 2 - {this.Name}",
                IsInstalled = true,
                Icon = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "gw2.png"),
                InstallDirectory = Path.GetDirectoryName(this.ResolvePath(settings)),
                PlayAction = new GuildWars2PlayAction(this, settings)
            };
        }

        public Game UpdatePlayniteGame(Game originalGame, Settings settings)
        {
            originalGame.GameId = this.internalId.ToString();
            originalGame.InstallDirectory = Path.GetDirectoryName(this.ResolvePath(settings));
            originalGame.IsInstalled = true;
            if (string.IsNullOrEmpty(originalGame.Icon))
                originalGame.Icon = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "gw2.png");
            originalGame.PlayAction = new GuildWars2PlayAction(this, settings);
            originalGame.PluginId = Plugin.PluginId;
            return originalGame;
        }
    }
}
