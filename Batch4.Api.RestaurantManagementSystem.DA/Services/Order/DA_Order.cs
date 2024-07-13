using Batch4.Api.RestaurantManagementSystem.Shared;
using Batch4.Api.RestaurantManagementSystem.Shared.Models.Order;
using Batch4.Api.RestaurantManagementSystem.Shared.Queries;

namespace Batch4.Api.RestaurantManagementSystem.DA.Services.Order;

public class DA_Order
{
    private readonly AppDbContext _db;
    private readonly DapperService _dapper;

    public DA_Order(AppDbContext db, DapperService dapper)
    {
        _db = db;
        _dapper = dapper;
    }

    public async Task<OrderResponseModel> CreateOrder(OrderRequestModel orderRequest)
    {   
        OrderResponseModel model = new OrderResponseModel();  
        List<OrderDetail> orderDetailLst = new List<OrderDetail>();
        Models.Order order = new Models.Order();
        var tax = new Tax();

        decimal totalPrice = 0;
        var invoiceNo = "INV" + DateTime.Now.ToString("yyyMMddHHmmss");
        string currentTax = "TAX001";
        
        order.InvoiceNo = invoiceNo;
        order.CreatedDate = DateTime.Now;
        await _db.Orders.AddAsync(order);
        await _db.SaveChangesAsync();        

        var orderObj = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == order.OrderId);
        if (orderObj is not null)
        {
            foreach (var item in orderRequest.Items)
            {
                OrderResponseModel orderRespondModel = new OrderResponseModel();
                OrderDetail detailModel = new OrderDetail();

                var menu = await _db.MenuItems.FirstOrDefaultAsync(x => x.ItemId == item.ItemId);
                tax = await _db.Taxes.FirstOrDefaultAsync(x => x.TaxCode == currentTax);
                if (menu is not null)
                {
                    detailModel.ItemId = item.ItemId;
                    detailModel.Quantity = item.Quantity;
                    detailModel.UnitPrice = menu.ItemPrice;
                    detailModel.OrderId = orderObj.OrderId;
                    totalPrice = totalPrice + (item.Quantity * detailModel.UnitPrice);
                }
                orderDetailLst.Add(detailModel);
            }
        
        if (orderDetailLst.Count == 0) return model;       
        
            if (tax is not null)
            {
                orderObj.TaxId = tax.TaxId;
            }

            orderObj.TotalPrice = totalPrice;
            orderObj.OrderDetails = orderDetailLst;
            orderObj.CreatedDate = DateTime.Now;            
        }
        await _db.OrderDetails.AddRangeAsync(orderDetailLst);       
        int result = await _db.SaveChangesAsync();       
        if (result > 0)
        {
            model.InvoiceNo = invoiceNo;
            model.TotalPrice = totalPrice;
        }
        return model;
    }

    public async Task<OrderDetailResponseModel> ViewOrder(string invoiceNo)
    {
        OrderDetailResponseModel model = new OrderDetailResponseModel();
        var order = await _db.Orders.FirstOrDefaultAsync(x => x.InvoiceNo == invoiceNo);
        if (order == null) return model;
        model.InvoiceNo = order.InvoiceNo;
        model.TotalPrice = order.TotalPrice;

        var orderDetail = _dapper.Query<OrderItemDetailModel>(OrderQuery.OrderDetailQuery, new { InvoiceNo = invoiceNo });
        model.Items = orderDetail;
        return model;
    }

    public async Task<List<Models.Order>> ViewOrders()
    {
        List<Models.Order> list = await _db.Orders.ToListAsync();
        return list;
    }
    public async Task<List<OrderBillRespondModel>> ViewOrderBill(string invoiceNo)
    {        
        List<OrderBillRespondModel> model = new List<OrderBillRespondModel>();
        //InvoiceNo. TotalPrice, TaxRate, TaxAmount,TotalBillAmount
        var orderBill = _dapper.Query<OrderBillRespondModel>(OrderQuery.OrderBillingQuery, new { InvoiceNo = invoiceNo });
        if(orderBill is not null) {
            model = orderBill;
        }        
        return model;
    }
}
