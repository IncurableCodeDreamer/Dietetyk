using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.CosmosDB
{
    public interface ICosmosRepository
    {
        IOrderedQueryable GetDocuments(string ss);
        MealList GetAllMeals();
    }
}
