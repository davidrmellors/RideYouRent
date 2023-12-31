using CLDVPOE.Pages.Drivers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Drivers
{
    public class CreateModel : PageModel
    {
        public Drivers drivers = new Drivers();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            drivers.driverName = Request.Form["driverName"];
            drivers.driverAddress = Request.Form["driverAddress"];
            drivers.driverEmail = Request.Form["driverEmail"];
            drivers.driverMobileNumber = Request.Form["driverMobileNumber"];

            if (drivers.driverName.Length == 0 || drivers.driverAddress.Length == 0 ||
            drivers.driverEmail.Length == 0 || drivers.driverMobileNumber.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client into the database
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={baseDir}rideyourent-dbs_Primary.mdf;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Driver " +
                                 "(driverName, driverAddress, driverEmail, driverMobileNumber) " +
                                 "VALUES (@driverName, @driverAddress, @driverEmail, @driverMobileNumber);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@driverName", drivers.driverName);
                        command.Parameters.AddWithValue("@driverAddress", drivers.driverAddress);
                        command.Parameters.AddWithValue("@driverEmail", drivers.driverEmail);
                        command.Parameters.AddWithValue("@driverMobileNumber", drivers.driverMobileNumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            drivers.driverName = ""; drivers.driverAddress = ""; drivers.driverEmail = ""; drivers.driverMobileNumber="";
            successMessage = "New Inspector Added Correctly";

            Response.Redirect("/Drivers/Index");
        }
    }
}
