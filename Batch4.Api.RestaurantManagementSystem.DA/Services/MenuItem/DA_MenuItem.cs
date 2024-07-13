using Batch4.Api.RestaurantManagementSystem.DA.Services.Category;

namespace Batch4.Api.RestaurantManagementSystem.DA.Services.MenuItem;

public class DA_MenuItem
{
    private readonly AppDbContext _db;
    private readonly DA_Category _daCategory;

    public DA_MenuItem(AppDbContext db,
        DA_Category daCategory)
    {
        _db = db;
        _daCategory = daCategory;
    }

    public async Task<int> CreateMenuItem(MenuItemRequestModel reqModel)
    {
        Models.MenuItem menu = new Models.MenuItem()
        {
            ItemName = reqModel.ItemName,
            ItemPrice = reqModel.ItemPrice,
            CategoryCode = reqModel.CategoryCode
        };
        _db.MenuItems.Add(menu);
        int result = await _db.SaveChangesAsync();
        return result;
    }

    public async Task<List<Models.MenuItem>> GetAllMenuItem()
    {
        List<Models.MenuItem> list = await _db.MenuItems.ToListAsync();
        return list;
    }

    public async Task<Models.MenuItem> GetMenuItemById(int id)
    {
        Models.MenuItem item = await _db.MenuItems.FirstOrDefaultAsync(x => x.ItemId == id)!;
        return item;
    }

    public async Task<List<Models.MenuItem>> GetMenuItemByCategoryCode(string categoryCode)
    {
        var lst = await _db.MenuItems.Where(x => x.CategoryCode == categoryCode).ToListAsync();
        return lst;
    }

    public async Task<int> UpdateMenuItem(int id, MenuItemRequestModel reqModel)
    {
        var category = await _daCategory.GetCategoryByCode(reqModel.CategoryCode);
        if (category is null) return 0;
        Models.MenuItem item = await GetMenuItemById(id);
        item.ItemName = reqModel.ItemName;
        item.ItemPrice = reqModel.ItemPrice;
        item.CategoryCode = reqModel.CategoryCode;

        int result = await _db.SaveChangesAsync();
        return result;
    }

    public async Task<int> DeleteMenuItem(int id)
    {
        Models.MenuItem item = await this.GetMenuItemById(id); ;
        if (item == null) throw new InvalidDataException("No data found");

        _db.MenuItems.Remove(item);

        int result = await _db.SaveChangesAsync();
        return result;
    }

    public Models.MenuItem FindByName(string name)
    {
        Models.MenuItem item = _db.MenuItems.FirstOrDefault(x => x.ItemName == name);
        return item;
    }
}
