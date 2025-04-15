using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Domain.Entity
{
    public class Users
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
