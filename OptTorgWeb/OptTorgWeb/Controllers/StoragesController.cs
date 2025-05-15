using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class StoragesController : Controller
    {
        private string _ContactsViewForm = "TCStorages";
        private string _AdressViewForm = "TAStorages";
        private string _CreateForm = "CStorages";
        private string _EditForm = "EStorages";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TAStorages()
        {
            ViewBag.Role = CurrentUser.role;
            return View(_AdressViewForm, Storages.GetAllStorages());
        }

        public IActionResult TCStorages()
        {
            ViewBag.Role = CurrentUser.role;
            return View(_ContactsViewForm, Storages.GetAllStorages());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCStorages()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CStorages(Storages p)
        {
            if (!ModelState.IsValid)
            {
                return View(_CreateForm);
            }

            Storages.CreatrStorages(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;
            //TODO:
            //Возврат к форме из которой пришел

            return View(_ContactsViewForm, Storages.GetAllStorages());
        }

        [HttpPost]
        public IActionResult CFewStorages(Storages p)
        {
            if (!ModelState.IsValid)
            {
                return View(_CreateForm);
            }

            Storages.CreatrStorages(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenEStorages(int id)
        {
            return View(_EditForm, Storages.GetStoragesById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesStorages(Storages p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Storages.UpdateStorages(p);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ContactsViewForm, Storages.GetAllStorages());
        }

        ////Delete
        [HttpPost]
        public IActionResult DStorages(int id)
        {
            Storages.DeleteStorages(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ContactsViewForm, Storages.GetAllStorages());
        }

        public IActionResult DCascade(int id)
        {
            Storages.DCascade(id);
            return View("TMeasureUnits", Storages.GetAllStorages());
        }
    }
}
