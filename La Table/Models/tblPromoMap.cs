using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblPromoMap : EntityTypeConfiguration<tblPromoModel>
    {
        public tblPromoMap()
        {
            HasKey(i => i.PromoID);
            ToTable("tblpromo");
        }
    {
    }
}