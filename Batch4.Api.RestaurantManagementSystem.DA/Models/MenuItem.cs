namespace Batch4.Api.RestaurantManagementSystem.DA.Models;

[Table("Tbl_MenuItem")]
public class MenuItem
{
    [Key]
    public int ItemId { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }        
    public decimal ItemPrice { get; set; }
    public string CategoryCode { get; set; }
    public DateTime CreatedDate { get; set; }

}
