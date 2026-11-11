using Classic.Windows;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace BZSD12A
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void LoginUser(object sender, StartupEventArgs e)
        {
            Login login = new();
            Application.Current.MainWindow = login;
            Menu menu = new Menu();
            login.ShowDialog();
        }
    }

}
