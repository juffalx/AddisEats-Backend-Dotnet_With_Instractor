using AddisEats.Api.Models;

namespace AddisEats.Api.Services.MenuItemServices;

public class MenuItemService : IMenuItemService
{
    private readonly List<MenuItem> _menuItems;

    public MenuItemService()
    {
        _menuItems = new List<MenuItem>
        {
            new MenuItem { Id = 1, Name = "Cheeseburger", Category = "Main Course", Price= 10.5m},
            new MenuItem { Id = 2, Name = "Shiro", Category = "Main Course", Price= 11.5m},
            new MenuItem { Id = 3, Name = "Cheeseburger", Category = "Main Course", Price= 12.5m},
            new MenuItem { Id = 4, Name = "Kurt Salad", Category = "Appetizer", Price= 20.5m},
            new MenuItem { Id = 5, Name = "Kitfo Salad", Category = "Appetizer", Price= 21.5m},
            new MenuItem { Id = 6, Name = "Chocolate Cake", Category = "Dessert", Price= 31.5m },
            new MenuItem { Id = 7, Name = "Aybee Cake", Category = "Dessert", Price= 32.5m },
            new MenuItem { Id = 8, Name = "Chocolate Cake", Category = "Dessert", Price= 33.5m }

        };
    }

    public List<MenuItem> GetAll(string? category, int? upPrice)
    {
        var result = _menuItems.Where(dish => category == null ||  dish.Category.Trim().ToLowerInvariant().Contains(category.Trim().ToLowerInvariant()))
        .Where(filterItem => upPrice == null || filterItem.Price >= upPrice)
        .ToList();

        return result;

    }

    public MenuItem? GetById(int id)
    {
        return _menuItems.Where(item => item.Id == id).FirstOrDefault();
    }

}