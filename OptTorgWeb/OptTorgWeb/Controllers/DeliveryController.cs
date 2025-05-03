using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class DeliveryController : Controller
    {
        private string _ViewForm = "TDelivery";
        private string _EditForm = "E";

        private string _CreateFormPickStorage = "TPickStorage";
        private string _CreateFormPickEmployeeAccept = "TPickEmployeeAccept";
        private string _CreateFormPickEmployeeRecive = "TPickEmployeeRecive";
        private string _CreateFormPickDriver = "TPickDriver";
        private string _CreateFormPickTransport = "TPickTransport";
        private string _CreateFormDeliveryData = "PickDeliveryData";

        //Read
        [HttpGet]
        public IActionResult TDelivery()
        {
            return View(_ViewForm, Delivery.GetAllDelivery());
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

        //Delete
        [HttpPost]

        public IActionResult DDelivery(int id)
        {
            Delivery.DeleteDelivery(id);
            return View(_ViewForm, Delivery.GetAllDelivery());
        }

        //ToDo
        public IActionResult DCascadeDelivery(int id)
        {
            return View("TMeasureUnits", Delivery.GetAllDelivery());
        }
    }
}
