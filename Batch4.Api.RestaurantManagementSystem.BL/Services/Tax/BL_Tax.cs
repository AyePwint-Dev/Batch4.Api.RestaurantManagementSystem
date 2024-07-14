using Batch4.Api.RestaurantManagementSystem.DA.Services.Tax;

namespace Batch4.Api.RestaurantManagementSystem.BL.Services.Tax;

public class BL_Tax
{
    private readonly DA_Tax _daTax;

    public BL_Tax(DA_Tax daTax)
    {
        _daTax = daTax;
    }

    public async Task<int> CreateTax(TaxRequestModel tax)
    {        
        var result = await _daTax.CreateTax(tax);
        return result;
    }

    public async Task<List<DA.Models.Tax>> GetAllTax()
    {
        return await _daTax.GetAllTax();
    }

    public async Task<DA.Models.Tax> GetTaxById(int id)
    {
        var category = await _daTax.GetTaxById(id);
        if (category == null) throw new InvalidDataException("no data found");
        return category;
    }

    public async Task<DA.Models.Tax> GetTaxByTaxCode(string code)
    {
        var category = await _daTax.GetTaxByTaxCode(code);
        return category;
    }

    public async Task<int> DeleteTax(int id)
    {
        return await _daTax.DeleteTax(id);
    }
    
}
