namespace App_QLPK.Domain.Entities;


public class ServiceResult
{
    public int Id { get; set; }
    public int ServiceOrderId { get; set; }
    public int TestByDoctorId { get; set; }
    

    
    public string? ResultText { get; set; }
    public string? Conclusion { get; set; }
    public string? FileUrl { get; set; }
    public DateTime CreatedAt { get; set; }


    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Doctor TestByDoctor { get; set; } = null!;
    public ICollection<ServiceResultDetail> ServiceResultDetails { get; set; } = new List<ServiceResultDetail>();
}