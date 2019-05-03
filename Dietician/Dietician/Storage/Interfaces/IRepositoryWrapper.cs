using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
