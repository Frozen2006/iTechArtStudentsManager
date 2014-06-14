using DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;


using DAL;

namespace StudentsManager.Security.Identity
{
    public class ApplicationUserManager<TUser> : UserManager<TUser> where TUser : ApplicationUser
    {
        protected ApplicationUserStore<TUser> ApplicationUserStore { get; set; }

        public ApplicationUserManager()
            : base(new ApplicationUserStore<TUser>(StudentsManagerDbContext.GetInstance()))
        {
            this.UserValidator = new UserValidator<TUser>(this) { AllowOnlyAlphanumericUserNames = false };
        }

        public virtual async Task<TUser> FindByEmailCustomAsync(string email)
        {
            ApplicationUserStore<TUser> applicationStore = (ApplicationUserStore<TUser>)this.Store;
            return await applicationStore.FindByEmailCustomAsync(email);
        }

    }
}