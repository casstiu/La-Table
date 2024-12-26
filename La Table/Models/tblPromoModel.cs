using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblPromoModel
    {
        public int PromoID { get; set; }
        public string promoImage { get; set; }
        public string promoName { get; set; }
        public string description { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

    }
}