using Microsoft.AspNetCore.Mvc;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;
using OptTorgWeb.Classes;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OptTorgWeb.Controllers
{
    public class DeliveryController : Controller
    {
        private string _ViewForm = "TDelivery";
        private string _EditForm = "EDeliveryData";
        
        private string _PickDeliveryForm = "TPickDelivery";

        private string _CreateFormPickStorage = "TPickStorage";
        private string _CreateFormPickEmployeeAccept = "TPickEmployeeAccept";
        private string _CreateFormPickEmployeeRecive = "TPickEmployeeRecive";
        private string _CreateFormPickDriver = "TPickDriver";
        private string _CreateFormPickTransport = "TPickTransport";
        private string _CreateFormDeliveryData = "PickDeliveryData";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TDelivery()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_ViewForm, Delivery.GetAllDelivery());
        }

        [HttpGet]
        public IActionResult OpenPickDelivery()
        {
            return View(_PickDeliveryForm, Delivery.GetDeliveryForTorg12());
        }

        [HttpPost]
        public IActionResult CreateTorg12(long id)
        {
            //string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            //string baseDir = Path.GetFullPath(Path.Combine(exeDir, "..", "..", "..", ".."));

            //String _sourcePath = Path.Combine(exeDir, "Torg12.XLS");
            //String _outputPath = Path.Combine(exeDir, "Torg12tmp.XLS"); ;

            string mimeType = "application/vnd.ms-excel";
            string date = Delivery.GetDeliveryById(id).Date.ToShortDateString() + "  " +DateTime.Now.ToLongDateString();
            date = date.Replace(".", " ");
            date = date.Replace(",", " ");

            string fileName = $"Torg12 {date}.XLS";

            //byte[] fileBytes = System.IO.File.ReadAllBytes(_outputPath);

            //System.IO.File.Delete(_outputPath);

            return File(Torg12.CreateExcellTorg12(Delivery.GetDeliveryById(id)), mimeType, fileName);
        }

        //Create
        [HttpGet]
        public IActionResult OpenPickStorage()
        {
            return View(_CreateFormPickStorage);
        }

        [HttpPost]
        public IActionResult OpenPickEmployeeAccept(Delivery d)
        {
            return View(_CreateFormPickEmployeeAccept, d);
        }

        [HttpPost]
        public IActionResult OpenPickEmployeeRecive(Delivery d)
        {
            return View(_CreateFormPickEmployeeRecive, d);
        }

        [HttpPost]
        public IActionResult OpenPickDriver(Delivery d)
        {
            return View(_CreateFormPickDriver, d);
        }

        [HttpPost]
        public IActionResult OpenPickTransport(Delivery d)
        {
            return View(_CreateFormPickTransport, d);
        }

        [HttpPost]
        public IActionResult OpenPickDeliveryData(Delivery d)
        {
            return View(_CreateFormDeliveryData, d);
        }

        [HttpPost]
        public IActionResult CreateDelivery(Delivery d)
        {
            Delivery.CreateDelivery(d);
            return View(_ViewForm, Delivery.GetAllDelivery());
        }
        
        //Edit
        [HttpPost]
        public IActionResult OpenEDelivery(long id)
        {
            return View(_EditForm, Delivery.GetDeliveryById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesDelivery(Delivery p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Delivery.UpdateDelivery(p);

            return View(_ViewForm, Delivery.GetAllDelivery());
        }

        //Delete
        [HttpPost]

        public IActionResult DDelivery(int id)
        {
            Delivery.DeleteDelivery(id);
            return View(_ViewForm, Delivery.GetAllDelivery());
        }


        public IActionResult DCascade(int id)
        {
            Delivery.DCascade(id);
            return View("TMeasureUnits", Delivery.GetAllDelivery());
        }
    }
}
