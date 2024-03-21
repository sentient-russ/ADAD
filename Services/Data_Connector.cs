using adad.Models;
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;

namespace adad.Services
{
    public class Data_Connector
    {
        public string? connectionString;

        public Data_Connector()
        {
            connectionString = Environment.GetEnvironmentVariable("MariaDbConnectionStringRemote");
        }

        /*
         * Gets all sites from db
         * @param postNumStart lower range bounds for posts
         * @param postNumEnd uper range bounds for posts
         * @return returns a List of the most recent posts
         */
        public List<SiteModel> GetSitess()
        {
            List<SiteModel> foundSitesList = new List<SiteModel>();
            try
            {
                MySqlConnection conn1 = new MySqlConnection(connectionString);
                conn1.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM gcai.SiteModel", conn1);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                SiteModel foundSite = new SiteModel();
                while (reader1.Read())
                {
                    if (reader1.GetValue(1).Equals(DBNull.Value)) { } else { foundSite.idSite = reader1.GetString(0).ToString(); }
                    foundSite.idSite = reader1.GetString(0);
                    foundSite.site_name = reader1.GetString(1);
                    foundSite.country = reader1.GetString(2);
                    foundSite.city = reader1.GetString(3);
                    foundSite.latitude = reader1.GetString(4);
                    foundSite.longitude = reader1.GetString(5);
                    foundSite.contact_name = reader1.GetString(6);
                    foundSite.phone = reader1.GetString(7);
                    foundSite.sms = reader1.GetString(8);
                    foundSite.email = reader1.GetString(9);

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



    }
}
