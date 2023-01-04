using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrakingPoint.Models
{
    public class LoginModel
    {
        [PrimaryKey]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
