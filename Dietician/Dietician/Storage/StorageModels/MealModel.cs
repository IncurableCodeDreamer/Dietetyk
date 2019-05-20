﻿using System;
using Dietician.Enums;

namespace Dietician.Storage.StorageModels
{
    public class MealModel
    {
        public string IdUser { get; set; }
        public string JsonId { get; set; }
        public DateTime MealDate { get; set; }
        //TO DO zamienic date time na nr dnia (1-7)
        public MealType MealTypeId { get; set; }
        public int Variant { get; set; }
        public MealModel()
        {

        }

        public MealModel(string idUser, string jsonId, DateTime mealDate, MealType mealTypeId, int variant)
        {
            Variant = variant;
            IdUser = idUser;
            JsonId = jsonId;
            MealDate = mealDate;
            MealTypeId = mealTypeId;
        }
    }
}
