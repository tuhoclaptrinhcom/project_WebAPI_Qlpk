using App_QLPK.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App_QLPK.Infrastructure.Persistence;


public partial class QlpkDbContext : DbContext
{
    public QlpkDbContext(DbContextOptions<QlpkDbContext> options) : base(options)
    {
    }

    // ===== KHAI BÁO BẢNG TẠO DbContext để EF : kết nối giữa code và database  =====
        // DbSet<> : là đại diện cho bảng trong database

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
    public virtual DbSet<Specialty> Specialties { get; set; }
    public virtual DbSet<Invoice> Invoices { get; set; }
    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<Medicine> Medicines { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<Prescription> Prescriptions { get; set; }
    public virtual DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<ServiceOrder> ServiceOrders { get; set; }
    public virtual DbSet<ServiceResult> ServiceResults { get; set; }
    public virtual DbSet<ServiceResultDetail> ServiceResultDetails { get; set; }
    public virtual DbSet<Role> Roles { get; set; }


    // ========= CẤU HÌNH FLUENT API : để xác định độ dài và kiểu cột =========

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Appointm_2368EC88CD891A3");

            // đánh Index (tạo mục lục) để tối ưu khi tìm lịch theo bác sĩ và bệnh nhân
            entity.HasIndex(e => e.DoctorId, "Index_Appointments_DoctorId");
            entity.HasIndex(e => new { e.DoctorId, e.AppointmentTime }, "Index_Appointments_Doctor_Time");
            entity.HasIndex(e => e.PatientId, "Index_Appointments_PatientId");

            entity.Property(e => e.CancelReason).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(e => e.Patient).WithMany(e => e.Appointments)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Patients");
            entity.HasOne(e => e.Doctor).WithMany(e => e.Appointments)
                .HasForeignKey(e => e.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                /*
                    .ClientSetNull : cho xóa ,nhưng EF sẽ set FK = null cho các entity con (chỉ khi FK cho phép null) -> sau đó xóa parent
                        vd: public int? RoleId { get; set; }


                    .Restrict : nếu có dữ liệu con -> Db sẽ chặn không cho xóa
                        vd : Role -> User 
                */

                .HasConstraintName("FK_Appointments_Doctors");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Doctors_3214EC07FF59A740");

            entity.HasIndex(e => e.UserId, "UQ_Doctors_1788CC4D23969187").IsUnique();
            entity.HasIndex(e => e.LicenseNumber, "UQ_DOctors_E889016661E63CAF").IsUnique();

            entity.Property(e => e.Biography).HasMaxLength(255);
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(e => e.User).WithOne(e => e.Doctor)
                .HasForeignKey<Doctor>(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctors_Users");
        });


        modelBuilder.Entity<DoctorSpecialty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DoctorSp_214EC0718E86EB0");

            entity.HasOne(e => e.Specialty).WithMany(e => e.DoctorSpecialties)
                .HasForeignKey(e => e.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorSpecialties_Specialties");
            entity.HasOne(e => e.Doctor).WithMany(e => e.DoctorSpecialties)
                .HasForeignKey(e => e.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorSpecialties_Doctors");
        });


        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Invoices_3214EC074A717040");

            entity.HasIndex(e => e.InvoiceNumber, "UQ_Invoices_D776E98106A36154").IsUnique();

            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10,2)");

            entity.HasOne(e => e.Appointment).WithMany(e => e.Invoices)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Appointments");

