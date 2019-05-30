namespace Dietician.Storage.Interfaces
{
    public interface IRepositoryWrapper
    {
        IIndicatorRepository Indicator {get;}
        IUserRepository User { get; }
        IUserMealRepository UserMeal { get; }
        IIngredientsRepository Ingredients { get; }
        IMealRepository Meal { get; }
        IMealTypeRepository MealType { get; }
        IMealSettingRepository MealSetting { get; }
        IFoodRepository Food { get; }
        IShoppingListRepository ShoppingList { get; }
    }
}
