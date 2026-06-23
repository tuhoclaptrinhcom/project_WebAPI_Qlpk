namespace App_QLPK.Domain.Entities;

public class ServiceResultDetail
{
    public int Id { get; set;}
    public int ServiceResultId { get; set;}
    
    public string ItemName { get; set;} = null!;
    public string ItemValue { get; set;} = null!; //chỉ số : xét nghiệm
    public string? NormalRange { get; set;} // chỉ số bình thường
    public string? AbnormalFlag { get; set;} // chỉ số bất thường

    public ServiceResult ServiceResult{ get; set;} = null!;
}