using Microsoft.AspNetCore.Mvc;
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

        //Read
        [HttpGet]
        public IActionResult TBuys()
        {
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
        //ToDo
        public IActionResult DCascadeBuyss(int id)
        {
            return View("TMeasureUnits", Buys.GetAllBuys());
        }
    }
}
