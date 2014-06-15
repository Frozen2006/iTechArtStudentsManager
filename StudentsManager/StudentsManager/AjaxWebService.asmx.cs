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
using StudentsManager.Security.Identity;
using DAL;

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

        public struct LoginResult
        {
            public bool authenticationResult;
            public string message;
            public string userName;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public LoginResult Login(string username, string passwordHash)
        {
            var result = new LoginResult();

            var userStore = new ApplicationUserStore<ApplicationUser>(StudentsManagerDbContext.GetInstance());
            var userManager = new ApplicationUserManager<ApplicationUser>();

            var userByName = userManager.FindByName(username);
            ApplicationUser userByPassword = null;
            if (userByName != null)
            {
                userByPassword = userManager.Find(username, passwordHash);
            }


            if (userByPassword != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                var identity = userManager.CreateIdentity<ApplicationUser,string>(userByPassword, DefaultAuthenticationTypes.ApplicationCookie);
                //var identity = await userManager.CreateIdentityAsync(userByPassword, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
                result.message = "ok";
                result.authenticationResult = true;
                result.userName = User.Identity.Name;
            }
            else
            {

                result.message = "Invalid email or password";
                result.authenticationResult = false;
            }


            return result;
            
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetRoleName(string username, string passwordHash)
        {
            var context = StudentsManagerDbContext.GetInstance();
            var userStore = new ApplicationUserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager<ApplicationUser>();

            var userByName = userManager.FindByName(username);
            if (userByName == null)
            {
                return "None";
            }

            string roleName = userManager
                .GetRoles<ApplicationUser, string>(userByName.Id)
                .FirstOrDefault();

            return roleName;
        }
    }
}
