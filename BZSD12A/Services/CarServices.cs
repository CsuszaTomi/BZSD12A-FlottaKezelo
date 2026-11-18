using System.Data;
using System.Windows;
using BZSD12A.Models;
using MySql.Data.MySqlClient;

namespace BZSD12A.Services
{
    internal class CarServices
    {
        private readonly MySqlConnection _conn;

        public CarServices(MySqlConnection conn)
        {
            _conn = conn;
        }

        // READ
        public List<Car> GetCars()
        {
            try
            {
                List<Car> cars = new List<Car>();
                string sql = @"SELECT c.ID, c.PlateNumb, c.ChassNum, c.ModelId, c.UserID, c.CarYear,
                                      c.Consumption, c.RanKM, c.FuelType, c.BrandId,
                                      m.BrandName, md.ModelName, u.Name AS UserName
                               FROM car c
                               JOIN manufacturers m ON m.ID = c.BrandId
                               JOIN models md ON md.ID = c.ModelId
                               JOIN user u ON u.ID = c.UserID
                               ORDER BY c.ID";
                MySqlCommand cmd = new MySqlCommand(sql, _conn);

                if (_conn.State != ConnectionState.Open) _conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cars.Add(new Car
                        {
                            Id = reader.GetInt32("ID"),
                            PlateNumber = reader.GetString("PlateNumb"),
                            ChassisNumber = reader.GetString("ChassNum"),
                            ModelId = reader.GetInt32("ModelId"),
                            UserId = reader.GetInt32("UserID"),
                            CarYear = reader.GetDateTime("CarYear"),
                            Consumption = reader.GetString("Consumption"),
                            RanKm = reader.GetString("RanKM"),
                            FuelType = reader.GetString("FuelType"),
                            BrandId = reader.GetInt32("BrandId"),
                            BrandName = reader.GetString("BrandName"),
                            ModelName = reader.GetString("ModelName"),
                            UserName = reader.GetString("UserName")
                        });
                    }
                }
                return cars;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Car>();
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
        }

        // CREATE
        public bool AddCar(Car newCar)
        {
            try
            {
                string sql = @"INSERT INTO car (PlateNumb, ChassNum, ModelId, UserID, CarYear, Consumption, RanKM, FuelType, BrandId)
                               VALUES (@Plate, @Chassis, @ModelId, @UserId, @CarYear, @Consumption, @RanKm, @FuelType, @BrandId)";
                MySqlCommand cmd = new MySqlCommand(sql, _conn);
                cmd.Parameters.AddWithValue("@Plate", newCar.PlateNumber);
                cmd.Parameters.AddWithValue("@Chassis", newCar.ChassisNumber);
                cmd.Parameters.AddWithValue("@ModelId", newCar.ModelId);
                cmd.Parameters.AddWithValue("@UserId", newCar.UserId);
                cmd.Parameters.AddWithValue("@CarYear", newCar.CarYear.Date);
                cmd.Parameters.AddWithValue("@Consumption", newCar.Consumption);
                cmd.Parameters.AddWithValue("@RanKm", newCar.RanKm);
                cmd.Parameters.AddWithValue("@FuelType", newCar.FuelType);
                cmd.Parameters.AddWithValue("@BrandId", newCar.BrandId);

                if (_conn.State != ConnectionState.Open) _conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
        }

        // UPDATE
        public bool ModifyCar(Car modifiedCar)
        {
            try
            {
                string sql = @"UPDATE car SET PlateNumb = @Plate, ChassNum = @Chassis, ModelId = @ModelId,
                                      UserID = @UserId, CarYear = @CarYear, Consumption = @Consumption,
                                      RanKM = @RanKm, FuelType = @FuelType, BrandId = @BrandId
                               WHERE ID = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, _conn);
                cmd.Parameters.AddWithValue("@Plate", modifiedCar.PlateNumber);
                cmd.Parameters.AddWithValue("@Chassis", modifiedCar.ChassisNumber);
                cmd.Parameters.AddWithValue("@ModelId", modifiedCar.ModelId);
                cmd.Parameters.AddWithValue("@UserId", modifiedCar.UserId);
                cmd.Parameters.AddWithValue("@CarYear", modifiedCar.CarYear.Date);
                cmd.Parameters.AddWithValue("@Consumption", modifiedCar.Consumption);
                cmd.Parameters.AddWithValue("@RanKm", modifiedCar.RanKm);
                cmd.Parameters.AddWithValue("@FuelType", modifiedCar.FuelType);
                cmd.Parameters.AddWithValue("@BrandId", modifiedCar.BrandId);
                cmd.Parameters.AddWithValue("@Id", modifiedCar.Id);

                if (_conn.State != ConnectionState.Open) _conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
        }

        // DELETE
        public bool DeleteCar(int carId)
        {
            try
            {
                string sql = "DELETE FROM car WHERE ID = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, _conn);
                cmd.Parameters.AddWithValue("@Id", carId);

                if (_conn.State != ConnectionState.Open) _conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
        }

        // Segéd: létezik-e már ilyen rendszám (módosításnál a saját sort kihagyja)
        public bool PlateNumberExists(string plateNumber, int excludeId = 0)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM car WHERE PlateNumb = @Plate AND ID <> @Id";
                MySqlCommand cmd = new MySqlCommand(sql, _conn);
                cmd.Parameters.AddWithValue("@Plate", plateNumber);
                cmd.Parameters.AddWithValue("@Id", excludeId);

                if (_conn.State != ConnectionState.Open) _conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true; // hiba esetén inkább ne engedjünk mentést
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
        }
    }
}