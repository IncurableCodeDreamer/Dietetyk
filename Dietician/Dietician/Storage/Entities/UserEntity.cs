
using System.Collections.Generic;
using Dietician.Enums;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class UserEntity : AzureUser
    {

        public string Name { get; set; }
        public string Lastname { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public Lifestyle LifeStyle { get; set; }
        public string IdMealSetting { get; set; }
        public string IdIngredientSetting { get; set; }

        public UserEntity()
        {
        }

        public UserEntity(string name, string lastname, int age,  Gender gender)
        {
            Name = name.ToUpper();
            Lastname = lastname;
            Age = age;
            Gender = gender;
        }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {
            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "id":
                        Id = prop.Value.StringValue;
                        break;
                    case "username":
                        UserName = prop.Value.StringValue;
                        break;
                    case "passwordhash":
                        PasswordHash = prop.Value.StringValue;
                        break;
                    case "name":
                        Name = prop.Value.StringValue;
                        break;
                    case "lastname":
                        Lastname = prop.Value.StringValue;
                        break;
                    case "age":
                        Age = (int)prop.Value.Int32Value;
                        break;
                    case "gender":
                        Gender = (Gender)prop.Value.Int32Value;
                        break;
                    case "lifestyle":
                        LifeStyle = (Lifestyle) prop.Value.Int32Value;
                        break;
                    case "idmealsetting":
                        IdMealSetting = prop.Value.StringValue;
                        break;
                    case "idingredientsetting":
                        IdIngredientSetting = prop.Value.StringValue;
                        break;
                }
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(UserName), new EntityProperty(UserName.ToUpper())},
                {nameof(PasswordHash), new EntityProperty(PasswordHash)},
                {nameof(Name), new EntityProperty(Name)},
                {nameof(Lastname), new EntityProperty(Lastname)},
                {nameof(Age), new EntityProperty(Age)},
                {nameof(Id), new EntityProperty(Id)},
                {nameof(Gender), new EntityProperty((int)Gender)},
                {nameof(LifeStyle), new EntityProperty((int)LifeStyle)},
                {nameof(IdMealSetting), new EntityProperty(IdMealSetting)},
                {nameof(IdIngredientSetting), new EntityProperty(IdIngredientSetting)}
            };
            return result;
        }
    }
}
