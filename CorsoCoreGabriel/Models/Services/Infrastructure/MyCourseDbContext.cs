using System;
using CorsoCoreGabriel.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CorsoCoreGabriel.Models.Services.Infrastructure
{
    public partial class MyCourseDbContext : DbContext
    {
  

        public MyCourseDbContext(DbContextOptions<MyCourseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }


        //Solo se non si usa add db context pool in startup configure services
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlite("Data Source=Data/MyCourse.db");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Course>(entity =>
            {

                entity.ToTable("Courses"); //Superfluo se la tabella si chiama come la propieta
                entity.HasKey(course => course.Id); //Superfluo se la propietà si chiama Id oppure CourseId
                //entity.HasKey(course => new { course.Id,course.Author }); chiave primaria composta

                //mapping owned types
                entity.OwnsOne(course => course.CurrentPrice, builder =>
                {

                    builder.Property(money => money.Currency).HasConversion<string>().HasColumnName("CurrentPrice_Currency"); //Superfluo perchè le colonne eseguono già la convezione
                    builder.Property(money => money.Amount).HasColumnName("CurrentPrice_Amount");//Superfluo perchè le colonne eseguono già la convezione

                });
                //versione ridotta
                entity.OwnsOne(course => course.FullPrice);


                //Mapping per le relazioni
                entity.HasMany(course => course.Lessons)
                      .WithOne(lesson => lesson.Course);
                     // .HasForeignKey(lesson => lesson.CourseId);// è superflua se la propieta si chiama CourseId 


                #region Mapping automatico reverse engineering

                /*
                entity.HasIndex(e => e.Title)
                    .HasName("ux_title")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.Property(e => e.CurrentPriceAmount)
                    .IsRequired()
                    .HasColumnName("CurrentPrice_Amount")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CurrentPriceCurrency)
                    .IsRequired()
                    .HasColumnName("CurrentPrice_Currency")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Email).HasColumnType("TEXT (100)");

                entity.Property(e => e.FullPriceAmount)
                    .IsRequired()
                    .HasColumnName("FullPrice_Amount")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FullPriceCurrency)
                    .IsRequired()
                    .HasColumnName("FullPrice_Currency")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.ImagePath).HasColumnType("TEXT (100)");

                entity.Property(e => e.RowVersion).HasColumnType("DATETIME");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");*/
                #endregion
            });

            ////non è neccesario perchè è già fatto prima 
            //modelBuilder.Entity<Lesson>(entity =>
            //{
            //    entity.HasOne(lesson => lesson.Course).WithMany(course => course.Lessons);

            //    #region Mapping automatico reverse engineering

            //    /*
            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

            //    entity.Property(e => e.Duration)
            //        .IsRequired()
            //        .HasColumnType("TEXT (8)")
            //        .HasDefaultValueSql("'00:00:00'");

            //    entity.Property(e => e.Title)
            //        .IsRequired()
            //        .HasColumnType("TEXT (100)");

            //    entity.HasOne(d => d.Course)
            //        .WithMany(p => p.Lessons)
            //        .HasForeignKey(d => d.CourseId);
            //    */
            //    #endregion
            //});
        }
    }
}
