using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetData_TTDN
{
    public partial class DataContext : DbContext
    {
        
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public virtual DbSet<ThongTin> ThongTins { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("Appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            var connect = config.GetSection("connect").Get<MyService.connect>();

            if (!optionsBuilder.IsConfigured)
            {
                object p = optionsBuilder.UseSqlServer(connect.source);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ThongTin>(entity =>
            {
                entity.ToTable("ThongTin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
                entity.Property(e => e.HienTai)
                 .HasMaxLength(50)
                 .HasColumnName("hienTai");

                entity.Property(e => e.CongSuatLn)
                    .HasMaxLength(50)
                    .HasColumnName("congSuatLonNhat");

                entity.Property(e => e.ThietKe)
                    .HasMaxLength(50)
                    .HasColumnName("thietKe");

                entity.Property(e => e.SanLuongNgay)
                    .HasMaxLength(50)
                    .HasColumnName("sanLuongNgay");

                entity.Property(e => e.Time)
                    .HasMaxLength(50)
                    .HasColumnName("time");


            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

