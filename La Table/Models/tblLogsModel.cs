using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblLogsModel
    {
        public int LogsID { get; set; }
        public int AccountID { get; set; }
        public string action { get; set; }
        public DateTime timestamp { get; set; }
    }
}