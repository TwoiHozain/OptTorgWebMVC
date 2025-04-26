using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class StoragesController : Controller
    {
        private string _ContactsViewForm = "TCStorages";
        private string _AdressViewForm = "TAStorages";
        private string _CreateForm = "CStorages";
        private string _EditForm = "EStorages";

        //Read
        [HttpGet]
        public IActionResult TAStorages()
        {
            return View(_AdressViewForm, Storages.GetAllStorages());
        }

        public IActionResult TCStorages()
        {
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
        //ToDo
        public IActionResult DCascadeStorages(int id)
        {
            return View("TMeasureUnits", Storages.GetAllStorages());
        }
    }
}
