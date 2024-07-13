namespace Batch4.Api.RestaurantManagementSystem.DA.Models;

public class OrderList
{
    public Order Order { get; set; }
    public List<OrderDetail> Details { get; set; } = new List<OrderDetail>();
}
