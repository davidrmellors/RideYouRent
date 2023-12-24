using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace CLDVPOE.Pages.Cars
{
    public class CreateModel : PageModel
    {
        public Cars cars = new Cars();
        public string errorMessage = "";
        public string successMessage = "";
        public IndexModel model = new IndexModel();
        public void OnGet()
        {
        }

        public void OnPost() 
        { 
            cars.carID = Request.Form["carID"];
            cars.makeID = Request.Form["makeID"];
            cars.bodyTypeID = Request.Form["bodyTypeID"];
            cars.carModel = Request.Form["carModel"];
            cars.kilometersTravelled = Request.Form["kilometersTravelled"];
            cars.serviceKilometers = Request.Form["serviceKilometers"];
            cars.available = Request.Form["available"];
            cars.serviceDue = Request.Form["serviceDue"];

            if(cars.carID.Length == 0 || cars.carModel.Length == 0 || cars.kilometersTravelled.Length == 0|| cars.serviceKilometers.Length == 0)
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
                    string sql = "INSERT INTO Car " +
                                 "(carID, makeID, bodyTypeID, carModel, kilometersTravelled, serviceKilometers, available, serviceDue) " +
                                 "VALUES (@carID, @makeID, @bodyTypeID, @carModel, @kilometersTravelled, @serviceKilometers, @available, @serviceDue);";
                    using(SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@carID", cars.carID);
                        command.Parameters.AddWithValue("@makeID", cars.makeID);
                        command.Parameters.AddWithValue("@bodyTypeID", cars.bodyTypeID);
                        command.Parameters.AddWithValue("@carModel", cars.carModel);
                        command.Parameters.AddWithValue("@kilometersTravelled", cars.kilometersTravelled);
                        command.Parameters.AddWithValue("@serviceKilometers", cars.serviceKilometers);
                        command.Parameters.AddWithValue("@available", cars.available);
                        command.Parameters.AddWithValue("@serviceDue", cars.serviceDue);

                        command.ExecuteNonQuery();
                    }

                    
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            cars.carID = ""; cars.makeID = ""; cars.bodyTypeID = ""; cars.carModel = ""; cars.kilometersTravelled = "";
            cars.serviceKilometers = ""; cars.available = ""; cars.serviceDue="";
            successMessage = "New Car Added Correctly";

            Response.Redirect("/Cars/Index");
        }
    }

 
}
