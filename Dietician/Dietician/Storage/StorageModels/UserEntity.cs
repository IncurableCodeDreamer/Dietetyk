
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class UserEntity : TableEntity
    {
        public PersonalData UserModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {
            int personId = 0;
            string login = "";
            string password = "";
            string name = "";
            string lastname = "";
            int age = 0;
            int height = 0;
            Gender? gender=null;
            int weight = 0;

            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "personid":
                        personId = (int) prop.Value.Int32Value;
                        break;
                    case "login":
                        login = prop.Value.StringValue;
                        break;
                    case "password":
                        password = prop.Value.StringValue;
                        break;
                    case "name":
                        name = prop.Value.StringValue;
                        break;
                    case "lastname":
                        lastname = prop.Value.StringValue;
                        break;
                    case "age":
                        age = (int)prop.Value.Int32Value;
                        break;
                    case "height":
                        height = (int)prop.Value.Int32Value;
                        break;
                    case "weight":
                        weight = (int)prop.Value.Int32Value;
                        break;
                    case "gender":
                        gender = (Gender) prop.Value.PropertyType;
                        break;
                }
            }
            UserModelData = new PersonalData(personId, login, password, name, lastname, age, height, gender, weight);
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(UserModelData.Login), new EntityProperty(UserModelData.Login)},
                {nameof(UserModelData.Password), new EntityProperty(UserModelData.Password)},
                {nameof(UserModelData.Name), new EntityProperty(UserModelData.Name)},
                {nameof(UserModelData.Lastname), new EntityProperty(UserModelData.Lastname)},
                {nameof(UserModelData.Age), new EntityProperty(UserModelData.Age)},
                {nameof(UserModelData.PersonId), new EntityProperty(UserModelData.PersonId)},
                {nameof(UserModelData.Weight), new EntityProperty(UserModelData.Weight)},
                {nameof(UserModelData.Height), new EntityProperty(UserModelData.Height)},
                {nameof(UserModelData.Gender), new EntityProperty(UserModelData.Gender.ToString())}
            };
            return result;
        }
    }
}
