﻿using System;
using System.Collections.Generic;
using Dietician.CosmosDB;
using Dietician.Enums;
using Dietician.Helpers;
using Dietician.Models;
using Dietician.Storage;
using Dietician.Storage.Interfaces;
using Dietician.Storage.Repositories;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dietician.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IRepositoryWrapper _repository;

        public MenuController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }

        public FileResult ExportToPdf(List<Meal> meals = null)
        {
            List<Meal> mealsList = new List<Meal>();
            Array values = Enum.GetValues(typeof(MealType));
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                mealsList.Add(new Meal()
                {
                    Date = DateTime.Now.AddDays(i),
                    CosmosMeal = new CosmosMealModel()
                    {
                        Calories = 24,
                        Carbohydrates = 10,
                        Fat = 10,
                        Guid = "guid",
                        Ingredients = "mleko, jaja, costam",
                        Kind = "typ",
                        Name = "owsianka",
                        Portions = "porcje",
                        Prepare = "nalej mleka do miski, dodaj płatki, wymieszaj w 30 stopniach przez 20 minut i wsio",
                        Proteins = 20,
                        Type = (MealType)values.GetValue(random.Next(values.Length)),
                    },
                    MealType = (MealType)values.GetValue(random.Next(values.Length)),
                    JsonId = 1
                });
            }

            var pdfByteArray = PdfHelper.WritePdf(mealsList);
            return File(pdfByteArray, System.Net.Mime.MediaTypeNames.Application.Octet, "Jadłospis-" + DateTime.Now.ToShortDateString() + ".pdf");                  
        }

        public IActionResult Index()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            //TODO change date
            List<Meal> dailyMeals = GetDailyMealsForUser(user, DateTime.Now);
            Array values = Enum.GetValues(typeof(MealType));
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                dailyMeals.Add(new Meal()
                {
                    Date = DateTime.Now.AddDays(i),
                    CosmosMeal = new CosmosMealModel()
                    {
                        Calories = 24,
                        Carbohydrates = 10,
                        Fat = 10,
                        Guid = "guid",
                        Ingredients = "mleko, jaja, costam",
                        Kind = "typ",
                        Name = "owsianka",
                        Portions = "porcje",
                        Prepare = "nalej mleka do miski, dodaj płatki, wymieszaj w 30 stopniach przez 20 minut i wsio",
                        Proteins = 20,
                        Type = (MealType)values.GetValue(random.Next(values.Length)),
                    },
                    MealType = (MealType)values.GetValue(random.Next(values.Length)),
                    JsonId = 1
                });
            }

            return View(dailyMeals); 
        }
        
        public ActionResult AddMeal(AddMeal meal)
        {
            if (ModelState.IsValid)
            {
                //TODO addmeal to db
            }
            return PartialView("_AddMealModal", meal);
        }

        public ActionResult ChangeMenu(ChangeMenu menu)
        {
            if (ModelState.IsValid)
            {
                //TODO change menu
            }
            return PartialView("_ChangeMenuModal", menu);
        }

        [HttpGet]
        public PartialViewResult GetAddMealView()
        {
            AddMeal model = new AddMeal();
            return PartialView("_AddMealModal", model);
        }

        [HttpGet]
        public PartialViewResult GetChangeMenuView()
        {
            ChangeMenu model = new ChangeMenu();
            UserEntity user = GetLoggedUser(_repository.User);
            model.MealTypes = GetMealsType(user);
            return PartialView("_ChangeMenuModal",model);
        }

        private List<SelectListItem> GetMealsType(UserEntity user)
        {
            List<SelectListItem> mealsType = new List<SelectListItem>();
            //TODO implement - wybrac mealtypes ktore chcemy uwzglednic
            mealsType.Add(new SelectListItem(MealType.Sniadanie.ToString(),MealType.Sniadanie.ToString()));
            mealsType.Add(new SelectListItem(MealType.Sniadanie2.ToString(), MealType.Sniadanie2.ToString()));
            mealsType.Add(new SelectListItem(MealType.Obiad.ToString(), MealType.Obiad.ToString()));
            mealsType.Add(new SelectListItem(MealType.Podwieczorek.ToString(), MealType.Podwieczorek.ToString()));
            mealsType.Add(new SelectListItem(MealType.Kolacja.ToString(), MealType.Kolacja.ToString()));
            
            return mealsType;
        }

        private List<Meal> GetDailyMealsForUser(UserEntity user, DateTime date)
        {
            List<Meal> dailyMeals = new List<Meal>();
            //TODO implement
            return dailyMeals;
        }
    }
}
