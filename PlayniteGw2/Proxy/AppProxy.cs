using System;
using System.Reflection;

namespace PlayniteGw2.Proxy
{
    internal static class AppProxy
    {
        private const string TypeName = "PlayniteUI.App, PlayniteUI";
        private static readonly Type TypeObject;
        private static readonly PropertyInfo DatabaseProxy;

        static AppProxy()
        {
            TypeObject = Type.GetType(TypeName);
            if (TypeObject == null)
                throw new InvalidOperationException($"Type {TypeName} could not be found, cannot proceed.");
            DatabaseProxy = TypeObject.GetProperty(nameof(Database), BindingFlags.Static | BindingFlags.Public);
        }

        public static object Database =>
            DatabaseProxy.GetValue(null);
    }
}
