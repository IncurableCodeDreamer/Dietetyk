using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    interface IIngredientsRepository
    {
        Task InsertIngredientsIntoTable(IngredientsModel model);
        Task<IngredientEntity> GetIIngredientsFromTable(string idIngredients);
    }
}
