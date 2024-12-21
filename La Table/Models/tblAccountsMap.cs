using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblAccountsMap : EntityTypeConfiguration<tblAccountsModel>
    {
        public tblAccountsMap()
            {
                HasKey(i => i.AccountID);
                ToTable("tblaccounts");
            }
        
    }
}