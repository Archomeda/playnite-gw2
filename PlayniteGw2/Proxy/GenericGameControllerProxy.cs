using System;
using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Models;

namespace PlayniteGw2.Proxy
{
    internal class GenericGameControllerProxy : IGameController
    {
        private const string TypeName = "Playnite.Controllers.GenericGameController, Playnite";
        private static readonly Type TypeObject;

        private readonly IGameController genericGameController;

        static GenericGameControllerProxy()
        {
            TypeObject = Type.GetType(TypeName);
            if (TypeObject == null)
                throw new InvalidOperationException($"Type {TypeName} could not be found, cannot proceed.");
        }

        public GenericGameControllerProxy(Game game)
        {
            this.genericGameController = (IGameController)Activator.CreateInstance(TypeObject, AppProxy.Database, game);
        }

        public bool IsGameRunning => this.genericGameController.IsGameRunning;

        public Game Game => this.genericGameController.Game;

        public event GameControllerEventHandler Starting
        {
            add => this.genericGameController.Starting += value;
            remove => this.genericGameController.Starting -= value;
        }

        public event GameControllerEventHandler Started
        {
            add => this.genericGameController.Started += value;
            remove => this.genericGameController.Started -= value;
        }

        public event GameControllerEventHandler Stopped
        {
            add => this.genericGameController.Stopped += value;
            remove => this.genericGameController.Stopped -= value;
        }

        public event GameControllerEventHandler Uninstalled
        {
            add => this.genericGameController.Uninstalled += value;
            remove => this.genericGameController.Uninstalled -= value;
        }

        public event GameControllerEventHandler Installed
        {
            add => this.genericGameController.Installed += value;
            remove => this.genericGameController.Installed -= value;
        }

        public virtual void Play()
        {
            this.genericGameController.Play();
        }

        public virtual void Install()
        {
            this.genericGameController.Install();
        }

        public virtual void Uninstall()
        {
            this.genericGameController.Uninstall();
        }

        public virtual void Dispose()
        {
            this.genericGameController.Dispose();
        }
    }
}
