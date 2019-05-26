using Dietician.Models;
using System;

namespace Dietician.Storage.StorageModels
{
    public class PersonalData : PersonalDataSettings
    {
        public int PersonId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public PersonalData(int personId, string login, string password, string name, string lastname,
            int age, int height, Gender gender, int weight)
        {
            PersonId = personId;
            Login = login;
            Password = password;
            Name = name;
            Lastname = lastname;
            Age = age;
            Height = height;
            Gender = gender;
            Weight = weight;
        }

        public PersonalData()
        {

        }
    }
}
