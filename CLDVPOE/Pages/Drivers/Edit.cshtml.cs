using CLDVPOE.Pages.Drivers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Drivers
{
    public class EditModel : PageModel
    {
        public Drivers drivers = new Drivers();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string driverID = Request.Query["driverID"];
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={baseDir}rideyourent-dbs_Primary.mdf;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Driver WHERE driverID=@driverID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@driverID", driverID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                drivers.driverName = reader.GetString(1);
                                drivers.driverAddress = reader.GetString(2);
                                drivers.driverEmail = reader.GetString(3);
                                drivers.driverMobileNumber = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            drivers.driverName = Request.Form["driverName"];
            drivers.driverAddress = Request.Form["driverAddress"];
            drivers.driverEmail = Request.Form["driverEmail"];
            drivers.driverMobileNumber = Request.Form["driverMobileNumber"];
            if (drivers.driverName.Length == 0 || drivers.driverAddress.Length == 0 || drivers.driverEmail.Length == 0 ||
            drivers.driverMobileNumber.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Driver " +
                                 "SET driverName=@driverName, driverAddress=@driverAddress, driverEmail=@driverEmail, driverMobileNumber=@driverMobileNumber " +
                                 "WHERE driverName=@driverName;";
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

            Response.Redirect("/Drivers/Index");
        }
    }
}
