using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblLogsMap : EntityTypeConfiguration<tblLogsModel>
    {
        public tblLogsMap()
        {
            HasKey(i => i.LogsID);
            ToTable("tbllogs");
        }

    }
    
    
}