using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblStatusMap : EntityTypeConfiguration<tblStatusModel>
    {
        public tblStatusMap()
        {
            HasKey(i => i.StatusID);
            ToTable("tblstatus");
        }
    }
}