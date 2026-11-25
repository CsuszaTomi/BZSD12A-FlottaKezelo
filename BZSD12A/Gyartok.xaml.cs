using BZSD12A.Models;
using BZSD12A.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Classic.Windows
{
    /// <summary>
    /// Interaction logic for Gyartok.xaml
    /// </summary>
    public partial class Gyartok : Window
    {
        static List<Brand> manufacturers = new List<Brand>();
        static DatabaseService db = new DatabaseService();
        static LookupServices lookupServices = new LookupServices(db.CreateConnection());
        public Gyartok()
        {
            InitializeComponent();
            LoadManufacturers();
        }

        private void LoadManufacturers()
        {
            manufacturers.Clear();
            manufacturers = lookupServices.GetBrands();
            dgManufacturers.ItemsSource = null;
            dgManufacturers.SelectedItem = null;
            dgManufacturers.ItemsSource = manufacturers;
        }

        private void CleanInputFields()
        {
            tbxBrandName.Clear();
            tbxId.Clear();
            dgManufacturers.SelectedIndex = -1;
        }

        private void dgManufacturers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Brand? kivalasztott = dgManufacturers.SelectedItem as Brand;
            tbxId.Text = kivalasztott?.Id.ToString() ?? string.Empty;
            tbxBrandName.Text = kivalasztott?.BrandName ?? string.Empty;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbxBrandName.Text))
            {
                MessageBox.Show("A gyártó neve nem lehet üres!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (var manufacturer in manufacturers)
            {
                if (manufacturer.BrandName.Equals(tbxBrandName.Text, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Ez a gyártó már létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if(lookupServices.AddBrand(tbxBrandName.Text))
            {
                MessageBox.Show("Gyártó hozzáadva!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadManufacturers();
                CleanInputFields();
            }
            else
            {
                MessageBox.Show("Hiba történt a gyártó hozzáadásakor!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Brand? kivalasztott = dgManufacturers.SelectedItem as Brand;
            if(kivalasztott == null)
            {
                MessageBox.Show("Nincs kiválasztott gyártó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(string.IsNullOrWhiteSpace(tbxBrandName.Text))
            {
                MessageBox.Show("A gyártó neve nem lehet üres!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (var manufacturer in manufacturers)
            {
                if (manufacturer.BrandName.Equals(tbxBrandName.Text, StringComparison.OrdinalIgnoreCase) && manufacturer.Id != kivalasztott.Id)
                {
                    MessageBox.Show("Ez a gyártó már létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if(lookupServices.UpdateBrand(kivalasztott.Id, tbxBrandName.Text))
            {
                MessageBox.Show("Gyártó frissítve!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadManufacturers();
                CleanInputFields();
            }
            else
            {
                MessageBox.Show("Hiba történt a gyártó frissítésekor!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Brand? kivalasztott = dgManufacturers.SelectedItem as Brand;
            if (kivalasztott == null)
            {
                MessageBox.Show("Nincs kiválasztott gyártó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (lookupServices.DeleteBrand(kivalasztott))
            {
                MessageBox.Show("Gyártó törölve!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadManufacturers();
                CleanInputFields();
            }
            else
            {
                MessageBox.Show("Hiba történt a gyártó törlésekor!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CleanInputFields();
        }
    }
}