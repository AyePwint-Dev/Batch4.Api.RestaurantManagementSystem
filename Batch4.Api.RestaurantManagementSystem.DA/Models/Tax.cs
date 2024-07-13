using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch4.Api.RestaurantManagementSystem.DA.Models
{
    [Table("Tbl_Tax")]
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
