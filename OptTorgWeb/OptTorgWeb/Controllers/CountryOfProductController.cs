using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class CountryOfProductController : Controller
    {
        private string _ViewForm = "TCountryOfProduct";
        private string _CreateForm = "CCountryOfProduct";
        private string _EditForm = "ECountryOfProduct";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TCountryOfProduct()
        {
            ViewBag.Role = CurrentUser.role;

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
            CountryOfProduct.CreatrCountryOfProduct(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, CountryOfProduct.GetAllCountryOfProduct());
        }

        [HttpPost]
        public IActionResult CFewCountryOfProduct(CountryOfProduct p)
        {
            CountryOfProduct.CreatrCountryOfProduct(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }

        //Edit
        [HttpPost]
        public IActionResult OpenECountryOfProduct(int id)
        {
            return View(_EditForm, CountryOfProduct.GetCountryOfProductById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesCountryOfProduct(CountryOfProduct p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            CountryOfProduct.UpdateCountryOfProduct(p);

            return View(_ViewForm, CountryOfProduct.GetAllCountryOfProduct());
        }

        ////Delete
        [HttpPost]
        public IActionResult DCountryOfProduct(int id)
        {
            CountryOfProduct.DeleteCountryOfProduct(id);
            return View(_ViewForm, CountryOfProduct.GetAllCountryOfProduct());
        }

        public IActionResult DCascade(int id)
        {
            CountryOfProduct.DCascade(id);
            return View("TMeasureUnits", CountryOfProduct.GetAllCountryOfProduct());
        }
    }
}
