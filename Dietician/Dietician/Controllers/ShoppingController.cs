using Dietician.Storage;
using Dietician.Storage.Interfaces;
using Dietician.Storage.Repositories;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Mvc;

namespace Dietician.Controllers
{
    public class ShoppingController : BaseController
    {
        private readonly IRepositoryWrapper _repository;

        public ShoppingController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }

        public IActionResult Index()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            if (user != null)
            {
                var list = _repository.ShoppingList.GetAllFoodsFromTable(user.Id).Result;
                return View(list);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult AddItem(string ingredientName)
        {
            UserEntity user = GetLoggedUser(_repository.User);
            ShoppingListModel shoppingListModel = new ShoppingListModel(user.Id, ingredientName);
            _repository.ShoppingList.InsertFoodIntoTable(shoppingListModel);

            return RedirectToAction("Index");
        }
        
        public IActionResult RemoveItem(string model)
        {
            UserEntity user = GetLoggedUser(_repository.User);
            ShoppingListModel shoppingListModel = new ShoppingListModel(user.Id, model);
            _repository.ShoppingList.RemoveFood(shoppingListModel);

            return RedirectToAction("Index");
        }
    }
}