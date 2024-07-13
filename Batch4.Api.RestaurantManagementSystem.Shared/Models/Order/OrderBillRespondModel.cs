using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch4.Api.RestaurantManagementSystem.Shared.Models.Order
{
    public class OrderBillRespondModel
    {
        //public List<OrderItemDetailModel> Items { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalBillAmount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class OrderBillRespondModelList{
        public List<OrderBillRespondModel> ItemsList { get; set; }
    }
}
