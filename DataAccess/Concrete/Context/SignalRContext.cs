using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccess.Concrete.Context
{
    public class SignalRContext : DbContext
    {
        public SignalRContext(DbContextOptions<SignalRContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Orders tablosunda trigger bulunduğundan EF'in normal çıktısı olan OUTPUT ayarını değiştirir ve tablonun trigger'a sahip olduğunu belirtir.
            modelBuilder.Entity<Order>()
                .ToTable("Orders", tableBuilder =>
                {
                    tableBuilder.HasTrigger("SumMoneyCases");
                    tableBuilder.HasTrigger("UpdateOrderDetailStatus");
                });

            base.OnModelCreating(modelBuilder);
        }

        DbSet<About> Abouts { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<DailyDiscount> DailyDiscounts { get; set; }
        DbSet<Feature> Features { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<SocialMedia> SocialMedias { get; set; }
        DbSet<Testimonial> Testimonials { get; set; }
        DbSet<WorkingHour> WorkingHours { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<MoneyCase> MoneyCases { get; set; }
        DbSet<RestaurantTable> RestaurantTables { get; set; }
        DbSet<Basket> Baskets { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<HelpDesk> HelpDesks { get; set; }
        DbSet<Discount> Discounts { get; set; }
    }
}
