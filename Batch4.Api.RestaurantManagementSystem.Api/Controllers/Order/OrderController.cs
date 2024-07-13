﻿using Batch4.Api.RestaurantManagementSystem.Shared;

namespace Batch4.Api.RestaurantManagementSystem.Api.Controllers.Order;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly BL_Order _blOrder;

    public OrderController(BL_Order blOrder)
    {
        _blOrder = blOrder;
    }    
    [HttpPost]
    public async Task<IActionResult> Create(OrderRequestModel orderRequest)
    {
        var model = await _blOrder.CreateOrder(orderRequest);
        if (model.InvoiceNo == null) return Ok("Order Creation Fail.");
        return Ok(model);
    }    
    [HttpGet("invoiceNo")]
    public async Task<IActionResult> OrderDetail(string invoiceNo)
    {
        var model = await _blOrder.ViewOrder(invoiceNo);
        if (model.InvoiceNo == null) return Ok("No order found.");
        return Ok(model);
    }

    [HttpGet]
    public async Task<IActionResult> ViewOrders()
    {
        var model = await _blOrder.ViewOrders();
        return Ok(model);
    }

    [HttpGet("ViewOrderBill")]    
    public async Task<IActionResult> ViewOrderBill(string invoiceNo)
    {
        var model = await _blOrder.ViewOrderBill(invoiceNo);
        if (model is null) return Ok("No Bill found.");
        return Ok(model);
    }
}
