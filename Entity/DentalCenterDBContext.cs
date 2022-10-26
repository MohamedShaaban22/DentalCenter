using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class DentalCenterDBContext : DbContext
    {
        public DentalCenterDBContext()
        {
        }

        public DentalCenterDBContext(DbContextOptions<DentalCenterDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Blogdetail> Blogdetails { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Commonquestion> Commonquestions { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Doctorimage> Doctorimages { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<Happypatient> Happypatients { get; set; }
        public virtual DbSet<Homecounter> Homecounters { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Newsimage> Newsimages { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Serviceimage> Serviceimages { get; set; }
        public virtual DbSet<Subscribtion> Subscribtions { get; set; }
        public virtual DbSet<Testmonial> Testmonials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DentalCenterAPI.Utility.Utility.GetDatabaseConnectionstring());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.Adminid)
                    .HasColumnName("adminid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointment");

                entity.Property(e => e.Appointmentid)
                    .HasColumnName("appointmentid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Branchname)
                    .HasMaxLength(50)
                    .HasColumnName("branchname");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Pateintid).HasColumnName("pateintid");

                entity.Property(e => e.Reservationdate)
                    .HasColumnType("date")
                    .HasColumnName("reservationdate");

                entity.Property(e => e.Reservationtime).HasColumnName("reservationtime");

                entity.Property(e => e.Servicename)
                    .HasMaxLength(50)
                    .HasColumnName("servicename");

                entity.HasOne(d => d.Pateint)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Pateintid)
                    .HasConstraintName("FK_appointment_patient");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("blog");

                entity.Property(e => e.Blogid)
                    .HasColumnName("blogid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.Writter)
                    .HasMaxLength(50)
                    .HasColumnName("writter");
            });

            modelBuilder.Entity<Blogdetail>(entity =>
            {
                entity.HasKey(e => e.Detailsid);

                entity.ToTable("blogdetails");

                entity.Property(e => e.Detailsid)
                    .HasColumnName("detailsid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Blogid).HasColumnName("blogid");

                entity.Property(e => e.Detailsorder).HasColumnName("detailsorder");

                entity.Property(e => e.Discreption)
                    .HasMaxLength(300)
                    .HasColumnName("discreption");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Blogdetails)
                    .HasForeignKey(d => d.Blogid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_blogdetails_blog");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("branch");

                entity.Property(e => e.Branchid)
                    .HasColumnName("branchid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.Fromday)
                    .HasMaxLength(50)
                    .HasColumnName("fromday");

                entity.Property(e => e.Fromhour)
                    .HasMaxLength(50)
                    .HasColumnName("fromhour");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ismain).HasColumnName("ismain");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(50)
                    .HasColumnName("phonenumber");

                entity.Property(e => e.Textnumber)
                    .HasMaxLength(50)
                    .HasColumnName("textnumber");

                entity.Property(e => e.Today)
                    .HasMaxLength(50)
                    .HasColumnName("today");

                entity.Property(e => e.Tohour)
                    .HasMaxLength(10)
                    .HasColumnName("tohour")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Commonquestion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("commonquestion");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer");

                entity.Property(e => e.Commonquestionid)
                    .HasColumnName("commonquestionid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnName("question");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("doctor");

                entity.Property(e => e.Doctorid)
                    .HasColumnName("doctorid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aboutdoctor)
                    .HasMaxLength(500)
                    .HasColumnName("aboutdoctor");

                entity.Property(e => e.Discription)
                    .HasMaxLength(500)
                    .HasColumnName("discription");

                entity.Property(e => e.Facebookurl)
                    .HasMaxLength(200)
                    .HasColumnName("facebookurl");

                entity.Property(e => e.Homedisplay).HasColumnName("homedisplay");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Instgramurl)
                    .HasMaxLength(200)
                    .HasColumnName("instgramurl");

                entity.Property(e => e.Linkedin)
                    .HasMaxLength(200)
                    .HasColumnName("linkedin");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Referalnumber)
                    .HasMaxLength(50)
                    .HasColumnName("referalnumber");

                entity.Property(e => e.Tiktokurl)
                    .HasMaxLength(200)
                    .HasColumnName("tiktokurl");

                entity.Property(e => e.Videopath)
                    .HasMaxLength(200)
                    .HasColumnName("videopath");
            });

            modelBuilder.Entity<Doctorimage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("doctorimage");

                entity.Property(e => e.Doctorid).HasColumnName("doctorid");

                entity.Property(e => e.Doctorimageid)
                    .HasColumnName("doctorimageid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(500)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Doctor)
                    .WithMany()
                    .HasForeignKey(d => d.Doctorid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_doctorimage_doctor");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.ToTable("gallery");

                entity.Property(e => e.Galleryid)
                    .HasColumnName("galleryid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Happypatient>(entity =>
            {
                entity.ToTable("happypatient");

                entity.Property(e => e.Happypatientid)
                    .HasColumnName("happypatientid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Isfavorite).HasColumnName("isfavorite");

                entity.Property(e => e.Videopath)
                    .HasMaxLength(200)
                    .HasColumnName("videopath");
            });

            modelBuilder.Entity<Homecounter>(entity =>
            {
                entity.ToTable("homecounter");

                entity.Property(e => e.Homecounterid)
                    .HasColumnName("homecounterid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Branchescount).HasColumnName("branchescount");

                entity.Property(e => e.Doctorscount).HasColumnName("doctorscount");

                entity.Property(e => e.Expyearscount).HasColumnName("expyearscount");

                entity.Property(e => e.Happypatientscount).HasColumnName("happypatientscount");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");

                entity.Property(e => e.Newsid)
                    .HasColumnName("newsid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Discription)
                    .HasMaxLength(300)
                    .HasColumnName("discription");

                entity.Property(e => e.Firstdetails)
                    .HasMaxLength(500)
                    .HasColumnName("firstdetails");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Seconddetails)
                    .HasMaxLength(500)
                    .HasColumnName("seconddetails");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");

                entity.Property(e => e.Writter)
                    .HasMaxLength(50)
                    .HasColumnName("writter");
            });

            modelBuilder.Entity<Newsimage>(entity =>
            {
                entity.HasKey(e => e.Imageid);

                entity.ToTable("newsimage");

                entity.Property(e => e.Imageid)
                    .HasColumnName("imageid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Imagepath)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Newsid).HasColumnName("newsid");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.Newsimages)
                    .HasForeignKey(d => d.Newsid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_newsimage_news");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                entity.Property(e => e.Patientid)
                    .HasColumnName("patientid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("date")
                    .HasColumnName("birthdate");

                entity.Property(e => e.Contactmethod)
                    .HasMaxLength(50)
                    .HasColumnName("contactmethod");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Isnewuser).HasColumnName("isnewuser");

                entity.Property(e => e.Message)
                    .HasMaxLength(50)
                    .HasColumnName("message");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(50)
                    .HasColumnName("phonenumber");
            });

            modelBuilder.Entity<Serviceimage>(entity =>
            {
                entity.HasKey(e => e.Serviceimagesid);

                entity.ToTable("serviceimages");

                entity.Property(e => e.Serviceimagesid)
                    .HasColumnName("serviceimagesid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Afterimagepath)
                    .HasMaxLength(200)
                    .HasColumnName("afterimagepath");

                entity.Property(e => e.Beforeimagepath)
                    .HasMaxLength(200)
                    .HasColumnName("beforeimagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Servicename)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("servicename");
            });

            modelBuilder.Entity<Subscribtion>(entity =>
            {
                entity.ToTable("subscribtion");

                entity.Property(e => e.Subscribtionid)
                    .HasColumnName("subscribtionid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Testmonial>(entity =>
            {
                entity.ToTable("testmonial");

                entity.Property(e => e.Testmonialid)
                    .HasColumnName("testmonialid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Insertdate)
                    .HasColumnType("datetime")
                    .HasColumnName("insertdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quote)
                    .HasMaxLength(300)
                    .HasColumnName("quote");

                entity.Property(e => e.Videopath)
                    .HasMaxLength(250)
                    .HasColumnName("videopath");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
