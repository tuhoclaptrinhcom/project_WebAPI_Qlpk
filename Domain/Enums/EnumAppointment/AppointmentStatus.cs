namespace App_QLPK.Domain.Enums;

public enum AppointmentStatus
{
    Scheduled,  // Đã đặt lịch
    CheckIn,    // Đã đến 
    Complete,   // Đã khám xong
    Cancelled,  // Đã hủy
}