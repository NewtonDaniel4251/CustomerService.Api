using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CustomerService.Core.Models
{
    public partial class CustomerRegistrationContext : DbContext
    {
        public CustomerRegistrationContext()
        {
        }

        public CustomerRegistrationContext(DbContextOptions<CustomerRegistrationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<LGA> LGAs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-F0FU8D3\\DEVELOPER;Database=CustomerOnboarding;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                //entity.Property(e => e.Lga)
                //    .IsRequired()
                //    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                //entity.Property(e => e.State)
                //    .IsRequired()
                //    .HasMaxLength(50)
                //    .IsUnicode(false);
            });
            modelBuilder.Entity<LGA>().HasOne(s => s.TBL_State).WithMany(s => s.TBL_Lgas).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<State>().HasIndex(e => e.StateName).IsUnique();
            modelBuilder.Entity<LGA>().HasIndex(e => e.LGAName).IsUnique();


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
