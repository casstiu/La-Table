using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblRolesModel
    {
        public int RoleID { get; set; }
        public string roleName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}