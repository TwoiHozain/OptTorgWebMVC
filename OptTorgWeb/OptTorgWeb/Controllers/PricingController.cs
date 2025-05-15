using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class PricingController : Controller
    {
        private string _ViewForm = "TPricing";
        private string _CreateForm = "CPricing";
        private string _EditForm = "EPricing";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TPricing()
        {
            ViewBag.Role = CurrentUser.role;

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

        public IActionResult DCascade(int id)
        {
            Pricing.DCascade(id);
            return View("TMeasureUnits", Pricing.GetAllPricing());
        }
    }
}
