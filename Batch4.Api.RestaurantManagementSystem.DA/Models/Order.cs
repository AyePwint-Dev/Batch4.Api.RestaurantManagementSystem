namespace Batch4.Api.RestaurantManagementSystem.DA.Models;

[Table("Tbl_Order")]
public class Order
{
    [Key]
    public int OrderId { get; set; }
    //FK TaxId
    public int TaxId { get; set; }
    public string InvoiceNo { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }        
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }   
}
