using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrakingPoint.Models
{
    public class LoginModel
    {
        [PrimaryKey]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConf { get; set; }
        [Required]
        [Unique]
        public string Email { get; set; }
    }
}
