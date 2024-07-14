using Batch4.Api.RestaurantManagementSystem.BL.RequestModels;
using Microsoft.IdentityModel.Tokens;

namespace Batch4.Api.RestaurantManagementSystem.DA.Services.Tax;

public class DA_Tax
{
    private readonly AppDbContext _db;

    public DA_Tax(AppDbContext db)
    {
        _db = db;
    }

    public async Task<int> CreateTax(TaxRequestModel reqModel)
    {     
        if(reqModel.TaxRate == 0) return 0;
        Models.Tax tax = new Models.Tax()
        {
            TaxCode = GenerateTaxCode(reqModel.TaxDescription),
            TaxRate = reqModel.TaxRate,
            Description = reqModel.TaxDescription,
            CreatedDate = DateTime.Now,
        };
        _db.Taxes.Add(tax);
        int result = await _db.SaveChangesAsync();
        return result;
    }

    public async Task<List<Models.Tax>> GetAllTax()
    {
        List<Models.Tax> list = await _db.Taxes.ToListAsync();
        return list;
    }

    public async Task<Models.Tax> GetTaxById(int id)
    {
        Models.Tax? tax = await _db.Taxes.FirstOrDefaultAsync(x => x.TaxId == id);        
        return tax;
    }

    public async Task<Models.Tax> GetTaxByTaxCode(string taxCode)
    {
        Models.Tax? tax = await _db.Taxes.FirstOrDefaultAsync(x => x.TaxCode == taxCode);
        return tax;
    }
    public async Task<int> DeleteTax(int id)
    {
        Models.Tax tax = await GetTaxById(id);

        if (tax == null)
        {
            throw new InvalidDataException("no data found");
        }
        _db.Taxes.Remove(tax);
        int result = await _db.SaveChangesAsync();
        return result;
    }
    private string GenerateTaxCode(string name)
    {
        string prefix = "TAX_"+name.Trim().Substring(0, 3).ToUpper();

        Random rdn = new Random();
        string code = prefix + rdn.Next(100, 999).ToString();

        return code;
    }
}
