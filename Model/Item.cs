using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestRazor.Model
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Category { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal BeginPrice { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal RedemtionPrice { get; set; }
        public long UserCreatedId { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public DateTime DurationTime { get; set; }
        public DateTime DateTimeBegin { get; set; }

        public bool BetWasDone { get; set; }
        public bool ItemWasRedempt { get; set; }
        public long LastBetUserId { get; set; }
        public string Status { get; set; }

        public class ConfirmToken
        {
            public long Id { get; set; }
            public long PersonId { get; set; }
            public string Email { get; set; }

            public string Token { get; set; }
            public DateTime CreationDateTime { get; set; }

            public int LifeTimeMin { get; set; }
        }
        public class AppData : DbContext
        {
            public DbSet<Item> Items { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<ConfirmToken> ConfirmTokens { get; set; }
            public AppData(DbContextOptions<AppData> dbContextOptions) : base(dbContextOptions)
            {
                // Database.EnsureCreated();

            }

            internal Task FirstOrDefaultAsync()
            {
                throw new NotImplementedException();
            }
        }
    }
}
