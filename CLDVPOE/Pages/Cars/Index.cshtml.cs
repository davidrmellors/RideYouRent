using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Cars
{
    public class IndexModel : PageModel
    {
        public static List<Cars> listCars = new List<Cars>();
        public static List<CarMake> listCarMake = new List<CarMake>();
        public static List<CarBodyType> listCarBodyType = new List<CarBodyType>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";
                listCars.Clear();
                listCarMake.Clear();
                listCarBodyType.Clear();

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Car";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                Cars cars = new Cars();
                                cars.carID = reader.GetString(0);
                                cars.makeID = "" + reader.GetInt32(1);
                                cars.bodyTypeID = "" + reader.GetInt32(2);
                                cars.carModel = reader.GetString(3);
                                cars.kilometersTravelled = "" + reader.GetInt32(4);
                                cars.serviceKilometers = "" + reader.GetInt32(5);
                                cars.available = "" + reader.GetBoolean(6);
                                cars.serviceDue = "" + reader.GetBoolean(7);

                                listCars.Add(cars);
                            }
                        }
                    }

                    sql = "SELECT * FROM CarMake";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CarMake carMake = new CarMake();
                                carMake.makeID = "" + reader.GetInt32(0);
                                carMake.makeName = reader.GetString(1);

                                listCarMake.Add(carMake);
                            }
                        }
                    }

                    listCarBodyType.Clear();
                    sql = "SELECT * FROM CarBodyType";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CarBodyType carBodyType = new CarBodyType();
                                carBodyType.bodyTypeID = "" + reader.GetInt32(0);
                                carBodyType.bodyTypeName = reader.GetString(1);

                                listCarBodyType.Add(carBodyType);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

    }
    public class Cars
    {
        public string carID;
        public string makeID;
        public string bodyTypeID;
        public string carModel;
        public string kilometersTravelled;
        public string serviceKilometers;
        public string available;
        public string serviceDue;

    }
    public class CarMake
    {
        public string makeID;
        public string makeName;
    }

    public class CarBodyType
    {
        public string bodyTypeID;
        public string bodyTypeName;
    }
}
