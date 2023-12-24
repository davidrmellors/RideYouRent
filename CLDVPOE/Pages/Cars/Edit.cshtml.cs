using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CLDVPOE.Pages.Cars
{
    public class EditModel : PageModel
    {
        public Cars cars = new Cars();
        public string errorMessage = "";
        public string successMessage = "";
        public IndexModel indexModel = new IndexModel();

        public void OnGet()
        {
            string carID = Request.Query["carID"];
            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Car WHERE carID=@carID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@carID", carID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                cars.carID = reader.GetString(0);
                                cars.makeID = "" + reader.GetInt32(1);
                                cars.bodyTypeID = "" + reader.GetInt32(2);
                                cars.carModel = reader.GetString(3);
                                cars.kilometersTravelled = "" + reader.GetInt32(4);
                                cars.serviceKilometers = "" + reader.GetInt32(5);
                                cars.available = "" + reader.GetBoolean(6);
                                cars.serviceDue = "" + reader.GetBoolean(7);
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
            cars.carID = Request.Form["carID"];
            cars.makeID = Request.Form["makeID"];
            cars.bodyTypeID = Request.Form["bodyTypeID"];
            cars.carModel = Request.Form["carModel"];
            cars.kilometersTravelled = Request.Form["kilometersTravelled"];
            cars.serviceKilometers = Request.Form["serviceKilometers"];
            cars.available = Request.Form["available"];
            cars.serviceDue = Request.Form["serviceDue"];

            if (cars.carID.Length == 0 || cars.makeID.Length == 0 || cars.bodyTypeID.Length == 0 ||
               cars.carModel.Length == 0 || cars.kilometersTravelled.Length == 0|| cars.serviceKilometers.Length == 0 ||
               cars.available.Length == 0|| cars.serviceDue.Length == 0)
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
                    string sql = "UPDATE Car " +
                                 "SET carID=@carID, makeID=@makeID, bodyTypeID=@bodyTypeID, carModel=@carModel, kilometersTravelled=@kilometersTravelled, " +
                                 "serviceKilometers=@serviceKilometers, available=@available, serviceDue=@serviceDue " +
                                 "WHERE carID=@carID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
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

            Response.Redirect("/Cars/Index");
        }
    }
}
