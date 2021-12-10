using System.Linq;
using Microsoft.EntityFrameworkCore;
using Morales.BookingSystem.EntityFramework.Entities;

namespace Morales.BookingSystem.EntityFramework
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentEntity>().HasOne(ae => ae.Customer)
                .WithMany(c => c.CustomerAppointments).HasForeignKey(a => new {a.CustomerId});
            modelBuilder.Entity<AppointmentEntity>().HasOne(ae => ae.Employee)
                .WithMany(c => c.EmployeeAppointments).HasForeignKey(a => new {a.EmployeeId});
            modelBuilder.Entity<AppointmentTreatmentEntity>().HasKey(ate => new {ApppointmentId = ate.AppointmentId, ate.TreatmentId});
            modelBuilder.Entity<AppointmentTreatmentEntity>().HasOne(ate => ate.Treatment)
                .WithMany(t => t.AppointmentTreatment);
            modelBuilder.Entity<AppointmentTreatmentEntity>().HasOne(ate => ate.Appointment)
                .WithMany(a => a.TreatmentsList);
        }

        public virtual DbSet<AccountEntity> Accounts { get; set; }

        public virtual DbSet<AppointmentEntity> Appointments { get; set; }
        
        public virtual DbSet<TreatmentEntity> Treatments { get; set; }
    }
}