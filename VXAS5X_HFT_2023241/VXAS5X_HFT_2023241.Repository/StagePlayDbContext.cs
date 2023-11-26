using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Repository
{
    public class StagePlayDbContext : DbContext
    {

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Dramaturg> Dramaturgs { get; set; }
        public virtual DbSet<StagePlay> Plays { get; set; }

        public StagePlayDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                .UseInMemoryDatabase("StagePlayDatabase");

            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(e =>
            {
                e.HasMany(a => a.Plays)
                .WithMany(t => t.Actors);

            });

            modelBuilder.Entity<StagePlay>(t =>
            {
                t.HasMany(z => z.Actors)
                .WithMany(u => u.Plays);

                t.HasOne(m => m.Dramaturg)
                .WithMany(d => d.Plays)
                .HasForeignKey(m => m.DramaturgId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Dramaturg>(t =>
            {
                t.HasMany(d => d.Plays)
                .WithOne(m => m.Dramaturg)
                .OnDelete(DeleteBehavior.Restrict);

            });


            Dramaturg GSzabo = new Dramaturg() { Id = 1, Name = "Gábor Szabó", Gender = "man", Age = 58 };
            Dramaturg ZHorvath = new Dramaturg() { Id = 2, Name = "Zoltán Horváth", Gender = "man", Age = 48 };
            Dramaturg BBalla = new Dramaturg() { Id = 3, Name = "Béla Balla", Gender = "man", Age = 60 };
            Dramaturg TToth = new Dramaturg() { Id = 4, Name = "Tamás Tóth", Gender = "man", Age = 59 };
            Dramaturg RVarga = new Dramaturg() { Id = 5, Name = "Réka Varga", Gender = "woman", Age = 49 };
            Dramaturg FKovacs = new Dramaturg() { Id = 6, Name = "Ferenc Kovács", Gender = "man", Age = 60 };
            Dramaturg NJanos = new Dramaturg() { Id = 7, Name = "Nagy János", Gender = "man", Age = 32 };

            Actor RNagy = new Actor() { Id = 1, Name = "Rudolf Nagy", Age = 48, Gender = "man" };
            Actor KIvancz = new Actor() { Id = 2, Name = "Iván Kamarás", Age = 52, Gender = "man" };
            Actor TAcs = new Actor() { Id = 3, Name = "Tamás Ács", Age = 56, Gender = "man" };
            Actor EMarton = new Actor() { Id = 4, Name = "Eszter Marton", Age = 42, Gender = "woman" };
            Actor OPiros = new Actor() { Id = 5, Name = "Orsolya Piros", Age = 51, Gender = "woman" };
            Actor KMarcell = new Actor() { Id = 6, Name = "Kiss Marcell", Age = 25, Gender = "man" };

            StagePlay tuktuk = new StagePlay() { Id = 1, Title = "Tükör a tóban", Profit = 8460294, Premier = 2012, Rating = "okay", DramaturgId = GSzabo.Id };
            StagePlay varazspal = new StagePlay() { Id = 2, Title = "A varázspálca", Profit = 9826482, Premier = 2011, Rating = "exceptionaal", DramaturgId = ZHorvath.Id };
            StagePlay noknek = new StagePlay() { Id = 3, Title = "Nőknek való", Profit = 1873181, Premier = 2014, Rating = "good", DramaturgId = BBalla.Id };
            StagePlay kispion = new StagePlay() { Id = 4, Title = "A kis kém", Profit = 236496581, Premier = 2013, Rating = "exceptionaal", DramaturgId = BBalla.Id };
            StagePlay nagybalhe = new StagePlay() { Id = 5, Title = "Nagy balhé", Profit = 84351, Premier = 1993, Rating = "horrible", DramaturgId = TToth.Id };
            StagePlay lakaskulcs = new StagePlay() { Id = 6, Title = "Lakáskulcs", Profit = 320045, Premier = 2019, Rating = "good", DramaturgId = RVarga.Id };
            StagePlay lathatatlankez = new StagePlay() { Id = 7, Title = "A Láthatatlan Kéz", Profit = 813456, Premier = 2010, Rating = "horrible", DramaturgId = RVarga.Id };
            StagePlay macskak = new StagePlay() { Id = 8, Title = "Macskák", Profit = 63701648, Premier = 2000, Rating = "exceptional", DramaturgId = FKovacs.Id };
            StagePlay varazslovo = new StagePlay() { Id = 9, Title = "A varázsló vő", Profit = 1156789, Premier = 2012, Rating = "okay", DramaturgId = GSzabo.Id };
            StagePlay kiralyilany = new StagePlay() { Id = 10, Title = "A királyi lány", Profit = 27001500, Premier = 2015, Rating = "exceptional", DramaturgId = KMarcell.Id };
            StagePlay tuktuk2 = new StagePlay() { Id = 11, Title = "Tükör a tóban - Béla Balla feldolgozás", Profit = 1851347, Premier = 2018, Rating = "good", DramaturgId = BBalla.Id };


            modelBuilder.Entity<Actor>().HasData(RNagy, KIvancz, TAcs, EMarton, OPiros, KMarcell);
            modelBuilder.Entity<Dramaturg>().HasData(GSzabo, ZHorvath, BBalla, TToth, RVarga, FKovacs, NJanos);
            modelBuilder.Entity<StagePlay>().HasData(tuktuk, varazspal, noknek, kispion, nagybalhe, lakaskulcs, lathatatlankez, macskak, varazslovo, kiralyilany, tuktuk2);


        }

    }
}
