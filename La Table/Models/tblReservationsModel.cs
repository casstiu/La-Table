using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblReservationsModel
    {
        public int ReservationID { get; set; }
        public int AccountID { get; set; }
        public int TableID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumofGuests { get; set; }
        public int StatusID { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}