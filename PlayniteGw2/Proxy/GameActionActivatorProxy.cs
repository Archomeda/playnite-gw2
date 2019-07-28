using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Playnite.SDK.Models;

namespace PlayniteGw2.Proxy
{
    internal static class GameActionActivatorProxy
    {
        private const string TYPE_NAME = "Playnite.GameActionActivator, Playnite";
        private static readonly Type typeObject = Type.GetType(TYPE_NAME);
        private static readonly MethodInfo activateActionProxy = typeObject.GetMethod(nameof(ActivateAction), BindingFlags.Static | BindingFlags.Public);
        private static readonly MethodInfo getGameActionEmulatorConfigProxy = typeObject.GetMethod(nameof(GetGameActionEmulatorConfig), BindingFlags.Static | BindingFlags.Public);

        public static Process ActivateAction(GameAction action) =>
            (Process)activateActionProxy.Invoke(null, new object[] { action });

        public static Process ActivateAction(GameAction action, Game gameData) =>
            (Process)activateActionProxy.Invoke(null, new object[] { action, gameData });

        public static Process ActivateAction(GameAction action, Game gameData, EmulatorProfile config) =>
            (Process)activateActionProxy.Invoke(null, new object[] { action, gameData, config });

        public static EmulatorProfile GetGameActionEmulatorConfig(GameAction action, List<Emulator> emulators) =>
            (EmulatorProfile)getGameActionEmulatorConfigProxy.Invoke(null, new object[] { action, emulators });
    }
}
