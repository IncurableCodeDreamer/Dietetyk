using System.Collections.Generic;
using System.Text;
using IPdfConverter = DinkToPdf.Contracts.IConverter;
using DinkToPdf;
using System.Linq;
using System;
using Dietician.Storage.StorageModels;

namespace Dietician.Helpers
{
    public class PdfHelper
    {
        private static byte[] file;
        private static string htmlTemplate;
        static IPdfConverter pdfConverter = new SynchronizedConverter(new PdfTools());

        private static byte[] BuildPdf(string html)
        {
            return pdfConverter.Convert(new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 20, Right = 20, Left = 20, Bottom = 20 },
                    DocumentTitle = "Jadłospis-" + DateTime.Now.ToShortDateString(),
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8",  LoadImages = true, EnableIntelligentShrinking = true, EnableJavascript = false },
                        FooterSettings = { FontName = "Arial", FontSize = 10, Right = "[page] z [toPage]", Line = true, Spacing = 3.0 }
                    }
                }
            });
        }   

        public static byte[] WritePdf(List<FoodWithDayModel> records)
        {
            htmlTemplate = GetHTML(records);
            file = BuildPdf(htmlTemplate);
            return file;
        }

        private static string GetHTML(List<FoodWithDayModel> records)
        {
            var meals = records.OrderBy(x => x.Type).ThenBy(x => x.Day).ToList();
            List<string> Days = new List<string>() { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };

            var sb = new StringBuilder();
        
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>");

            foreach (var mealDay in meals)
            {
                sb.AppendFormat(@"                           
                                <h1 align='center'> {0} </h1>", Days[mealDay.Day-1]);
           
                sb.AppendFormat(@"
                                <h3 align='center'> {0} </h3>
                                <p align='center'> <strong> {1} </strong></p>
                                <p align='center'> 
                                    <img style='width: 250px; height: 250px;' src={2}/>
                               </p>
                                <p> Składniki:</p>     
                                <p> {3} </p>  
                                <p> Sposób przygotowania:</p>
                                <p> {4} </p>", mealDay.Type, mealDay.Name, mealDay.Url, mealDay.Ingredients, mealDay.Prepare);                
            }
               
            sb.Append(@"
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
