﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace La_Table.Models
{
    public class tblTableMap : EntityTypeConfiguration<tblTableModel>
    {
        public tblTableMap()
        {
            HasKey(i => i.TableID);
            ToTable("tbltable");
        }
    }
}