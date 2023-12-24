using CLDVPOE.Pages.Rentals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace CLDVPOE.Pages.Returns
{
    public class RetrieveModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        public IndexModel model = new IndexModel();
        public bool formSubmitted = false;
        public Returns returns = new Returns();
        public static string returnPenalty = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            string returnID = Request.Form["returnID"];
            returnPenalty = returnID;
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={baseDir}rideyourent-dbs_Primary.mdf;Trusted_Connection=True;";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM CarReturn WHERE returnID=@returnID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@returnID", returnID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                returns.returnID = "" + reader.GetInt32(0);
                                returns.rentalID = "" + reader.GetInt32(1);
                                returns.inspectorID = reader.GetString(2);
                                returns.returnDate = "" + reader.GetDateTime(3);
                                returns.elapsedDate = "" + reader.GetInt32(4);
                                returns.fine = "" + reader.GetDecimal(5);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            formSubmitted = true;
        }

    }
}

