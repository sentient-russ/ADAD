using adad.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MySql.Data.MySqlClient;

namespace adad.Services
{
    public class DataAccess
    {
        public string? connectionString;
        public DataAccess()
        {
            connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
        }
        /*
         * Gets all sites from db
         * @return returns a list of all sites
         */
        public List<SiteModel> GetSites()
        {
            List<SiteModel> foundSitesList = new List<SiteModel>();
            try
            {
                MySqlConnection conn1 = new MySqlConnection(connectionString);
                conn1.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM adad.SiteModel", conn1);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                
                while (reader1.Read())
                {
                    SiteModel foundSite = new SiteModel();
                    if (reader1.GetValue(1).Equals(DBNull.Value)) { } else { foundSite.idSite = reader1.GetString(0); }
                    foundSite.site_name = reader1.GetString(1);
                    foundSite.country = reader1.GetString(2);
                    foundSite.country_id = reader1.GetString(3);
                    foundSite.city = reader1.GetString(4);
                    foundSite.latitude = reader1.GetString(5);
                    foundSite.longitude = reader1.GetString(6);
                    foundSite.contact_name = reader1.GetString(7);
                    foundSite.country_code = reader1.GetString(8);
                    foundSite.phone = reader1.GetString(9);
                    foundSite.sms = reader1.GetString(10);
                    foundSite.email = reader1.GetString(11);
                    foundSite.threat = reader1.GetString(12);
                    foundSite.severity = reader1.GetString(13);

                    foundSitesList.Add(foundSite);
                }

                reader1.Close();
                conn1.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return foundSitesList;
        }
        /*
         * Gets a single site from db
         * @param idSiteIn the site to be looked up
         * @return returns a single site
         */
            public SiteModel GetSite(string idSiteIn)
            {
                SiteModel foundSite = new SiteModel();
            try
                {
                    MySqlConnection conn1 = new MySqlConnection(connectionString);
                    conn1.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM adad.SiteModel WHERE idSite LIKE @idSiteIn", conn1);
                    cmd1.Parameters.AddWithValue("@idPostModel", "%" + idSiteIn + "%");
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    
                    while (reader1.Read())
                    {
                        if (reader1.GetValue(1).Equals(DBNull.Value)) { } else { foundSite.idSite = reader1.GetString(0).ToString(); }
                        foundSite.site_name = reader1.GetString(1);
                        foundSite.country = reader1.GetString(2);
                        foundSite.country_id = reader1.GetString(3);
                        foundSite.city = reader1.GetString(4);
                        foundSite.latitude = reader1.GetString(5);
                        foundSite.longitude = reader1.GetString(6);
                        foundSite.contact_name = reader1.GetString(7);
                        foundSite.country_code = reader1.GetString(8);
                        foundSite.phone = reader1.GetString(9);
                        foundSite.sms = reader1.GetString(10);
                        foundSite.email = reader1.GetString(11);
                        foundSite.threat = reader1.GetString(12);
                        foundSite.severity = reader1.GetString(13);
                }

                    reader1.Close();
                    conn1.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return foundSite;
            }

        public SiteModel InsertSiteDB(SiteModel siteIn)
        {
            string connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
            //connectionString += "";
            try
            {
                MySqlConnection conn1 = new MySqlConnection(connectionString);
 
                string command = "INSERT INTO adad.SiteModel (idSite, site_name, country, country_id, city, latitude, longitude, contact_name, country_code, phone, sms, email, threat, severity) VALUES (@idSite, @site_name, @country, @country_id, @city, @latitude, @longitude, @contact_name, @country_code, @phone, @sms, @email, @threat, @severity)";
                conn1.Open();
                MySqlCommand cmd1 = new MySqlCommand(command, conn1);
                
                cmd1.Parameters.AddWithValue("@idSite", siteIn.idSite);
                cmd1.Parameters.AddWithValue("@site_name", siteIn.site_name);
                cmd1.Parameters.AddWithValue("@country", siteIn.country);
                cmd1.Parameters.AddWithValue("@country_id", siteIn.country_id);
                cmd1.Parameters.AddWithValue("@city", siteIn.city);
                cmd1.Parameters.AddWithValue("@latitude", siteIn.latitude);
                cmd1.Parameters.AddWithValue("@longitude", siteIn.longitude);
                cmd1.Parameters.AddWithValue("@contact_name", siteIn.contact_name);
                cmd1.Parameters.AddWithValue("@country_code", siteIn.country_code);
                cmd1.Parameters.AddWithValue("@sms", siteIn.sms);
                cmd1.Parameters.AddWithValue("@email", siteIn. email);
                cmd1.Parameters.AddWithValue("@phone", siteIn.phone);
                cmd1.Parameters.AddWithValue("@threat", siteIn.threat);
                cmd1.Parameters.AddWithValue("@severity", siteIn.severity);

                MySqlDataReader reader1 = cmd1.ExecuteReader();

                reader1.Close();
                conn1.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            SiteModel insertedSite = GetSite(siteIn.idSite);

            return insertedSite;
        }
        public SiteModel UpdateSiteLatLng(SiteModel siteIn)
        {
            string connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
            //connectionString += "";
            try
            {
                MySqlConnection conn1 = new MySqlConnection(connectionString);

                string command = "UPDATE adad.SiteModel SET latitude = @latitude, longitude = @longitude WHERE idSite LIKE @idSite";
                conn1.Open();
                MySqlCommand cmd1 = new MySqlCommand(command, conn1);

                cmd1.Parameters.AddWithValue("@idSite", siteIn.idSite);
                cmd1.Parameters.AddWithValue("@site_name", siteIn.site_name);
                cmd1.Parameters.AddWithValue("@country", siteIn.country);
                cmd1.Parameters.AddWithValue("@country_id", siteIn.country_id);
                cmd1.Parameters.AddWithValue("@city", siteIn.city);
                cmd1.Parameters.AddWithValue("@latitude", siteIn.latitude);
                cmd1.Parameters.AddWithValue("@longitude", siteIn.longitude);
                cmd1.Parameters.AddWithValue("@contact_name", siteIn.contact_name);
                cmd1.Parameters.AddWithValue("@country_code", siteIn.country_code);
                cmd1.Parameters.AddWithValue("@sms", siteIn.sms);
                cmd1.Parameters.AddWithValue("@phone", siteIn.phone);
                cmd1.Parameters.AddWithValue("@email", siteIn.email);
                cmd1.Parameters.AddWithValue("@threat", siteIn.threat);
                cmd1.Parameters.AddWithValue("@severity", siteIn.severity);


                MySqlDataReader reader1 = cmd1.ExecuteReader();

                reader1.Close();
                conn1.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            SiteModel insertedSite = GetSite(siteIn.idSite);

            return insertedSite;
        }
        public SiteModel UpdateSite(SiteModel siteIn)
        {
            string connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
            //connectionString += "";
            try
            {
                MySqlConnection conn1 = new MySqlConnection(connectionString);

                string command = "UPDATE adad.SiteModel SET site_name = @site_name, country = @country, country_id = @country_id, city = @city, latitude = @latitude, longitude = @longitude, " +
                                                            "contact_name = @contact_name, country_code = @country_code, sms = @sms, phone = @phone, email = @email, threat = @threat, " +
                                                            "severity = @severity, WHERE idSite LIKE @idSite";
                conn1.Open();
                MySqlCommand cmd1 = new MySqlCommand(command, conn1);

                cmd1.Parameters.AddWithValue("@idSite", siteIn.idSite);
                cmd1.Parameters.AddWithValue("@site_name", siteIn.site_name);
                cmd1.Parameters.AddWithValue("@country", siteIn.country);
                cmd1.Parameters.AddWithValue("@country_id", siteIn.country_id);
                cmd1.Parameters.AddWithValue("@city", siteIn.city);
                cmd1.Parameters.AddWithValue("@latitude", siteIn.latitude);
                cmd1.Parameters.AddWithValue("@longitude", siteIn.longitude);
                cmd1.Parameters.AddWithValue("@contact_name", siteIn.contact_name);
                cmd1.Parameters.AddWithValue("@country_code", siteIn.country_code);
                cmd1.Parameters.AddWithValue("@sms", siteIn.sms);
                cmd1.Parameters.AddWithValue("@phone", siteIn.phone);
                cmd1.Parameters.AddWithValue("@email", siteIn.email);
                cmd1.Parameters.AddWithValue("@threat", siteIn.threat);
                cmd1.Parameters.AddWithValue("@severity", siteIn.severity);


                MySqlDataReader reader1 = cmd1.ExecuteReader();

                reader1.Close();
                conn1.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            SiteModel insertedSite = GetSite(siteIn.idSite);

            return insertedSite;
        }
        public bool DeleteSite(SiteModel siteIn)
        {
            string connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
            bool result = false;
            try
            {
                MySqlConnection conn1 = new MySqlConnection(connectionString);
                string command = "DELETE FROM adad.SiteModel WHERE idSite = @idSite";
                conn1.Open();
                MySqlCommand cmd1 = new MySqlCommand(command, conn1);
                cmd1.ExecuteNonQuery();                
                conn1.Close();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
    }

}




