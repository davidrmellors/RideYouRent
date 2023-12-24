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
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";

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
