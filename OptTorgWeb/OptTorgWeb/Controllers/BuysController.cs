using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class BuysController : Controller
    {
        private string _ViewForm = "TBuys";
        private string _EditForm = "EBuys";

        private string _CreateFormPickSupplier = "TPickSuppliers";
        private string _CreateFormPickProduct = "TPickProducts";
        private string _CreateFormPickBuysData = "TPickBuysData";
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }

        //Read
        [HttpGet]
        public IActionResult TBuys()
        {
            ViewBag.Role = CurrentUser.role;
            return View(_ViewForm, Buys.GetAllBuys());
        }

        //Create
        [HttpGet]
        public IActionResult OpenPickSupplier()
        {
            return View(_CreateFormPickSupplier);
        }

        [HttpPost]
        public IActionResult OpenPickProduct(Buys b)
        {
            return View(_CreateFormPickProduct, b);
        }

        [HttpPost]
        public IActionResult OpenPickCount(Buys b)
        {
            b = Buys.GetNavData(b);
            return View(_CreateFormPickBuysData, b);
        }

        [HttpPost]
        public IActionResult CreateBuys(Buys b)
        {
            Buys.CreatrBuys(b);
            return View(_ViewForm, Buys.GetAllBuys());
        }

        //Edit
        [HttpPost]
        public IActionResult OpenEBuys(int id)
        {
            return View(_EditForm, Buys.GetBuysById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesBuys(Buys p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;
            p.Active = true;

            Buys.UpdateBuys(p);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, Buys.GetAllBuys());
        }

        ////Delete
        [HttpPost]

        public IActionResult DBuys(int id)
        {
            Buys.DeleteBuys(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, Buys.GetAllBuys());
        }

        public IActionResult DCascade(int id)
        {
            Buys.DCascade(id);
            return View("TMeasureUnits", Buys.GetAllBuys());
        }
    }
}
