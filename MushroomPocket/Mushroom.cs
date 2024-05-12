using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MushroomPocket{
    public class MushroomMaster{
        public string Name {get;set;}
        public int NoToTransform {get; set;}
        public  string TransformTo {get; set;}

        public MushroomMaster(string name, int noToTransform, string transformTo){
            this.Name = name;
            this.NoToTransform = noToTransform;
            this.TransformTo = transformTo;
        }
    }

    public class DatabaseContext : DbContext { // map classes to their respective tables
        public DbSet<Character> Characters { get; set; }
        public DbSet<Waluigi> Waluigis { get; set; }
        public DbSet<Daisy> Daisies { get; set; }
        public DbSet<Wario> Warios { get; set; }
        public DbSet<Peach> Peaches { get; set; }
        public DbSet<Mario> Marios { get; set; }
        public DbSet<Luigi> Luigis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=database.db"); // db config
        }
    }
}