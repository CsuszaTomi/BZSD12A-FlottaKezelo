using BZSD12A.Services;
using System.Windows;
using System.Windows.Controls;
using MaintenanceModel = BZSD12A.Models.Maintenance;

namespace Classic.Windows
{
    public partial class Maintenance : Window
    {
        static List<MaintenanceModel> maintenanceRecords = new();
        static DatabaseService db = new DatabaseService();
        static MaintenanceService service = new MaintenanceService(db.CreateConnection());

        public Maintenance()
        {
            InitializeComponent();
            LoadMaintenanceRecords();
        }

        private void LoadMaintenanceRecords()
        {
            try
            {
                maintenanceRecords = service.GetMaintenance();
                dgMaintenanceRecords.ItemsSource = null;
                dgMaintenanceRecords.ItemsSource = maintenanceRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanInputFields()
        {
            tbxId.Clear();
            tbxCarID.Clear();
            tbxServiceDate.Clear();
            tbxServiceKm.Clear();
            tbxServiceDescription.Clear();
            tbxServiceCost.Clear();
            dgMaintenanceRecords.SelectedIndex = -1;
        }

        private void dgMaintenanceRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var m = dgMaintenanceRecords.SelectedItem as MaintenanceModel;
            tbxId.Text = m?.Id.ToString() ?? "";
            tbxCarID.Text = m?.CarID.ToString() ?? "";
            tbxServiceDate.Text = m?.ServiceDate.ToString("yyyy.MM.dd") ?? "";
            tbxServiceKm.Text = m?.RanKm.ToString() ?? "";
            tbxServiceDescription.Text = m?.Description ?? "";
            tbxServiceCost.Text = m?.Cost.ToString() ?? "";
        }

        /// Beolvassa és validálja a mezőket; hiba esetén null-t ad.
        private MaintenanceModel? ReadInput()
        {
            if (!int.TryParse(tbxCarID.Text, out int carId) ||
                !DateTime.TryParse(tbxServiceDate.Text, out DateTime date) ||
                !int.TryParse(tbxServiceKm.Text, out int km) ||
                !int.TryParse(tbxServiceCost.Text, out int cost) ||
                string.IsNullOrWhiteSpace(tbxServiceDescription.Text))
            {
                MessageBox.Show("Minden mezőt helyesen kell kitölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return new MaintenanceModel
            {
                CarID = carId,
                ServiceDate = date,
                RanKm = km,
                Cost = cost,
                Description = tbxServiceDescription.Text
            };
        }

        private void Run(Action action, string successMessage)
        {
            try
            {
                action();
                MessageBox.Show(successMessage, "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadMaintenanceRecords();
                CleanInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var m = ReadInput();
            if (m == null) return;
            Run(() => service.AddMaintenance(m), "Karbantartás hozzáadva!");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgMaintenanceRecords.SelectedItem as MaintenanceModel;
            if (selected == null)
            {
                MessageBox.Show("Nincs kiválasztott rekord!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var m = ReadInput();
            if (m == null) return;
            m.Id = selected.Id;
            Run(() => service.ModifyMaintenance(m), "Karbantartás frissítve!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgMaintenanceRecords.SelectedItem as MaintenanceModel;
            if (selected == null)
            {
                MessageBox.Show("Nincs kiválasztott rekord!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Run(() => service.RemoveMaintenance(selected), "Karbantartás törölve!");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) => CleanInputFields();
    }
}