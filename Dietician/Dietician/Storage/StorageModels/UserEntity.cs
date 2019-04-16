
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
                    case "height":
                        Height = (int)prop.Value.Int32Value;
                        break;
                    case "weight":
                        Weight = (int)prop.Value.Int32Value;
                        break;
                    case "gender":
                        Gender = prop.Value.StringValue==Gender.Kobieta.ToString() ? Gender.Kobieta:Gender.Mężczyzna;
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
                {nameof(Weight), new EntityProperty(Weight)},
                {nameof(Height), new EntityProperty(Height)},
                {nameof(Gender), new EntityProperty(Gender.ToString())}
            };
            return result;
        }
    }
}
