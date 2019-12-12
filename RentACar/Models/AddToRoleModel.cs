using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class AddToRoleModel
    {
        public string UserEmail { get; set; }

        public List<string> users { get; set; }

        public string Role { get; set; }

        public List<string> roles { get; set; }

        public AddToRoleModel()
        {
            users = new List<string>();
            roles = new List<string>();
        }
    }
}