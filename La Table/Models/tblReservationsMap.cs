using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblReservationsMap : EntityTypeConfiguration<tblReservationsModel>
    {
        public tblReservationsMap()
        {
            HasKey(i => i.ReservationID);
            ToTable("tblreservations");
        }
    }
}