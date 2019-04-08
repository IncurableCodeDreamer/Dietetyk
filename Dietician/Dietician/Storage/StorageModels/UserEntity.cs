
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class UserEntity : AzureUser
    {

        public string Name { get; set; }
        public string Lastname { get; set; }
        public int? Age { get; set; }
        public int? Height { get; set; }
        public Gender Gender { get; set; }
        public int? Weight { get; set; }

        public UserEntity()
        {

        }

        public UserEntity(string name, string lastname,
          int age, int height, Gender gender, int weight)
        {
            Name = name;
            Lastname = lastname;
            Age = age;
            Height = height;
            Gender = gender;
            Weight = weight;
        }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {
            int personId = 0;
            string login = "";
            string password = "";
            string name = "";
            string lastname = "";
            int age = 0;
            int height = 0;
            Gender gender = 0;
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
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(UserName), new EntityProperty(UserName)},
                {nameof(PasswordHash), new EntityProperty(PasswordHash)},
                {nameof(Name), new EntityProperty(Name)},
                {nameof(Lastname), new EntityProperty(Lastname)},
                {nameof(Age), new EntityProperty(Age)},
                {nameof(Id), new EntityProperty(Id)},
                {nameof(Weight), new EntityProperty(Weight)},
                {nameof(Height), new EntityProperty(Height)},
                {nameof(Gender), new EntityProperty(Gender.ToString())}
            };
            return result;
        }
    }
}
