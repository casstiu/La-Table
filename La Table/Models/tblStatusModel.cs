using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblStatusModel
    {
        public int StatusID { get; set; }
        public string statusName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}