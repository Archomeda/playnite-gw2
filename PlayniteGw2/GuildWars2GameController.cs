using System;
using System.IO;
using System.Linq;
using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Models;
using PlayniteGw2.Proxy;
using static System.Environment;

namespace PlayniteGw2
{
    internal class GuildWars2GameController : GenericGameControllerProxy
    {
        private readonly IPlayniteAPI api;
        private readonly Settings settings;

        public GuildWars2GameController(IPlayniteAPI api, Settings settings, Game game) : base(game)
        {
            this.settings = settings;
            this.api = api;
        }

        public override void Play()
        {
            if (this.Game.PlayAction == null)
                throw new InvalidOperationException("Cannot start game without play action");

            var accountData = this.settings.GuildWars2Accounts.FirstOrDefault(a => a.InternalId.ToString() == this.Game.GameId);

            string appData = Path.Combine(GetFolderPath(SpecialFolder.ApplicationData), "Guild Wars 2");
            string localDat = Path.Combine(appData, "Local.dat");
            string localDatBackup = Path.Combine(appData, "Local.dat._bak");
            string sourceDat = Path.Combine(appData, $"Local.dat.{accountData.Id}");

            if (File.Exists(localDatBackup))
                File.Delete(localDatBackup);
            if (File.Exists(localDat))
                File.Move(localDat, localDatBackup);
            if (File.Exists(sourceDat))
                File.Copy(sourceDat, localDat);
            if (File.Exists(localDatBackup))
                File.Delete(localDatBackup);

            this.Stopped += this.GuildWars2GameController_Stopped;
            base.Play();
        }

        private void GuildWars2GameController_Stopped(object sender, GameControllerEventArgs controller)
        {
            var accountData = this.settings.GuildWars2Accounts.FirstOrDefault(a => a.InternalId.ToString() == this.Game.GameId);

            string appData = Path.Combine(GetFolderPath(SpecialFolder.ApplicationData), "Guild Wars 2");
            string localDat = Path.Combine(appData, "Local.dat");
            string sourceDat = Path.Combine(appData, $"Local.dat.{accountData.Id}");
            string sourceDatBackup = Path.Combine(appData, $"Local.dat.{accountData.Id}._bak");

            if (File.Exists(sourceDatBackup))
                File.Delete(sourceDatBackup);
            if (File.Exists(sourceDat))
                File.Move(sourceDat, sourceDatBackup);
            File.Copy(localDat, sourceDat);
            if (File.Exists(sourceDatBackup))
                File.Delete(sourceDatBackup);

            this.Stopped -= this.GuildWars2GameController_Stopped;
        }

        public override void Install()
        {
            // Method intentionally left empty.
        }

        public override void Uninstall()
        {
            this.Game.IsUninstalling = false;
            this.api.Dialogs.ShowErrorMessage("This game cannot be uninstalled in Playnite." + NewLine +
                "Removing a Guild Wars 2 account can be done in the plugin settings." + NewLine +
                "Removing Guild Wars 2 entirely should be done in the Windows Control Panel.", "Removing Guild Wars 2");
        }
    }
}