            entity.HasOne(e => e.Patient).WithMany(e => e.Invoices)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Patients");
        });


        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InvoiceD_3214EC074DDE14D2");

            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");

            entity.HasOne(e => e.Invoice).WithMany(e => e.InvoiceDetails)
                .HasForeignKey(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetails_Invoices");
        });


        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MedicalR_3214EC07027D1A9C");

            entity.HasIndex(e => e.AppointmentId, "UQ_MedicalR_8ECDFCC33FA7AEBF").IsUnique();

            entity.Property(e => e.Diagnosis).HasMaxLength(150);
            entity.Property(e => e.Symptom).HasMaxLength(150);
            entity.Property(e => e.Conclusion).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(255);

            entity.HasOne(e => e.Appointment).WithOne(e => e.MedicalRecord)
                .HasForeignKey<MedicalRecord>(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecords_Appointments");
        });


        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Medicine_3214EC078E1D40EC");

            entity.HasIndex(e => e.Code, "UQ_Medicine_A25C5AA74131C8A8").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MinStock).HasDefaultValue(10);
            entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
        });


        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Patients__3214EC0783EB5B32");

            entity.HasIndex(e => e.InsuranceNumber, "UQ_Patients_01A4DDAC09A0DB1B").IsUnique();
            entity.HasIndex(e => e.UserId, "UQ_Patients_1788CC4D257182D3").IsUnique();

            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.BloodType).HasMaxLength(10);
            entity.Property(e => e.EmergencyName).HasMaxLength(50);
            entity.Property(e => e.EmergencyPhone).HasMaxLength(20);
            entity.Property(e => e.InsuranceNumber).HasMaxLength(50);

            entity.HasOne(e => e.User).WithOne(e => e.Patient)
                .HasForeignKey<Patient>(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull) // cho xóa 
                .HasConstraintName("FK_Patients_Users");
        
        });


        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Payments_3214EC071B678636");

            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");

            entity.HasOne(e => e.Invoice).WithMany(e => e.Payments)
                .HasForeignKey(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Invoices");

        });


        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Prescrip_3214EC07CC0CDF3A");

            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(150);

            entity.HasOne(e => e.MedicalRecord).WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescriptions_MedicalRecords");
        });


        modelBuilder.Entity<PrescriptionDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PrescripD_3214EC07CC0CDF3A");

            entity.Property(e => e.Dosage).HasMaxLength(50);
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.Instructions).HasMaxLength(50);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");

            entity.HasOne(e => e.Medicine).WithMany(e => e.PrescriptionDetails)
                .HasForeignKey(e => e.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionDetails_Medicines");  
            
            entity.HasOne(e => e.Prescription).WithMany(e => e.PrescriptionDetails)
                .HasForeignKey(e => e.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionDetails_Prescriptions");
        });


        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_3214EC07DF74547F");

            entity.HasIndex(e => e.Code, "UQ_Roles_A25C5AA705467315").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(150);
        });


        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Services_3214EC07B3A11096");

            entity.HasIndex(e => e.Code, "UQ_Services_A25C5AA750045F12").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
            
        });


        modelBuilder.Entity<ServiceOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ServiceO_3214EC07158449E1");

            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(150);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");

            entity.HasOne(e => e.MedicalRecord).WithMany(e => e.ServiceOrders)
                .HasForeignKey(e => e.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceOrders_MedicalRecords");
            
            entity.HasOne(e => e.Service).WithMany(e => e.ServiceOrders)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceOrders_Services");

            entity.HasOne(e => e.OrderByDoctor).WithMany(e => e.ServiceOrders)
                .HasForeignKey(e => e.OrderByDoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceOrders_Doctors");
        });


        modelBuilder.Entity<ServiceResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ServiceR_3214EC07193A0356");

            entity.Property(e => e.Conclusion).HasMaxLength(255);
            entity.Property(e => e.ResultText).HasMaxLength(255);
            entity.Property(e => e.FileUrl).HasMaxLength(255);

            entity.HasOne(e => e.ServiceOrder).WithMany(e => e.ServiceResults)
                .HasForeignKey(e => e.ServiceOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceResults_ServiceOrders");

            entity.HasOne(e => e.TestByDoctor).WithMany(e => e.ServiceResults)
                .HasForeignKey(e => e.TestByDoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceResults_Doctors");
        });


        modelBuilder.Entity<ServiceResultDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ServiceRD_3214EC07EDCDD072");

            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.ItemValue).HasMaxLength(100);
            entity.Property(e => e.NormalRange).HasMaxLength(100);
            entity.Property(e => e.AbnormalFlag).HasMaxLength(20);

            entity.HasOne(e => e.ServiceResult).WithMany(e => e.ServiceResultDetails)
                .HasForeignKey(e => e.ServiceResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceResultDetails_ServiceResults");
        });


        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Specialty_3214EC07876F1114");
            entity.HasIndex(e => e.Name, "UQ_Specialty_737584F6E052D4DD").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(150);

        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users_3214EC071D0E9DF9");

            entity.HasIndex(e => e.Username, "UQ_Users_536C85E41E35ADEA").IsUnique();
            entity.HasIndex(e => e.Phone, "UQ_Users_5C7E359E1BB1B968").IsUnique();
            entity.HasIndex(e => e.Email, "UQ_Users_A9D1053443088778").IsUnique();

            entity.Property(e => e.Username).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Alias).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Address).HasMaxLength(255);

            entity.HasOne(e => e.Role).WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        base.OnModelCreating(modelBuilder);
    }
}