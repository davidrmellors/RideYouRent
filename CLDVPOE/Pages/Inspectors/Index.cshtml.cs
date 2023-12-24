using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CLDVPOE.Pages.Inspectors
{
    public class IndexModel : PageModel
    {
        public static List<Inspectors> listInspectors = new List<Inspectors>();
        public void OnGet()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={baseDir}rideyourent-dbs_Primary.mdf;Trusted_Connection=True;";
                listInspectors.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Inspector";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Inspectors inspectors = new Inspectors();
                                inspectors.inspectorID = reader.GetString(0);
                                inspectors.inspectorName = reader.GetString(1);
                                inspectors.inspectorEmail = reader.GetString(2);
                                inspectors.inspectorMobileNumber = reader.GetString(3);
                                listInspectors.Add(inspectors);
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

    public class Inspectors
    {
        public string inspectorID;
        public string inspectorName;
        public string inspectorEmail;
        public string inspectorMobileNumber;
    }
}
