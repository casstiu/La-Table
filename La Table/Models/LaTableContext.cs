using La_Table.Models;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LaTable.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LaTableContext : DbContext
    {
        static LaTableContext()
        {
            Database.SetInitializer<LaTableContext>(null);
        }
        public LaTableContext() : base("Name=latabledb")
        {

        }
        public virtual DbSet<tblAccountsModel> tblaccounts { get; set; }
        public virtual DbSet<tblLogsModel> tbllogs { get; set; }
        public virtual DbSet<tblPromoModel> tblpromo { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tblAccountsMap());
            modelBuilder.Configurations.Add(new tblLogsMap());
            modelBuilder.Configurations.Add(new tblPromoMap());


        }
    }
}