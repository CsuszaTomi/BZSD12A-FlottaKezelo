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

using BZSD12A.Models;
using BZSD12A.Services;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Classic.Windows
{
    /// <summary>
    /// Interaction logic for Autok.xaml
    /// A CRUD műveletek a Services/CarServices.cs-ben vannak, ez a fájl csak az ablak logikája.
    /// </summary>
    public partial class Autok : Window
    {
        private readonly CarServices carServices;
        private readonly LookupServices lookupServices;
        private List<Car> cars = new List<Car>();

        public Autok()
        {
            InitializeComponent();

            DatabaseService db = new DatabaseService();
            carServices = new CarServices(db.CreateConnection());
            lookupServices = new LookupServices(db.CreateConnection());

            cbxFuel.ItemsSource = new List<string> { "Benzin", "Dízel", "Hibrid", "Elektromos" };
            cbxBrand.ItemsSource = lookupServices.GetBrands();
            cbxUser.ItemsSource = lookupServices.GetUsers();

            LoadCars();
        }

        private void LoadCars()
        {
            cars = carServices.GetCars();
            dgCars.ItemsSource = null;
            dgCars.ItemsSource = cars;
        }

        // Gyártó váltásakor csak az adott gyártó modelljeit mutatjuk
        private void cbxBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxBrand.SelectedItem is Brand brand)
                cbxModel.ItemsSource = lookupServices.GetModelsByBrand(brand.BrandName);
            else
                cbxModel.ItemsSource = null;
        }

        private void dgCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCars.SelectedItem is not Car car) return;

            tbxId.Text = car.Id.ToString();
            tbxPlate.Text = car.PlateNumber;
            tbxChassis.Text = car.ChassisNumber;
            dpCarYear.SelectedDate = car.CarYear;
            cbxBrand.SelectedValue = car.BrandId;   // ez betölti a modelleket is
            cbxModel.SelectedValue = car.ModelId;
            cbxUser.SelectedValue = car.UserId;
            tbxConsumption.Text = car.Consumption.Split(' ')[0];   // "5.4 l/100km" -> "5.4"
            tbxKm.Text = car.RanKm;
            cbxFuel.SelectedItem = car.FuelType;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!TryReadForm(out Car newCar)) return;

            if (carServices.PlateNumberExists(newCar.PlateNumber))
            {
                MessageBox.Show("Már létezik ilyen rendszámú autó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (carServices.AddCar(newCar))
            {
                CleanInputFields();
                LoadCars();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgCars.SelectedItem is not Car selected)
            {
                MessageBox.Show("Nincs kiválasztott autó a módosításhoz!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!TryReadForm(out Car updatedCar)) return;
            updatedCar.Id = selected.Id;

            if (carServices.PlateNumberExists(updatedCar.PlateNumber, updatedCar.Id))
            {
                MessageBox.Show("Már létezik ilyen rendszámú autó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (carServices.ModifyCar(updatedCar))
            {
                LoadCars();
                CleanInputFields();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCars.SelectedItem is not Car selected)
            {
                MessageBox.Show("Nincs kiválasztott autó a törléshez!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show($"Biztosan törölni szeretné az autót: {selected.PlateNumber}?", "Megerősítés",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (carServices.DeleteCar(selected.Id))
                {
                    LoadCars();
                    CleanInputFields();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CleanInputFields();
            dgCars.SelectedItem = null;
        }

        private void CleanInputFields()
        {
            tbxId.Clear();
            tbxPlate.Clear();
            tbxChassis.Clear();
            dpCarYear.SelectedDate = null;
            cbxBrand.SelectedIndex = -1;
            cbxModel.SelectedIndex = -1;
            cbxUser.SelectedIndex = -1;
            tbxConsumption.Clear();
            tbxKm.Clear();
            cbxFuel.SelectedIndex = -1;
        }

        /// <summary>Beolvassa és ellenőrzi az űrlapot. Hibánál üzenetet ír ki és false-t ad vissza.</summary>
        private bool TryReadForm(out Car car)
        {
            car = new Car();

            if (string.IsNullOrWhiteSpace(tbxPlate.Text) || string.IsNullOrWhiteSpace(tbxChassis.Text))
                return Warn("A rendszám és az alvázszám megadása kötelező!");

            if (cbxBrand.SelectedValue is not int brandId)
                return Warn("Válassz gyártót!");

            if (cbxModel.SelectedValue is not int modelId)
                return Warn("Válassz modellt!");

            if (cbxUser.SelectedValue is not int userId)
                return Warn("Válassz felhasználót!");

            if (dpCarYear.SelectedDate is not DateTime carYear)
                return Warn("Add meg a gyártási dátumot!");

            if (!decimal.TryParse(tbxConsumption.Text.Trim().Replace(',', '.'), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out decimal consumption) || consumption <= 0)
                return Warn("A fogyasztás pozitív szám legyen (pl. 5.4)!");

            if (!int.TryParse(tbxKm.Text.Trim(), out int km) || km < 0)
                return Warn("A futott km nemnegatív egész szám legyen!");

            if (cbxFuel.SelectedItem is not string fuel)
                return Warn("Válassz üzemanyag típust!");

            car = new Car
            {
                PlateNumber = tbxPlate.Text.Trim().ToUpper(),
                ChassisNumber = tbxChassis.Text.Trim().ToUpper(),
                BrandId = brandId,
                ModelId = modelId,
                UserId = userId,
                CarYear = carYear,
                Consumption = $"{consumption.ToString(CultureInfo.InvariantCulture)} l/100km",
                RanKm = km.ToString(),
                FuelType = fuel
            };
            return true;
        }

        private static bool Warn(string message)
        {
            MessageBox.Show(message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
    }
}
