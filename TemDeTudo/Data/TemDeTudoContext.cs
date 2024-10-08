﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TemDeTudo.Models;

namespace TemDeTudo.Data
{
    public class TemDeTudoContext : DbContext
    {
        public TemDeTudoContext (DbContextOptions<TemDeTudoContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; } = default!;
        public DbSet<Depatment> Depatment { get; set; } = default!;
        public DbSet<SalesRecord> SalesRecord { get; set; } = default!;
    }
}
