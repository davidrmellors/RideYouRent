using CLDVPOE.Pages.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace CLDVPOE.Pages
{
 
    public class LoginModel : PageModel
    {
        public string errorMessage = "";
        public void OnGet()
        {
           
        }

        public void OnPost() 
        {
            string email = Request.Form["email"].ToString();
            string password = Request.Form["password"].ToString();

            if(LoginDetails.email.Equals(email))
            {
                if (LoginDetails.password.Equals(password))
                {
                    Response.Redirect("/Cars/Index");
                }
                else
                {
                    errorMessage = "Incorrect email or password";
                }
            }
            else
            {
                errorMessage = "Incorrect email or password";
            }
        }

    }

    public class LoginDetails
    {
        public static string email;
        public static string password;

    }
}
