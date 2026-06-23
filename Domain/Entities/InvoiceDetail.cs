using App_QLPK.Domain.Enums;

namespace App_QLPK.Domain.Entities;

public class InvoiceDetail
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }

    public InvoiceDetailType Type { get; set; } // dùng để lưu dòng tiền của từng loại : thuốc , dịch vụ , tiền khám
    public int ReferenceId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }

    public Invoice Invoice { get; set; } = null!;

}