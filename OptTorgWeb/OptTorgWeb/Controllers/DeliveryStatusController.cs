using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class DeliveryStatusController : Controller
    {
        private string _ViewForm = "TDeliveryStatus";
        private string _CreateForm = "CDeliveryStatus";
        private string _EditForm = "EDeliveryStatus";

        //Read
        [HttpGet]
        public IActionResult TDeliveryStatus()
        {
            return View(_ViewForm, DeliveryStatus.GetAllDeliveryStatus());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCDeliveryStatus()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CDeliveryStatus(DeliveryStatus p)
        {
            DeliveryStatus.CreatrPosition(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, DeliveryStatus.GetAllDeliveryStatus());
        }

        [HttpPost]
        public IActionResult CFewDeliveryStatus(DeliveryStatus p)
        {
            DeliveryStatus.CreatrPosition(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenEDeliveryStatus(int id)
        {
            return View(_EditForm, DeliveryStatus.GetPositionById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesDeliveryStatus(DeliveryStatus p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            DeliveryStatus.UpdatePosition(p);

            return View(_ViewForm, DeliveryStatus.GetAllDeliveryStatus());
        }

        ////Delete
        [HttpPost]
        public IActionResult DPosition(int id)
        {
            DeliveryStatus.DeletePosition(id);
            return View(_ViewForm, DeliveryStatus.GetAllDeliveryStatus());
        }
        //ToDo
        public IActionResult DCascadePosition(int id)
        {
            return View("TMeasureUnits", DeliveryStatus.GetAllDeliveryStatus());
        }
    }
}
