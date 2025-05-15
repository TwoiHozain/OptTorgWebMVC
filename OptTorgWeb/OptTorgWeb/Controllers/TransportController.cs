using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class TransportController : Controller
    {
        private string _ViewForm = "TTransport";
        private string _CreateForm = "CTransport";
        private string _EditForm = "ETransport";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TTransport()
        {
            ViewBag.Role = CurrentUser.role;
            return View(_ViewForm, Transport.GetAllTransport());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCTransport()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CTransport(Transport p)
        {
            Transport.CreatrTransport(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, Transport.GetAllTransport());
        }

        [HttpPost]
        public IActionResult CFewTransport(Transport p)
        {
            Transport.CreatrTransport(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenETransport(int id)
        {
            return View(_EditForm, Transport.GetTransportById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesTransport(Transport p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Transport.UpdateTransport(p);

            return View(_ViewForm, Transport.GetAllTransport());
        }

        ////Delete
        [HttpPost]
        public IActionResult DTransport(int id)
        {
            Transport.DeleteTransport(id);
            return View(_ViewForm, Transport.GetAllTransport());
        }

        public IActionResult DCascade(int id)
        {
            Transport.DCascade(id);
            return View("TMeasureUnits", Transport.GetAllTransport());
        }
    }
}
