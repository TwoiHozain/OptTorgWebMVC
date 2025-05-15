using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class SendingStatusController : Controller
    {
        private string _ViewForm = "TSendingStatus";
        private string _CreateForm = "CSendingStatus";
        private string _EditForm = "ESendingStatus";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TSendingStatus()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_ViewForm, SendingStatus.GetAllSendingStatus());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCSendingStatus()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CSendingStatus(SendingStatus p)
        {
            SendingStatus.CreatrSendingStatus(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, SendingStatus.GetAllSendingStatus());
        }

        [HttpPost]
        public IActionResult CFewSendingStatus(SendingStatus p)
        {
            SendingStatus.CreatrSendingStatus(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenESendingStatus(int id)
        {
            return View(_EditForm, SendingStatus.GetSendingStatusById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesSendingStatus(SendingStatus p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            SendingStatus.UpdateSendingStatus(p);

            return View(_ViewForm, SendingStatus.GetAllSendingStatus());
        }

        ////Delete
        [HttpPost]
        public IActionResult DSendingStatus(int id)
        {
            SendingStatus.DeleteSendingStatus(id);
            return View(_ViewForm, SendingStatus.GetAllSendingStatus());
        }

        public IActionResult DCascade(int id)
        {
            SendingStatus.DCascade(id);
            return View("TMeasureUnits", SendingStatus.GetAllSendingStatus());
        }
    }
}
