using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Playnite.SDK;
using PlayniteGw2.Extensions;

namespace PlayniteGw2
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private IPlayniteAPI api;

        public SettingsView(IPlayniteAPI api)
        {
            this.InitializeComponent();
            this.api = api;
        }

        protected Settings Settings => (Settings)this.DataContext;

        private GuildWars2AccountData GetAccount(DependencyObject elementInItemsControl)
        {
            var item = elementInItemsControl.GetVisualParent<ContentPresenter>();
            var itemControl = item.GetVisualParent<ItemsControl>();
            int index = itemControl.Items.IndexOf(item.DataContext);
            return this.Settings.GuildWars2Accounts.ElementAt(index);
        }

        private void ButtonEditDefaultPath_Click(object sender, RoutedEventArgs e)
        {
            string newPath = this.api.Dialogs.SelectFile("Executable|*.exe");
            if (!string.IsNullOrEmpty(newPath))
                this.Settings.DefaultPath = newPath;
        }

        private void ButtonEditExecutablePath_Click(object sender, RoutedEventArgs e)
        {
            var account = this.GetAccount((DependencyObject)e.Source);
            string newPath = this.api.Dialogs.SelectFile("Executable|*.exe");
            if (!string.IsNullOrEmpty(newPath))
                account.ExecutablePath = newPath;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Settings.GuildWars2Accounts.Add(new GuildWars2AccountData());
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var account = this.GetAccount((DependencyObject)e.Source);
            this.Settings.GuildWars2Accounts.Remove(account);
        }
    }
}
