using System.Data;

namespace Dietician.Storage.StorageModels
{
    public class UserMealModel
    {
        public int IdUserMeal { get; set; }
        public int IdUser { get; set; }
        public int IdMeal { get; set; }

        public UserMealModel()
        {

        }

        public UserMealModel(int idUserMeal, int idUser, int idMeal)
        {
            this.IdUser = idUser;
            this.IdMeal = idMeal;
            this.IdUserMeal = idUserMeal;
        }
    }
}