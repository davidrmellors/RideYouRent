using CLDVPOE.Pages.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace CLDVPOE.Pages.Rentals
{
    public class CreateModel : PageModel
    {
        public Rentals rentals = new Rentals();
        public Cars.IndexModel carsIndex = new Cars.IndexModel();
        public Inspectors.IndexModel inspectorIndex = new Inspectors.IndexModel();
        public Drivers.IndexModel driverIndex = new Drivers.IndexModel();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            carsIndex.OnGet();
            inspectorIndex.OnGet();
            driverIndex.OnGet();
        }

        public void OnPost() 
        {
            rentals.carID = Request.Form["carID"];
            rentals.inspectorID = Request.Form["inspectorID"];
            rentals.driverID = Request.Form["driverID"];
            rentals.rentalFee = Request.Form["rentalFee"];
            rentals.startDate = Request.Form["startDate"];
            rentals.endDate = Request.Form["endDate"];

            if (rentals.carID.Length == 0 || rentals.inspectorID.Length == 0 ||
            rentals.driverID.Length == 0 || rentals.rentalFee.Length == 0 || rentals.startDate.Length == 0 ||
            rentals.endDate.Length == 0)
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
                    string sql = "INSERT INTO CarRental " +
                                 "(carID, inspectorID, driverID, rentalFee, startDate, endDate) " +
                                 "VALUES (@carID, @inspectorID, @driverID, @rentalFee, @startDate, @endDate);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@carID", rentals.carID);
                        command.Parameters.AddWithValue("@inspectorID", rentals.inspectorID);
                        command.Parameters.AddWithValue("@driverID", rentals.driverID);
                        command.Parameters.AddWithValue("@rentalFee", rentals.rentalFee);
                        command.Parameters.AddWithValue("@startDate", rentals.startDate);
                        command.Parameters.AddWithValue("@endDate", rentals.endDate);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            rentals.carID = ""; rentals.inspectorID = ""; rentals.driverID = ""; rentals.rentalFee = ""; rentals.startDate = ""; rentals.endDate = "";
            successMessage = "New Rental Added Correctly";

            Response.Redirect("/Rentals/Index");
        }

    }

}
