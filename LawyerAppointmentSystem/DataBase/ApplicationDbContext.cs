using LawyerAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LawyerAppointmentSystem.DataBase;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Lawyer> Lawyers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<LawyerAvailability> LawyerAvailabilities { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<LawyerAvailability>()
            .HasOne(ac => ac.Lawyer)
            .WithMany(l => l.Availabilities)
            .HasForeignKey(ac => ac.LawyerId)
        .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Lawyer)
            .WithMany(l => l.Appointments)
            .HasForeignKey(a => a.LawyerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Appointment)
            .WithMany(a => a.Payments)
            .HasForeignKey(p => p.AppointmentId)
            .OnDelete(DeleteBehavior.NoAction);






        // Configure relationships and constraints if needed
        //modelBuilder.Entity<Appointment>()



        //    .HasOne(a => a.Lawyer)
        //    .WithMany(l => l.Appointments)
        //    .HasForeignKey(a => a.LawyerId);
        //modelBuilder.Entity<Appointment>()
        //    .HasOne(a => a.User)
        //    .WithMany(u => u.Appointments)
        //    .HasForeignKey(a => a.UserId);
        //modelBuilder.Entity<Appointment>()
        //    .HasOne(a => a.Payment)
        //    .WithOne(p => p.Appointment)
        //    .HasForeignKey<Payment>(p => p.AppointmentId);
        //modelBuilder.Entity<Review>()
        //    .HasOne(r => r.Lawyer)
        //    .WithMany(l => l.Reviews)
        //    .HasForeignKey(r => r.LawyerId);
        //modelBuilder.Entity<Review>()
        //    .HasOne(r => r.User)
        //    .WithMany(u => u.Reviews)
        //    .HasForeignKey(r => r.UserId);
    }
}

