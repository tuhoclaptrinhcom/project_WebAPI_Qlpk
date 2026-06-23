namespace App_QLPK.Application.DTO.Appointments;

/// <summary>
///        Dữ liệu lịch hẹn trả về cho client
/// </summary>
public class AppointmentDTO
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set;} = "";
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = "";
    public DateTime AppointmentTime { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = "" ; 
    public DateTime? CheckInTime { get; set;}
    public DateTime? CompleteAt { get; set;}
    public string? CancelReason { get; set;}
    public DateTime CreatedAt { get; set;}
}