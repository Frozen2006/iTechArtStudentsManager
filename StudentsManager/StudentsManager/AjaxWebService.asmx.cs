using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;


using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using DAL.Entities;
using System.Net;
using System.Threading.Tasks;

namespace StudentsManager
{
    /// <summary>
    /// Summary description for AjaxWebService
    /// </summary>
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AjaxWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        async public Task<string> Login(string username, string passwordHash)
        {
            var responseJson = "{\"authenticationResult\":\"{{authenticationResult}}\"," +
                                "\"message\":\"{{message}}\"}";

            bool authenticationResult = true;
            string message = "";

            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            var userByName = userManager.FindByName(username);
            IdentityUser userByPassword = null;
            if (userByName != null)
            {
                userByPassword = userManager.Find(username, passwordHash);
            }


            if (userByPassword != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                var identity = await userManager.CreateIdentityAsync(userByPassword, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
            }
            else
            {

                message = "Invalid email or password";
                authenticationResult = false;
            }

           

            return responseJson
                    .Replace("{{authenticationResult}}", authenticationResult.ToString())
                    .Replace("{{message}}", message);
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            
        }
    }
}
