using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class IngredientsModel
    {
        public string IdIngredient { get; set; }
        public bool Milk { get; set; }
        public bool Eggs { get; set; }
        public bool Chocolate { get; set; }
        public bool Potatoes { get; set; }
        public bool Peanuts { get; set; }
        public bool Tomatoes { get; set; }
        public bool Soy { get; set; }
        public bool Wheat { get; set; }

        public IngredientsModel()
        {

        }

        public IngredientsModel(string idIngredient, bool milk, bool eggs, bool chocolate, bool potatoes, bool peanuts, bool tomatoes, bool soy, bool wheat)
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
