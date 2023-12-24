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
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={baseDir}rideyourent-dbs_Primary.mdf;Trusted_Connection=True;";


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
