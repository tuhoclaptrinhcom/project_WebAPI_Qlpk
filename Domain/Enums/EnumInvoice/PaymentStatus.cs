namespace App_QLPK.Domain.Enums;

public enum PaymentStatus
{
    Pending,    // chờ xử lý
    Success,    // thanh toán thành công
    Failed,     // thanh toán thất bại(lỗi , sai OTP, hết tiền,...)
    Cancelled,  // do người dùng hủy (tài khoản không đủ, đổi ý muốn trả tiền mặt, hóa đơn đơn thuốc đã có nhưng đổi ý không mua,...)
    Refunded,   // hoàn tiền 
}