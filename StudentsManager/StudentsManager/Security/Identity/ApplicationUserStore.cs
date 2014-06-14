using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;

using DAL;
using DAL.Entities;

namespace StudentsManager.Security.Identity
{
    public class ApplicationUserStore<TUser> : UserStore<TUser> where TUser : ApplicationUser
    {
        public ApplicationUserStore(StudentsManagerDbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual Task<TUser> FindByEmailCustomAsync(string email)
        {


            return System.Threading.Tasks.Task.FromResult<TUser>(
                Queryable.FirstOrDefault<TUser>(
                    Queryable.Where<TUser>(
                        Context.Set<TUser>(),
                        (Expression<Func<TUser, bool>>)(u => u.Email.ToUpper() == email.ToUpper())
                    )
                )
            );
        }
    }
}