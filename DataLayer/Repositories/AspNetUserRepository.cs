using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class AspNetUserRepository : AbstractRepository
    {
        public AspNetUser FindAspNetUser(string name)
        {
            try
            {
                var user = Context.AspNetUsers.FirstOrDefault(x => x.UserName == name);
                return user;
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}
