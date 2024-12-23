using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblRolesMap : EntityTypeConfiguration<tblRolesModel>
    {
        public tblRolesMap()
        {
            HasKey(i => i.RoleID);
            ToTable("tblroles");
        }
    }
}