using Batch4.Api.RestaurantManagementSystem.BL.RequestModels;
using Microsoft.IdentityModel.Tokens;

namespace Batch4.Api.RestaurantManagementSystem.DA.Services.Category;

public class DA_Category
{
    private readonly AppDbContext _db;

    public DA_Category(AppDbContext db)
    {
        _db = db;
    }

    public async Task<int> CreateCategory(CategoryRequestModel reqModel)
    {
        if (reqModel.CategoryName.IsNullOrEmpty()) return 0;
        Models.Category category = new Models.Category()
        {
            CategoryName = reqModel.CategoryName.Trim().ToUpper(),
            CategoryCode = GenerateCode(reqModel.CategoryName)
        };
        _db.Categories.Add(category);
        int result = await _db.SaveChangesAsync();
        return result;
    }

    public async Task<List<Models.Category>> GetAllCategories()
    {
        List<Models.Category> list = await _db.Categories.ToListAsync();
        return list;
    }

    public async Task<Models.Category> GetCategoryById(int id)
    {
        Models.Category? category = await _db.Categories.FirstOrDefaultAsync(x => x.CategoryId ==  id);
        return category;
    }

    public async Task<Models.Category> GetCategoryByCode(string code)
    {
        Models.Category? category = await _db.Categories.FirstOrDefaultAsync(x => x.CategoryCode == code);
        return category;
    }

    public async Task<int> DeleteCategory(int id)
    {
        Models.Category category = await GetCategoryById(id);
        if (category == null) throw new InvalidDataException("no data found");

        _db.Categories.Remove(category);

        int result = await _db.SaveChangesAsync();
        return result;
    }

    public Models.Category FindByName(string name)
    {
        Models.Category category = _db.Categories.FirstOrDefault(x => x.CategoryName == name);
        return category;
    }

    private string GenerateCode(string name)
    {
        string prefix = name.Trim().Substring(0, 3).ToUpper();

        Random rdn = new Random();
        string code = prefix + rdn.Next(100, 999).ToString();

        return code;
    }
}
