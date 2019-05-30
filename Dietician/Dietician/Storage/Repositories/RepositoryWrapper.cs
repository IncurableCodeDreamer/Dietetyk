using Dietician.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IAppConfiguration _appConfiguration;
        private IIndicatorRepository _indicator;
        private IUserRepository _user;
        private IUserMealRepository _userMeal;
        private IIngredientsRepository _ingredients;
        private IMealRepository _meal;
        private IMealTypeRepository _mealType;
        private IMealSettingRepository _mealSetting;
        private IFoodRepository _food;
        private IShoppingListRepository _shoppingList;
        
        public RepositoryWrapper(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public IIndicatorRepository Indicator {
            get
            {
                if (_indicator == null)
                {
                    _indicator = new IndicatorRepository(_appConfiguration);
                }
                return _indicator;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_appConfiguration);
                }
                return _user;
            }
        }

        public IUserMealRepository UserMeal
        {
            get
            {
                if (_userMeal == null)
                {
                    _userMeal = new UserMealRepository(_appConfiguration);
                }
                return _userMeal;
            }
        }

        public IIngredientsRepository Ingredients
        {
            get
            {
                if (_ingredients == null)
                {
                    _ingredients = new IngredientsRepository(_appConfiguration);
                }
                return _ingredients;
            }
        }
        public IFoodRepository Food
        {
            get
            {
                if (_food == null)
                {
                    _food = new FoodRepository(_appConfiguration);
                }
                return _food;
            }
        }
        public IMealRepository Meal
        {
            get
            {
                if (_meal == null)
                {
                    _meal = new MealRepository(_appConfiguration);
                }
                return _meal;
            }
        }

        public IMealTypeRepository MealType
        {
            get
            {
                if (_mealType == null)
                {
                    _mealType = new MealTypeRepository(_appConfiguration);
                }
                return _mealType;
            }
        }

        public IMealSettingRepository MealSetting
        {
            get
            {
                if (_mealSetting == null)
                {
                    _mealSetting = new MealSettingRepository(_appConfiguration);
                }
                return _mealSetting;
            }
        }

        public IShoppingListRepository ShoppingList
        {
            get
            {
                if (_shoppingList == null)
                {
                    _shoppingList = new ShoppingListRepository(_appConfiguration);
                }
                return _shoppingList;
            }
        }
    }
}
