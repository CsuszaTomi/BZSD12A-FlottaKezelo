using MySql.Data.MySqlClient;
using BZSD12A.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BZSD12A.Services
{
    internal class MaintenanceService
    {
        private readonly MySqlConnection _conn;

        public MaintenanceService(MySqlConnection conn)
        {
            _conn = conn;
        }

        public void GetMaintenance()
        {
            try
            {
                List<Maintenance> maintenance = new List<Maintenance>();
                string command = "SELECT * FROM maintenance";
                using (MySqlCommand cmd = new MySqlCommand(command, _conn))
                {
                    _conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Maintenance maintenance1 = new Maintenance
                            {
                                Id = reader.GetInt32("ID"),
                                CarID = Convert.ToInt32(reader.GetString("CarID")),
                                ServiceDate = reader.GetDateTime("ServiceDate"),
                                Description = reader.GetString("Description"),
                                Cost = reader.GetInt32("Cost"),
                                RanKm = Convert.ToInt32(reader.GetString("RanKm"))
                            };
                            maintenance.Add(maintenance1);
                        }

                    }
                    _conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public void AddMaintenance(Maintenance newMaintenance)
        {
            try
            {
                string command = "INSERT INTO maintenance (CarID, ServiceDate, Description, Cost, RanKm) VALUES (@CarID, @ServiceDate, @Description, @Cost, @RanKm)";
                using (MySqlCommand cmd = new MySqlCommand(command, _conn))
                {
                    cmd.Parameters.AddWithValue("@CarID", newMaintenance.CarID);
                    cmd.Parameters.AddWithValue("@ServiceDate", newMaintenance.ServiceDate);
                    cmd.Parameters.AddWithValue("@Description", newMaintenance.Description);
                    cmd.Parameters.AddWithValue("@Cost", newMaintenance.Cost);
                    cmd.Parameters.AddWithValue("@RanKm", newMaintenance.RanKm);
                    _conn.Open();
                    cmd.ExecuteNonQuery();
                    _conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public void RemoveMaintenance(Maintenance newMaintenance)
        {
            try
            {
                string command = "DELETE FROM maintenance WHERE ID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(command, _conn))
                {
                    cmd.Parameters.AddWithValue("@ID", newMaintenance.Id);
                    _conn.Open();
                    cmd.ExecuteNonQuery();
                    _conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        public void ModifyMaintenance(Maintenance newMaintenance)
        {
            try
            {
                string command = "UPDATE maintenance SET CarID = @CarID, ServiceDate = @ServiceDate, Description = @Description, Cost = @Cost, RanKm = @RanKm WHERE ID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(command, _conn))
                {
                    cmd.Parameters.AddWithValue("@CarID", newMaintenance.CarID);
                    cmd.Parameters.AddWithValue("@ServiceDate", newMaintenance.ServiceDate);
                    cmd.Parameters.AddWithValue("@Description", newMaintenance.Description);
                    cmd.Parameters.AddWithValue("@Cost", newMaintenance.Cost);
                    cmd.Parameters.AddWithValue("@RanKm", newMaintenance.RanKm);
                    cmd.Parameters.AddWithValue("@ID", newMaintenance.Id);
                    _conn.Open();
                    cmd.ExecuteNonQuery();
                    _conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
