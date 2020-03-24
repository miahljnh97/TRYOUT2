using System;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.Domain
{
    public class NotifContext : DbContext
    {
        public NotifContext(DbContextOptions<NotifContext> opt) : base(opt) { }

        public DbSet<Notification> notification { get; set; }
        public DbSet<NotificationLogs> notificationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<NotificationLogs>()
                .HasOne(x => x.notif)
                .WithMany()
                .HasForeignKey(x => x.Notification_id);

            modelBuilder
                .Entity<NotificationLogs>()
                .HasOne(x => x.users)
                .WithMany()
                .HasForeignKey(x => x.From);
        }
    }
}
