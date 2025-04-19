using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class CountryOfProductController : Controller
    {
        private string _ViewForm = "TCountryOfProduct";
        private string _CreateForm = "CCountryOfProduct";
        private string _EditForm = "ECountryOfProduct";

        //Read
        [HttpGet]
        public IActionResult TCountryOfProduct()
        {
            return View(_ViewForm, CountryOfProduct.GetAllCountryOfProduct());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCCountryOfProduct()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CCountryOfProduct(CountryOfProduct p)
        {
            CountryOfProduct.CreatrPosition(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, CountryOfProduct.GetAllCountryOfProduct());
        }

        [HttpPost]
        public IActionResult CFewCountryOfProduct(CountryOfProduct p)
        {
            CountryOfProduct.CreatrPosition(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenECountryOfProduct(int id)
        {
            return View(_EditForm, CountryOfProduct.GetPositionById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesCountryOfProduct(CountryOfProduct p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            CountryOfProduct.UpdatePosition(p);

            return View(_ViewForm, CountryOfProduct.GetAllCountryOfProduct());
        }

        ////Delete
        [HttpPost]
        public IActionResult DPosition(int id)
        {
            CountryOfProduct.DeletePosition(id);
            return View(_ViewForm, CountryOfProduct.GetAllCountryOfProduct());
        }
        //ToDo
        public IActionResult DCascadePosition(int id)
        {
            return View("TMeasureUnits", CountryOfProduct.GetAllCountryOfProduct());
        }
    }
}
