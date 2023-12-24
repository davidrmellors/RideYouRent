using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Returns
{
    public class PenaltyModel : PageModel
    {
        public Returns returns = new Returns();
        public string errorMessage = "";
        public string successMessage = "";
        public bool formSubmitted = false;
        public void OnGet()
        {
        }

        public void OnPost()
        {
            string returnID = Request.Form["returnID"];
            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE CarReturn SET fine = elapsedDate * 500 WHERE returnID=@returnID;";
                    
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@returnID", returnID);
                    command.ExecuteNonQuery();

                    sql = "SELECT returnID, fine FROM CarReturn WHERE returnID=@returnID;";
                    using (SqlCommand command1 = new SqlCommand(sql, connection))
                    {
                        command1.Parameters.AddWithValue("@returnID", returnID);
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                returns.returnID = "" + reader.GetInt32(0);
                                returns.fine = "" + reader.GetDecimal(1);

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
