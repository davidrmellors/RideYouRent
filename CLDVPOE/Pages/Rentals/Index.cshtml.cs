using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Rentals
{
    public class IndexModel : PageModel
    {
        public static List<Rentals> listRentals = new List<Rentals>();
        public void OnGet()
        {
            try
            {
                listRentals.Clear();
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={baseDir}rideyourent-dbs_Primary.mdf;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM CarRental";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Rentals rentals = new Rentals();
                                rentals.rentalID = "" + reader.GetInt32(0);
                                rentals.carID = reader.GetString(1);
                                rentals.inspectorID = reader.GetString(2);
                                rentals.driverID = "" + reader.GetInt32(3);
                                rentals.rentalFee = "" + reader.GetInt32(4);
                                rentals.startDate = "" + reader.GetDateTime(5);
                                rentals.endDate = "" + reader.GetDateTime(6);

                                listRentals.Add(rentals);
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

    public class Rentals
    {
        public string rentalID;
        public string carID;
        public string inspectorID;
        public string driverID;
        public string rentalFee;
        public string startDate;
        public string endDate;
    }
}
