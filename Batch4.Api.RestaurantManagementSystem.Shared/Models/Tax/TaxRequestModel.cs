namespace Batch4.Api.RestaurantManagementSystem.BL.RequestModels;

public  class TaxRequestModel
{
    public string TaxCode { get; set; }
    public decimal TaxRate { get; set; } 
   public string TaxDescription { get; set; }
}
