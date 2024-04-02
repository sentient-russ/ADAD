using adad.Models;
using Google.Protobuf.WellKnownTypes;
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
                    foundSite.wind_direction = reader1.GetString(14);
                    foundSite.wind_speed = reader1.GetString(15);
                    foundSite.curr_gusts = reader1.GetString(16);
                    foundSite.curr_precip = reader1.GetString(17);
                    foundSite.curr_temperature = reader1.GetString(18);
                    foundSite.curr_time = reader1.GetString(19);
                    foundSite.curr_weather_code = reader1.GetString(20);
                    foundSite.curr_wind_dir = reader1.GetString(21);
                    foundSite.curr_windspeed = reader1.GetString(22);
                    foundSite.tomorrow_gusts = reader1.GetString(23);
                    foundSite.tomorrow_high_temp = reader1.GetString(24);
                    foundSite.tomorrow_low_temp = reader1.GetString(25);
                    foundSite.tomorrow_precip = reader1.GetString(26);
                    foundSite.tomorrow_weather_code = reader1.GetString(27);
                    foundSite.tomorrow_wind_dir = reader1.GetString(28);
                    foundSite.tomorrow_windspeed = reader1.GetString(29);                  
                    

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
                    cmd1.Parameters.AddWithValue("@idSiteIn", idSiteIn);
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
                        foundSite.wind_direction = reader1.GetString(14);
                        foundSite.wind_speed = reader1.GetString(15);
                        foundSite.curr_gusts = reader1.GetString(16);
                        foundSite.curr_precip = reader1.GetString(17);
                        foundSite.curr_temperature = reader1.GetString(18);
                        foundSite.curr_time = reader1.GetString(19);
                        foundSite.curr_weather_code = reader1.GetString(20);
                        foundSite.curr_wind_dir = reader1.GetString(21);
                        foundSite.curr_windspeed = reader1.GetString(22);
                        foundSite.tomorrow_gusts = reader1.GetString(23);
                        foundSite.tomorrow_high_temp = reader1.GetString(24);
                        foundSite.tomorrow_low_temp = reader1.GetString(25);
                        foundSite.tomorrow_precip = reader1.GetString(26);
                        foundSite.tomorrow_weather_code = reader1.GetString(27);
                        foundSite.tomorrow_wind_dir = reader1.GetString(28);
                        foundSite.tomorrow_windspeed = reader1.GetString(29);
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
        public int GetSiteId()
        {
            int nextId = 0;
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
                    foundSite.wind_direction = reader1.GetString(14);
                    foundSite.wind_speed = reader1.GetString(15);
                    foundSite.curr_gusts = reader1.GetString(16);
                    foundSite.curr_precip = reader1.GetString(17);
                    foundSite.curr_temperature = reader1.GetString(18);
                    foundSite.curr_time = reader1.GetString(19);
                    foundSite.curr_weather_code = reader1.GetString(20);
                    foundSite.curr_wind_dir = reader1.GetString(21);
                    foundSite.curr_windspeed = reader1.GetString(22);
                    foundSite.tomorrow_gusts = reader1.GetString(23);
                    foundSite.tomorrow_high_temp = reader1.GetString(24);
                    foundSite.tomorrow_low_temp = reader1.GetString(25);
                    foundSite.tomorrow_precip = reader1.GetString(26);
                    foundSite.tomorrow_weather_code = reader1.GetString(27);
                    foundSite.tomorrow_wind_dir = reader1.GetString(28);
                    foundSite.tomorrow_windspeed = reader1.GetString(29);
                    foundSitesList.Add(foundSite);
                }

                reader1.Close();
                conn1.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            string lastId = foundSitesList[foundSitesList.Count - 1].idSite;
            nextId = Int32.Parse(lastId) + 1;
            return nextId;
        }

        public SiteModel InsertSiteDB(SiteModel siteIn)
        {
            string connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
            //connectionString += "";
            try
            {
                MySqlConnection conn1 = new MySqlConnection(connectionString);
 
                string command = "INSERT INTO adad.SiteModel (idSite, site_name, country, country_id, city, latitude, longitude, contact_name, country_code, phone, sms, email, threat, severity, wind_speed, wind_direction," +
                    "curr_time, curr_weather_code, curr_temperature, curr_precip, curr_windspeed, curr_gusts, curr_wind_dir, tomorrow_weather_code, tomorrow_high_temp, tomorrow_low_temp, tomorrow_precip, tomorrow_windspeed, tomorrow_gusts, tomorrow_wind_dir) " +
                    "VALUES (@idSite, @site_name, @country, @country_id, @city, @latitude, @longitude, @contact_name, @country_code, @phone, @sms, @email, @threat, @severity, @wind_speed, @wind_direction, " +
                    "@curr_time, @curr_weather_code, @curr_temperature, @curr_precip, @curr_windspeed, @curr_gusts, @curr_wind_dir, @tomorrow_weather_code, @tomorrow_high_temp, @tomorrow_low_temp, @tomorrow_precip, @tomorrow_windspeed, @tomorrow_gusts, @tomorrow_wind_dir)";
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
                cmd1.Parameters.AddWithValue("@wind_speed", siteIn.wind_speed);
                cmd1.Parameters.AddWithValue("@Wind_direction", siteIn.wind_direction);
                cmd1.Parameters.AddWithValue("@curr_time", siteIn.curr_time);
                cmd1.Parameters.AddWithValue("@curr_weather_code", siteIn.curr_weather_code);
                cmd1.Parameters.AddWithValue("@curr_temperature", siteIn.curr_temperature);
                cmd1.Parameters.AddWithValue("@curr_precip", siteIn.curr_precip);
                cmd1.Parameters.AddWithValue("@curr_windspeed", siteIn.curr_windspeed);
                cmd1.Parameters.AddWithValue("@curr_gusts", siteIn.curr_gusts);
                cmd1.Parameters.AddWithValue("@curr_wind_dir", siteIn.curr_wind_dir);
                cmd1.Parameters.AddWithValue("@tomorrow_weather_code", siteIn.tomorrow_weather_code);
                cmd1.Parameters.AddWithValue("@tomorrow_high_temp", siteIn.tomorrow_high_temp);
                cmd1.Parameters.AddWithValue("@tomorrow_low_temp", siteIn.tomorrow_low_temp);
                cmd1.Parameters.AddWithValue("@tomorrow_precip", siteIn.tomorrow_precip);
                cmd1.Parameters.AddWithValue("@tomorrow_windspeed", siteIn.tomorrow_windspeed);
                cmd1.Parameters.AddWithValue("@tomorrow_gusts", siteIn.tomorrow_gusts);
                cmd1.Parameters.AddWithValue("@tomorrow_wind_dir", siteIn.tomorrow_wind_dir);

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
                cmd1.Parameters.AddWithValue("@curr_time", siteIn.curr_time);
                cmd1.Parameters.AddWithValue("@curr_weather_code", siteIn.curr_weather_code);
                cmd1.Parameters.AddWithValue("@curr_temperature", siteIn.curr_temperature);
                cmd1.Parameters.AddWithValue("@curr_precip", siteIn.curr_precip);
                cmd1.Parameters.AddWithValue("@curr_windspeed", siteIn.curr_windspeed);
                cmd1.Parameters.AddWithValue("@curr_gusts", siteIn.curr_gusts);
                cmd1.Parameters.AddWithValue("@curr_wind_dir", siteIn.curr_wind_dir);
                cmd1.Parameters.AddWithValue("@tomorrow_weather_code", siteIn.tomorrow_weather_code);
                cmd1.Parameters.AddWithValue("@tomorrow_high_temp", siteIn.tomorrow_high_temp);
                cmd1.Parameters.AddWithValue("@tomorrow_low_temp", siteIn.tomorrow_low_temp);
                cmd1.Parameters.AddWithValue("@tomorrow_precip", siteIn.tomorrow_precip);
                cmd1.Parameters.AddWithValue("@tomorrow_windspeed", siteIn.tomorrow_windspeed);
                cmd1.Parameters.AddWithValue("@tomorrow_gusts", siteIn.tomorrow_gusts);
                cmd1.Parameters.AddWithValue("@tomorrow_wind_dir", siteIn.tomorrow_wind_dir);


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
                                                            "severity = @severity, wind_speed = @wind_speed, wind_direction = @wind_direction, curr_time = @curr_time, " +
                                                            "curr_weather_code = @curr_weather_code, curr_temperature = @curr_temperature, curr_precip = @curr_precip, curr_windspeed = @curr_windspeed, " +
                                                            "curr_gusts = @curr_gusts, curr_wind_dir = @curr_wind_dir, tomorrow_weather_code = @tomorrow_weather_code, tomorrow_high_temp = @tomorrow_high_temp, " +
                                                            "tomorrow_low_temp = @tomorrow_low_temp, tomorrow_precip = @tomorrow_precip, tomorrow_windspeed = @tomorrow_windspeed, tomorrow_gusts = @tomorrow_gusts, " +
                                                            "tomorrow_wind_dir = @tomorrow_wind_dir WHERE idSite LIKE @idSite";


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
                cmd1.Parameters.AddWithValue("@wind_speed", siteIn.curr_windspeed);
                cmd1.Parameters.AddWithValue("@wind_direction", siteIn.curr_wind_dir);
                cmd1.Parameters.AddWithValue("@curr_time", siteIn.curr_time);
                cmd1.Parameters.AddWithValue("@curr_weather_code", siteIn.curr_weather_code);
                cmd1.Parameters.AddWithValue("@curr_temperature", siteIn.curr_temperature);
                cmd1.Parameters.AddWithValue("@curr_precip", siteIn.curr_precip);
                cmd1.Parameters.AddWithValue("@curr_windspeed", siteIn.curr_windspeed);
                cmd1.Parameters.AddWithValue("@curr_gusts", siteIn.curr_gusts);
                cmd1.Parameters.AddWithValue("@curr_wind_dir", siteIn.curr_wind_dir);
                cmd1.Parameters.AddWithValue("@tomorrow_weather_code", siteIn.tomorrow_weather_code);
                cmd1.Parameters.AddWithValue("@tomorrow_high_temp", siteIn.tomorrow_high_temp);
                cmd1.Parameters.AddWithValue("@tomorrow_low_temp", siteIn.tomorrow_low_temp);
                cmd1.Parameters.AddWithValue("@tomorrow_precip", siteIn.tomorrow_precip);
                cmd1.Parameters.AddWithValue("@tomorrow_windspeed", siteIn.tomorrow_windspeed);
                cmd1.Parameters.AddWithValue("@tomorrow_gusts", siteIn.tomorrow_gusts);
                cmd1.Parameters.AddWithValue("@tomorrow_wind_dir", siteIn.tomorrow_wind_dir);

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
                cmd1.Parameters.AddWithValue("@idSite", siteIn.idSite);
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




