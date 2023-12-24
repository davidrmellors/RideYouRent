using CLDVPOE.Pages.Inspectors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Inspectors
{
    public class CreateModel : PageModel
    {
        public Inspectors inspectors = new Inspectors();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            inspectors.inspectorID = Request.Form["inspectorID"];
            inspectors.inspectorName = Request.Form["inspectorName"];
            inspectors.inspectorEmail = Request.Form["inspectorEmail"];
            inspectors.inspectorMobileNumber = Request.Form["inspectorMobileNumber"];
            if (inspectors.inspectorID.Length == 0 || inspectors.inspectorName.Length == 0 || inspectors.inspectorEmail.Length == 0 ||
            inspectors.inspectorMobileNumber.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client into the database
            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Inspector " +
                                 "(inspectorID, inspectorName, inspectorEmail, inspectorMobileNumber) " +
                                 "VALUES (@inspectorID, @inspectorName, @inspectorEmail, @inspectorMobileNumber);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@inspectorID", inspectors.inspectorID);
                        command.Parameters.AddWithValue("@inspectorName", inspectors.inspectorName);
                        command.Parameters.AddWithValue("@inspectorEmail", inspectors.inspectorEmail);
                        command.Parameters.AddWithValue("@inspectorMobileNumber", inspectors.inspectorMobileNumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            inspectors.inspectorID = ""; inspectors.inspectorName = ""; inspectors.inspectorEmail = ""; inspectors.inspectorMobileNumber = ""; 
            successMessage = "New Inspector Added Correctly";

            Response.Redirect("/Inspectors/Index");
        }
    }
}
