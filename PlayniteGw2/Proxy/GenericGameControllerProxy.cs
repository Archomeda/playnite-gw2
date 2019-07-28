using System;
using System.Diagnostics.CodeAnalysis;
using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Models;

namespace PlayniteGw2.Proxy
{
    [SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "Proxy")]
    internal class GenericGameControllerProxy : IGameController
    {
        private const string TYPE_NAME = "Playnite.Controllers.GenericGameController, Playnite";
        private static readonly Type typeObject = Type.GetType(TYPE_NAME);

        private readonly IGameController genericGameController;

        public GenericGameControllerProxy(Game game)
        {
            this.genericGameController = (IGameController)Activator.CreateInstance(typeObject, PlayniteApplicationProxy.Database, game);
        }

        public bool IsGameRunning => this.genericGameController.IsGameRunning;

        public Game Game => this.genericGameController.Game;

        public event EventHandler<GameControllerEventArgs> Starting
        {
            add => this.genericGameController.Starting += value;
            remove => this.genericGameController.Starting -= value;
        }

        public event EventHandler<GameControllerEventArgs> Started
        {
            add => this.genericGameController.Started += value;
            remove => this.genericGameController.Started -= value;
        }

        public event EventHandler<GameControllerEventArgs> Stopped
        {
            add => this.genericGameController.Stopped += value;
            remove => this.genericGameController.Stopped -= value;
        }

        public event EventHandler<GameControllerEventArgs> Uninstalled
        {
            add => this.genericGameController.Uninstalled += value;
            remove => this.genericGameController.Uninstalled -= value;
        }

        public event EventHandler<GameInstalledEventArgs> Installed
        {
            add => this.genericGameController.Installed += value;
            remove => this.genericGameController.Installed -= value;
        }

        public virtual void Play() =>
            this.genericGameController.Play();

        public virtual void Install() =>
            this.genericGameController.Install();

        public virtual void Uninstall() =>
            this.genericGameController.Uninstall();

        public virtual void Dispose() =>
            this.genericGameController.Dispose();
    }
}
