using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    public interface IIngredientsRepository
    {
        Task<IngredientsModel> InsertIngredientsIntoTable(IngredientsModel model);
        Task<IngredientEntity> GetIIngredientsFromTable(string idIngredients);
        Task UpdateIngridients(IngredientEntity model);
    }
}
