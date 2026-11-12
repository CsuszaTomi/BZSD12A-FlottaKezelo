using Classic.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Classic
{
    public partial class Menu : Window
    {
        public static bool bejelentkezve = false;
        public Menu()
        {
            InitializeComponent();
            bejelentkezve = true;
        }

        private void Exit(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan ki akarsz lépni a programból?", "Kilépés", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void AutokCRUD(object sender, RoutedEventArgs e)
        {
        }

        private void UsersCrud(object sender, RoutedEventArgs e)
        {
            Felhasznalok felhasznalok = new Felhasznalok();
            felhasznalok.Show();
        }
    }
}