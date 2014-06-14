using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;

using DAL;
using DAL.Entities;
using StudentsManager.Security.Identity;

namespace WebFormsIdentity
{
    public partial class Register : System.Web.UI.Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var context = new StudentsManagerDbContext();
            var userStore = new ApplicationUserStore<ApplicationUser>(context);
            var manager = new ApplicationUserManager<ApplicationUser>();

            var user = new ApplicationUser() { UserName = UserName.Text };
            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                StatusMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
            }
            else
            {
                StatusMessage.Text = result.Errors.FirstOrDefault();
            }

            ApplicationUser someUser = context.Users.FirstOrDefault();
            context.Tasks.Add(new Task
            {
                Desciption = "You lold",
                Level = 9,
                Name = "lo",
                Tags = "Oh, my, tags"

            });
            context.SaveChanges();
            
        }
    }
}