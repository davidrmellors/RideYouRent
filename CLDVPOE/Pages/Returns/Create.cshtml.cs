using CLDVPOE.Pages.Rentals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Returns
{
    public class CreateModel : PageModel
    {
        public Returns returns = new Returns();
        public string errorMessage = "";
        public string successMessage = "";
        public IndexModel model = new IndexModel();
        public Rentals.IndexModel rentalIndex = new Rentals.IndexModel();
        public Inspectors.IndexModel inspectorIndex = new Inspectors.IndexModel();
        public void OnGet()
        {
            rentalIndex.OnGet();
            inspectorIndex.OnGet();
        }

        public void OnPost()
        {
            returns.rentalID = Request.Form["rentalID"];
            returns.inspectorID = Request.Form["inspectorID"];
            returns.returnDate = Request.Form["returnDate"];
            returns.elapsedDate = Request.Form["elapsedDate"];
            returns.fine = Request.Form["fine"];

            if (returns.rentalID.Length == 0 || returns.inspectorID.Length == 0 ||
            returns.returnDate.Length == 0 || returns.elapsedDate.Length == 0 || returns.fine.Length == 0)
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
                    string sql = "INSERT INTO CarReturn " +
                                 "(rentalID, inspectorID, returnDate, elapsedDate, fine) " +
                                 "VALUES (@rentalID, @inspectorID, @returnDate, @elapsedDate, @fine);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@rentalID", returns.rentalID);
                        command.Parameters.AddWithValue("@inspectorID", returns.inspectorID);
                        command.Parameters.AddWithValue("@returnDate", returns.returnDate);
                        command.Parameters.AddWithValue("@elapsedDate", returns.elapsedDate);
                        command.Parameters.AddWithValue("@fine", returns.fine);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            returns.rentalID = ""; returns.inspectorID = ""; returns.returnDate = ""; returns.elapsedDate = ""; returns.fine = "";
            successMessage = "New Return Added Correctly";

            Response.Redirect("/Returns/Index");
        }
    }
}
