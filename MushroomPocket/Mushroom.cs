using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using Microsoft.EntityFrameworkCore;

namespace MushroomPocket {
    public class MushroomMaster {
        public string Name { get;set; }
        public int NoToTransform { get; set; }
        public  string TransformTo { get; set; }

        public MushroomMaster(string name, int noToTransform, string transformTo) {
            this.Name = name;
            this.NoToTransform = noToTransform;
            this.TransformTo = transformTo;
        }
    }

    public abstract class Character { // abstract class, avoid direct instances of itself
        [Key]
        public int Id { get; set; } // { PK }

        public string Name { get; set; }
        public int HP { get; set; }
        public int EXP { get; set; }
        public string Skill { get; set; }
        public int DMG { get; set; }
    }

    public class Waluigi : Character {
        public Waluigi() {
            Name = "Waluigi";
            Skill = "Agility";
            DMG = 18;
        }
    }

    public class Daisy : Character {
        public Daisy() {
            Name = "Daisy";
            Skill = "Leadership";
            DMG = 20;
        }
    }

    public class Wario : Character {
        public Wario() {
            Name = "Wario";
            Skill = "Strength";
            DMG = 25;
        }
    }

    public class Peach : Character {
        public Peach() {
            Name = "Peach";
            Skill = "Magic Abilities";
            DMG = 22;
        }
    }

    public class Mario : Character {
        public Mario() {
            Name = "Mario";
            Skill = "Combat Skills";
            DMG = 30;
        }
    }

    public class Luigi : Character {
        public Luigi() {
            Name = "Luigi";
            Skill = "Precision and Accuracy";
            DMG = 21;
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