using BZSD12A.Models;
using BZSD12A.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Classic.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnBejelentkezes_Click(object sender, RoutedEventArgs e)
        {
            //if (txbUserName.Text == "a" && pwbPassword.Password == "a")
            //{
            //    Menu menu = new Menu();
            //    menu.Show();
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            if (string.IsNullOrWhiteSpace(txbUserName.Text) || string.IsNullOrWhiteSpace(pwbPassword.Password))
            {
                MessageBox.Show("Kérem, töltse ki az összes mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DatabaseService db = new DatabaseService();
            MySqlConnection conn = db.CreateConnection();
            UserServices userServices = new UserServices(conn);
            List<User> users = userServices.GetUsers();
            User user = users.FirstOrDefault(user => user.Name == txbUserName.Text && user.Password == pwbPassword.Password);
            if (user != null && new UserServices(conn).Login(user.Name,user.Password) && user.Role == "admin")
            {
                Menu menu = new Menu();
                menu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
