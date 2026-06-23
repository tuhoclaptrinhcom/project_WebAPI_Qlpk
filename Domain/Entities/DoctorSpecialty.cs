namespace App_QLPK.Domain.Entities;

public class DoctorSpecialty
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int SpecialtyId { get; set; }
    

    public Doctor Doctor { get; set; } = null!;
    public Specialty Specialty { get; set; } = null!;

}