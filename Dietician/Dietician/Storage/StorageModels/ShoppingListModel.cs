﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class ShoppingListModel
    {
        public ShoppingListModel(string userId, string ingredient)
        {
            UserId = userId;
            Ingredient = ingredient;
        }

        public string Ingredient { get; set; }
        public string UserId { get; set; }
    }
}
