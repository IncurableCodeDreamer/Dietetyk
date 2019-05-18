using Dietician.Models;
using Dietician.Storage;
using Dietician.Storage.Interfaces;
using Dietician.Storage.Repositories;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            ShoppingList list = new ShoppingList();
            List<string> items = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                items.Add("item" + i);
            };

            list.Item = items;
          // var x = _repository.ShoppingList.GetAllFoodsFromTable(user.Id);
            return View(list);
        }

       // [HttpPost]
        public IActionResult AddItem(ShoppingListModel model)
        {
            _repository.ShoppingList.InsertFoodIntoTable(model);
            return View();
        }

        //[HttpPost]
        public IActionResult RemoveItem(ShoppingListModel model)
        {
            _repository.ShoppingList.RemoveFood(model);
            return View();
        }
    }
}