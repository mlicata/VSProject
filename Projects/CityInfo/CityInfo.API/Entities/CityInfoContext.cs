﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<POI> POIs { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
