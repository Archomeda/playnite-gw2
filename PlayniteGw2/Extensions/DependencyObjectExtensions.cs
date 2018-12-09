using System.Windows;
using System.Windows.Media;

namespace PlayniteGw2.Extensions
{
    internal static class DependencyObjectExtensions
    {
        public static T GetVisualParent<T>(this DependencyObject dependencyObject) where T : class
        {
            var parent = dependencyObject;
            while (!(parent is T || parent == null))
                parent = VisualTreeHelper.GetParent(parent);
            return parent as T;
        }
    }
}
