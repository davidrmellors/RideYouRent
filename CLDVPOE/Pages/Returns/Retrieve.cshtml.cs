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
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";

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

