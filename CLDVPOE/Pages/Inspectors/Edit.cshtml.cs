using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CLDVPOE.Pages.Inspectors
{
    public class EditModel : PageModel
    {
        public Inspectors inspectors = new Inspectors();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string inspectorID = Request.Query["inspectorID"];
            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Inspector WHERE inspectorID=@inspectorID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@inspectorID", inspectorID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                inspectors.inspectorID = reader.GetString(0);
                                inspectors.inspectorName = reader.GetString(1);
                                inspectors.inspectorEmail = reader.GetString(2);
                                inspectors.inspectorMobileNumber = reader.GetString(3);
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

            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Inspector " +
                                 "SET inspectorID=@inspectorID, inspectorName=@inspectorName, inspectorEmail=@inspectorEmail, inspectorMobileNumber=@inspectorMobileNumber " +
                                 "WHERE inspectorID=@inspectorID;";
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

            Response.Redirect("/Inspectors/Index");
        }
    }
}
