using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class PricingController : Controller
    {
        private string _ViewForm = "TPricing";
        private string _CreateForm = "CPricing";
        private string _EditForm = "EPricing";

        //Read
        [HttpGet]
        public IActionResult TPricing()
        {
            return View(_ViewForm, Pricing.GetAllPricing());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCPricing()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CPricing(Pricing p)
        {
            Pricing.CreatrPricing(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, Pricing.GetAllPricing());
        }

        [HttpPost]
        public IActionResult CFewPricing(Pricing p)
        {
            Pricing.CreatrPricing(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenEPricing(int id)
        {
            return View(_EditForm, Pricing.GetPricingById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesPricing(Pricing p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Pricing.UpdatePricing(p);

            return View(_ViewForm, Pricing.GetAllPricing());
        }

        ////Delete
        [HttpPost]
        public IActionResult DPricing(int id)
        {
            Pricing.DeletePricing(id);
            return View(_ViewForm, Pricing.GetAllPricing());
        }
        //ToDo
        public IActionResult DCascadePricing(int id)
        {
            return View("TMeasureUnits", Pricing.GetAllPricing());
        }
    }
}
