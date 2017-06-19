using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityClient.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string College { get; set; }
        public string Stream { get; set; }
       
    }
}