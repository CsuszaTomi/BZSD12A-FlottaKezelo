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
using BZSD12A.Services;
using BZSD12A.Models;
using BZSD12A.Encryption;

namespace Classic.Windows
{
    /// <summary>
    /// Interaction logic for Felhasznalok.xaml
    /// </summary>
    public partial class Felhasznalok : Window
    {
        static List<User> users = new List<User>();
        static DatabaseService db = new DatabaseService();
        static UserServices userServices = new UserServices(db.CreateConnection());
        public Felhasznalok()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            users.Clear();
            users = userServices.GetUsers();
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = users;
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User? selectedUser = dgUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                tbxName.Text = selectedUser.Name;
                tbxAge.Text = selectedUser.Age.ToString();
                cbxRole.Text = selectedUser.Role;
                tbxIdentityCardNumber.Text = selectedUser.IdentityCardNumber;
                tbxPassword.Text = selectedUser.Password;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User uj = new User
                {
                    Name = tbxName.Text,
                    Age = int.Parse(tbxAge.Text),
                    Role = cbxRole.Text,
                    IdentityCardNumber = tbxIdentityCardNumber.Text,
                    Password = tbxPassword.Text,
                };
                if (!users.Contains(uj))
                {
                    foreach (var u in users)
                    {
                        if (u.IdentityCardNumber == uj.IdentityCardNumber)
                        {
                            MessageBox.Show("Már létezik ilyen személyi számú felhasználó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    if (userServices.AddUser(uj))
                    {
                        CleanInputFields();
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a felhasználó hozzáadásakor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanInputFields()
        {
            tbxName.Clear();
            tbxAge.Clear();
            cbxRole.SelectedIndex = -1;
            tbxIdentityCardNumber.Clear();
            tbxPassword.Clear();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            User? user = dgUsers.SelectedItem as User;
            if (user != null)
            {
                User updatedUser = new User
                {
                    Id = user.Id,
                    Name = tbxName.Text,
                    Age = int.Parse(tbxAge.Text),
                    Role = cbxRole.Text,
                    IdentityCardNumber = tbxIdentityCardNumber.Text,
                    Password = tbxPassword.Text
                };
                if (updatedUser != null)
                {
                    if (userServices.ModifyUser(updatedUser))
                    {
                        LoadUsers();
                        CleanInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt a felhasználó módosításakor!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztott felhasználó a módosításhoz!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            User? user = dgUsers.SelectedItem as User;
            if (user != null)
            {
                if (MessageBox.Show($"Biztosan törölni szeretné a felhasználót: {user.Name}?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (userServices.DeleteUser(user.Id))
                    {
                        LoadUsers();
                        CleanInputFields();
                    }
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztott felhasználó a törléshez!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CleanInputFields();
            dgUsers.SelectedItem = null;
        }
    }
}