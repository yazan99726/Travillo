using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication4.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Accountenst> Accountensts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<EmpContAdd> EmpContAdds { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Paymant> Paymants { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=train_user252;PASSWORD=oday1234;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TRAIN_USER252")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Accountenst>(entity =>
            {
                entity.ToTable("ACCOUNTENST");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Adress)
                    .HasMaxLength(50)
                    .HasColumnName("ADRESS");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Salary)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SALARY");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("ADMIN");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Adress)
                    .HasMaxLength(50)
                    .HasColumnName("ADRESS");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("CLIENT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Adress)
                    .HasMaxLength(50)
                    .HasColumnName("ADRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("CONTACT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Expdate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPDATE");

                entity.Property(e => e.IdCus)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ID_CUS");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.IdCusNavigation)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.IdCus)
                    .HasConstraintName("FK_CL");
            });

            modelBuilder.Entity<EmpContAdd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("EMP_CONT_ADD");

                entity.Property(e => e.AddName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ADD_NAME");

                entity.Property(e => e.AddresId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ADDRES_ID");

                entity.Property(e => e.ContId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONT_ID");

                entity.Property(e => e.EmpId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EMP_ID");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EMP_NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("IMAGE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.TripId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TRIP_ID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_TRIP");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Accountid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ACCOUNTID");

                entity.Property(e => e.Adminid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ADMINID");

                entity.Property(e => e.Clientid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CLIENTID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Rolid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Accountid)
                    .HasConstraintName("FK_ACC");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Adminid)
                    .HasConstraintName("FK_ADM");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Clientid)
                    .HasConstraintName("FK_CLIE");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Rolid)
                    .HasConstraintName("FK_ROL");
            });

            modelBuilder.Entity<Paymant>(entity =>
            {
                entity.HasKey(e => e.CardId)
                    .HasName("SYS_C00103007");

                entity.ToTable("PAYMANT");

                entity.Property(e => e.CardId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CARD_ID");

                entity.Property(e => e.Cardnumber)
                    .HasMaxLength(20)
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Cardtype)
                    .HasMaxLength(50)
                    .HasColumnName("CARDTYPE");

                entity.Property(e => e.Cvv)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CVV");

                entity.Property(e => e.Expdate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPDATE");

                entity.Property(e => e.IdCus)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ID_CUS");

                entity.HasOne(d => d.IdCusNavigation)
                    .WithMany(p => p.Paymants)
                    .HasForeignKey(d => d.IdCus)
                    .HasConstraintName("FK_CLIENT");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("RESERVATION");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Customerid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTOMERID");

                entity.Property(e => e.Tripid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TRIPID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("FK_T");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Tripid)
                    .HasConstraintName("FK_C");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Accept)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ACCEPT");

                entity.Property(e => e.Rate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RATE");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IdCus)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ID_CUS");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .HasColumnName("IMAGEPATH");

                entity.HasOne(d => d.IdCusNavigation)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.IdCus)
                    .HasConstraintName("FK_CUSTO");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("TRIP");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Datefrom)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEFROM");

                entity.Property(e => e.Dateto)
                    .HasColumnType("DATE")
                    .HasColumnName("DATETO");

                entity.Property(e => e.Descriptoin)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTOIN");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Itinerary)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ITINERARY");

                entity.Property(e => e.Price)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRICE");

                entity.Property(e => e.Cost)
                  .HasMaxLength(10)
                  .IsUnicode(false)
                  .HasColumnName("COST");

                entity.Property(e => e.Tripname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TRIPNAME");
            });

            modelBuilder.HasSequence("SQ_S");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
