using CLDVPOE.Pages.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Rentals
{
    public class RetrieveModel : PageModel
    {
        public bool FormSubmitted;
        public string errorMessage = "";
        public string successMessage = "";
        public Rentals rentals = new Rentals();
        public IndexModel index = new IndexModel();
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            string rentalID = Request.Form["rentalID"];
            
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={baseDir}rideyourent-dbs_Primary.mdf;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM CarRental WHERE rentalID=@rentalID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@rentalID", rentalID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rentals.rentalID = "" + reader.GetInt32(0);
                                rentals.carID = reader.GetString(1);
                                rentals.inspectorID = reader.GetString(2);
                                rentals.driverID = "" + reader.GetInt32(3);
                                rentals.rentalFee = "" + reader.GetInt32(4);
                                rentals.startDate = "" + reader.GetDateTime(5);
                                rentals.endDate = "" + reader.GetDateTime(6);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            FormSubmitted = true;
        }
    }
}
