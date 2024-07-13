namespace Batch4.Api.RestaurantManagementSystem.Shared.Queries;

public static class OrderQuery
{
    public static string OrderDetailQuery { get; } = @"select mi.ItemName,mi.ItemPrice,od.Quantity
                                                       from Tbl_MenuItem mi ,Tbl_OrderDetail od, Tbl_Order o
                                                       where mi.ItemId=od.ItemId AND od.OrderId=o.OrderId
                                                       AND o.InvoiceNo=@InvoiceNo";

    public static string OrderBillingQuery { get; } = @"SELECT 
                                                        o.InvoiceNo,
                                                        o.TotalPrice,
                                                        t.TaxRate,
                                                        (o.TotalPrice * t.TaxRate / 100) AS TaxAmount,
                                                        (o.TotalPrice + (o.TotalPrice * t.TaxRate / 100)) AS TotalBillAmount,
                                                        o.CreatedDate
                                                    FROM 
                                                        Tbl_Order o
                                                    JOIN 
                                                        Tbl_Tax t ON o.TaxId = t.TaxId
                                                    WHERE 
                                                        o.InvoiceNo = @InvoiceNo";
}
