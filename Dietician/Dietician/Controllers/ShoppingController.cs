using Dietician.Helpers;
using Dietician.Storage;
using Dietician.Storage.Interfaces;
using Dietician.Storage.Repositories;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;

namespace Dietician.Controllers
{
    public class ShoppingController : BaseController
    {
        private readonly IRepositoryWrapper _repository;

        public ShoppingController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }

        public IActionResult Index()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            if (user != null)
            {
                var list = _repository.ShoppingList.GetAllFoodsFromTable(user.Id).Result;
                return View(list);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult AddItem(string ingredientName)
        {
            UserEntity user = GetLoggedUser(_repository.User);
            ShoppingListModel shoppingListModel = new ShoppingListModel(user.Id, ingredientName);
            _repository.ShoppingList.InsertFoodIntoTable(shoppingListModel);

            return RedirectToAction("Index");
        }

        public FileResult ExportToPdf()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            var list = _repository.ShoppingList.GetAllFoodsFromTable(user.Id).Result;
            var pdfByteArray = PdfHelper.WritePdf(list);

            return File(pdfByteArray, System.Net.Mime.MediaTypeNames.Application.Octet, "Lista_Zakupow-" + DateTime.Now.ToShortDateString() + ".pdf");
        }

        [HttpPost]
        public IActionResult SendByMail(string mail)
        {
            UserEntity user = GetLoggedUser(_repository.User);
            var shippingList = _repository.ShoppingList.GetAllFoodsFromTable(user.Id).Result;
            using (MailMessage mm = new MailMessage("aplikacjadietetyczna@gmail.com", mail))
            {
                mm.Subject = "Lista zakupów";
                string body = "Witaj ";
                body += "<br /><br />Oto Twoja lista zakupów:";
                foreach (var item in shippingList)
                {
                    body += "<br />" + item.ShopModelData.Ingredient;
                }
                body += "<br /><br />Miłych zakupów życzy Twoja Aplikacja Dietetyczna";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    EnableSsl = true
                };
                NetworkCredential NetworkCred = new NetworkCredential("aplikacjadietetyczna@gmail.com", "appIP2019");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }


            return RedirectToAction("Index");
        }

        public IActionResult RemoveItem(string model)
        {
            UserEntity user = GetLoggedUser(_repository.User);
            ShoppingListModel shoppingListModel = new ShoppingListModel(user.Id, model);
            _repository.ShoppingList.RemoveFood(shoppingListModel);

            return RedirectToAction("Index");
        }
    }
}