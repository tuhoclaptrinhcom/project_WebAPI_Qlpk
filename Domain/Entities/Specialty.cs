namespace App_QLPK.Domain.Entities;

public class Specialty
{
    public int Id { get; set;}
    public string Name { get; set;} = null!;
    public string? Description { get; set;}

    public ICollection<DoctorSpecialty> DoctorSpecialties { get; set;} = new List<DoctorSpecialty>();

}