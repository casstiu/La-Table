using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblAccountsModel
    {
        public int AccountID { get; set; }
        public int RoleID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Phone_Num { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int StatusID { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}