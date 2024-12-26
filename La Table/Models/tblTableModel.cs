using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblTableModel
    {
        public int TableID { get; set; }
        public int SeatingCap { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}