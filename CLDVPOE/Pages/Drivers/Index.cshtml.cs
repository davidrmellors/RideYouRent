using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Drivers
{
    public class IndexModel : PageModel
    {
        public static List<Drivers> listDrivers = new List<Drivers>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";
                listDrivers.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Driver";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Drivers drivers = new Drivers();
                                drivers.driverID = "" + reader.GetInt32(0);
                                drivers.driverName = reader.GetString(1);
                                drivers.driverAddress = reader.GetString(2);
                                drivers.driverEmail = reader.GetString(3);
                                drivers.driverMobileNumber = reader.GetString(4);

                                listDrivers.Add(drivers);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        
    }
    public class Drivers
    {
        public string driverID;
        public string driverName;
        public string driverAddress;
        public string driverEmail;
        public string driverMobileNumber;
    }
}
