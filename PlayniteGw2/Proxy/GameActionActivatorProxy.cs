using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Playnite.SDK.Models;

namespace PlayniteGw2.Proxy
{
    internal static class GameActionActivatorProxy
    {
        private const string TypeName = "Playnite.GameActionActivator, Playnite";
        private static readonly Type TypeObject;
        private static readonly MethodInfo ActivateActionProxy;
        private static readonly MethodInfo GetGameActionEmulatorConfigProxy;

        static GameActionActivatorProxy()
        {
            TypeObject = Type.GetType(TypeName);
            if (TypeObject == null)
                throw new InvalidOperationException($"Type {TypeName} could not be found, cannot proceed.");
            ActivateActionProxy = TypeObject.GetMethod(nameof(ActivateAction), BindingFlags.Static | BindingFlags.Public);
            GetGameActionEmulatorConfigProxy = TypeObject.GetMethod(nameof(GetGameActionEmulatorConfig), BindingFlags.Static | BindingFlags.Public);
        }

        public static Process ActivateAction(GameAction action) =>
            (Process)ActivateActionProxy.Invoke(null, new object[] { action });

        public static Process ActivateAction(GameAction action, Game gameData) =>
            (Process)ActivateActionProxy.Invoke(null, new object[] { action, gameData });

        public static Process ActivateAction(GameAction action, Game gameData, EmulatorProfile config) =>
            (Process)ActivateActionProxy.Invoke(null, new object[] { action, gameData, config });

        public static EmulatorProfile GetGameActionEmulatorConfig(GameAction action, List<Emulator> emulators) =>
            (EmulatorProfile)GetGameActionEmulatorConfigProxy.Invoke(null, new object[] { action, emulators });
    }
}
