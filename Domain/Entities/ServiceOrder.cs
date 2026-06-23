using App_QLPK.Domain.Enums;

namespace App_QLPK.Domain.Entities;


public class ServiceOrder
{
    public int Id { get; set;}
    public int MedicalRecordId { get; set;}
    public int ServiceId { get; set;}
    public int OrderByDoctorId { get; set;}

    public ServiceStatus Status { get; set;} = ServiceStatus.Pending;

    public decimal UnitPrice { get; set;}
    public string? Notes { get; set;}
    public DateTime CreatedAt { get; set;}
    public DateTime? UpdatedAt { get; set;}

    public MedicalRecord MedicalRecord { get; set;} = null!;
    public Doctor OrderByDoctor { get; set;} = null!;
    public Service Service { get; set; } = null!;
    public ICollection<ServiceResult> ServiceResults { get; set; } = new List<ServiceResult>();

}