using System.Collections.Generic;
using System.Text;
using IPdfConverter = DinkToPdf.Contracts.IConverter;
using DinkToPdf;
using Dietician.Models;
using System.Linq;

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
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 20, Right = 10 },
                    DocumentTitle = "PDF Report",
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8", EnableJavascript = false },
                        FooterSettings = { FontName = "Arial", FontSize = 9, Right = "Strona [page] z [toPage]", Line = true, Spacing = 3.0 }
                    }
                }
            });
        }

        public static byte[] WritePdf(List<Meal> records)
        {
            htmlTemplate = GetHTML(records);
            file = BuildPdf(htmlTemplate);
            return file;
        }

        private static string GetHTML(List<Meal> records)
        {
            var meals = records.GroupBy(x => x.Date).OrderBy(x => x.Key).ToList();
            List<string> Days = new List<string>() { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };

            var sb = new StringBuilder();
        
            sb.Append(@"
                        <html>
                            <head>
                            </head>");

            foreach (var day in Days)
            {
                sb.AppendFormat(@"
                            <body>
                                <h1 align='center'> {0} </h1>", day);

                foreach (var mealDay in meals)
                {
                    foreach (var meal in mealDay.Select(x => x.CosmosMeal))
                    {
                        sb.AppendFormat(@"
                                <h3 align='center'> {0} </h3>
                                <p align='center'> <strong> {1} </strong></p>
                                <p align='center'> 
                                    <img src={2}/>
                               </p>
                                <p> Składniki:</p>     
                                <p> {3} </p>  
                                <p> Sposób przygotowania:</p>
                                <p> {4} </p>", meal.Type, meal.Name, meal.ImageUrl, meal.Ingredients, meal.Prepare);
                    }
                }
            }
               
            sb.Append(@"
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
