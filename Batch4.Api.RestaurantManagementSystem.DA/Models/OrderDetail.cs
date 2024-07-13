namespace Batch4.Api.RestaurantManagementSystem.DA.Models;

[Table("Tbl_OrderDetail")]
public class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }   

    public int ItemId { get; set; } //FK

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; } //FK
    
}
