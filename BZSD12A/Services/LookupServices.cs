using BZSD12A.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace BZSD12A.Services
{
    /// <summary>A ComboBox-ok feltöltéséhez szükséges listák (gyártó, modell, felhasználó).</summary>
    internal class LookupServices
    {
        private readonly MySqlConnection _conn;

        public LookupServices(MySqlConnection conn)
        {
            _conn = conn;
        }

        public List<Brand> GetBrands()
        {
            List<Brand> list = new List<Brand>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT ID, BrandName FROM manufacturers ORDER BY BrandName", _conn);
                if (_conn.State != ConnectionState.Open) _conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Brand
                        {
                            Id = reader.GetInt32("ID"),
                            BrandName = reader.GetString("BrandName")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
            return list;
        }

        public List<CarModel> GetModelsByBrand(string brandName)
        {
            List<CarModel> list = new List<CarModel>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT ID, ModelName, ModelMaker FROM models WHERE ModelMaker = @Brand ORDER BY ModelName", _conn);
                cmd.Parameters.AddWithValue("@Brand", brandName);
                if (_conn.State != ConnectionState.Open) _conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CarModel
                        {
                            Id = reader.GetInt32("ID"),
                            ModelName = reader.GetString("ModelName"),
                            ModelMaker = reader.GetString("ModelMaker")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
            return list;
        }

        public List<UserLookup> GetUsers()
        {
            List<UserLookup> list = new List<UserLookup>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT ID, Name FROM user ORDER BY Name", _conn);
                if (_conn.State != ConnectionState.Open) _conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UserLookup
                        {
                            Id = reader.GetInt32("ID"),
                            Name = reader.GetString("Name")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (_conn.State == ConnectionState.Open) _conn.Close();
            }
            return list;
        }
    }
}