using System;
using System.Reflection;

namespace PlayniteGw2.Proxy
{
    internal static class PlayniteApplicationProxy
    {
        private const string TYPE_NAME = "Playnite.PlayniteApplication, Playnite";
        private static readonly Type typeObject = Type.GetType(TYPE_NAME);
        private static readonly PropertyInfo currentProxy = typeObject.GetProperty("Current", BindingFlags.Static | BindingFlags.Public);
        private static readonly PropertyInfo databaseProxy = typeObject.GetProperty(nameof(Database), BindingFlags.Instance | BindingFlags.Public);

        public static object Database =>
            databaseProxy.GetValue(currentProxy.GetValue(null));
    }
}
