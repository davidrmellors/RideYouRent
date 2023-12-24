using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Returns
{
    public class IndexModel : PageModel
    {
        public static List<Returns> listReturns = new List<Returns>();

        public void OnGet()
        {
            try
            {
                listReturns.Clear();
                string connectionString = "Data Source=dbs-vc-cldv6221-st10241466.database.windows.net;Initial Catalog=db_vc_cldv6221_st10241466;Persist Security Info=True;User ID=st10241466;Password=Monster@$123";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE CarReturn SET elapsedDate = DATEDIFF(day, CarRental.endDate, CarReturn.returnDate) " +
                        "FROM CarReturn JOIN CarRental ON CarReturn.rentalID = CarRental.rentalID;";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();

                    sql = "SELECT * FROM CarReturn";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Returns returns = new Returns();
                                returns.returnID = "" + reader.GetInt32(0);
                                returns.rentalID = "" + reader.GetInt32(1);
                                returns.inspectorID = reader.GetString(2);
                                returns.returnDate = "" + reader.GetDateTime(3);
                                returns.elapsedDate = "" + reader.GetInt32(4);
                                returns.fine = "" + reader.GetDecimal(5);

                                listReturns.Add(returns);
                            }
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        
        }
    }

    public class Returns
    {
        public string returnID;
        public string rentalID;
        public string inspectorID;
        public string returnDate;
        public string elapsedDate;
        public string fine;
    }
}
