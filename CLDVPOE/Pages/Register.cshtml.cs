using CLDVPOE.Pages.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages
{
    public class RegisterModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            string email = Request.Form["email"];
            string password = Request.Form["password"];
            string confirmPassword = Request.Form["confirmPassword"];

            if(email.Length == 0 ||  password.Length == 0 || confirmPassword.Length == 0) 
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Inspector WHERE inspectorEmail=@email;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                if (password.ToString().Equals(confirmPassword.ToString()))
                                {

                                    LoginDetails.email = email;
                                    LoginDetails.password = password;
                                    Response.Redirect("/Login");
                                }
                                else
                                {
                                    errorMessage = "Passwords do not match";
                                }
                            }
                            else
                            {
                                errorMessage = "The email you entered was not a verified inspector email";
                            }


                        }
                    }

                   
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        } 
    }

    
}
