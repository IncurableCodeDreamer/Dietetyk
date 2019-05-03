using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class IngredientsModel
    {
        public int? IdIngredient { get; set; }
        public int? Milk { get; set; }
        public int? Eggs { get; set; }
        public int? Chocolate { get; set; }
        public int? Potatoes { get; set; }
        public int? Peanuts { get; set; }
        public int? Tomatoes { get; set; }
        public int? Soy { get; set; }
        public int? Wheat { get; set; }

        public IngredientsModel()
        {

        }

        public IngredientsModel(int? idIngredient, int? milk, int? eggs, int? chocolate, int? potatoes, int? peanuts, int? tomatoes, int? soy, int? wheat)
        {
            IdIngredient = idIngredient;
            Milk = milk;
            Eggs = eggs;
            Chocolate = chocolate;
            Potatoes = potatoes;
            Peanuts = peanuts;
            Tomatoes = tomatoes;
            Soy = soy;
            Wheat = wheat;
        }
    }
}
