namespace App_QLPK.Domain.Entities;

/// <summary>
/// Bảng trung gian giữa Medicine - Prescription
/// </summary> 
public class PrescriptionDetail
{
    public int Id { get; set;}
    public int PrescriptionId { get;}
    public int MedicineId { get; set;}

    public string Dosage { get; set;} = null!;  // liều dùng
    public string Frequency { get; set;} = null!; // tần suất sử dụng (viên/ngày , 25ml/lần)
    public string? Duration { get; set;} // sử dụng trong bao lâu
    public string? Instructions { get; set;} // hướng dẫn sử dụng

    public int Quantity { get; set;}
    public decimal UnitPrice { get; set;}

    public Medicine Medicine { get; set;} = null!;
    public Prescription  Prescription { get; set;} = null!;
}