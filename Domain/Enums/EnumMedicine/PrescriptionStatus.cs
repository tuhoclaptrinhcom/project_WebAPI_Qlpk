namespace App_QLPK.Domain.Enums;

public enum PrescriptionStatus
{
    
    Draft,        // đang tạo
    Prescribed,   // đã kê
    Dispensed,    // đã phát thuốc
    Completed,    // hoàn tất
    Cancelled     // đã hủy
}