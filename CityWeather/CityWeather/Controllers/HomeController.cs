using CityWeather.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CityWeather.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report(HttpPostedFileBase UploadedFile)
        {
            BLL.WeatherService weatherService = new BLL.WeatherService();
                string fileName = Path.GetFileName(UploadedFile.FileName);
                var dto = new WeatherDTO();
                dto.CityIds = new List<string>();

                if (UploadedFile.ContentLength > 0 && !string.IsNullOrEmpty(fileName))
                {  
                    using (var package = new ExcelPackage(UploadedFile.InputStream))
                    {
                        // get the first worksheet in the workbook
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int noOfRows = worksheet.Dimension.End.Row;
                        for (int row = 2; row <= noOfRows; row++)
                        {
                            dto.CityIds.Add(worksheet.Cells[row, 1].Value.ToString());
                        }
                    }    
                }
                return View(weatherService.FetchWeather(dto));
        }
    }
}