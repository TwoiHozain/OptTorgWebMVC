using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class StorageEmployeesController : Controller
    {
        private string _ViewForm = "TStorageEmployees";
        private string _CreateFormPickStorage = "SEPickStorage";
        private string _CreateFormPickEmployee = "SEPickEmployee";
        private string _EditForm = "EStorageEmployees";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TStorageEmployees()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_ViewForm, StorageEmployees.GetAllStorageEmployees());
        }

        //Create
        [HttpGet]
        public IActionResult OpenPickEmployee()
        {
            return View(_CreateFormPickEmployee);
        }

        [HttpPost]
        public IActionResult OpenPickStorage(StorageEmployees se)
        {
            return View(_CreateFormPickStorage, se);
        }

        [HttpPost]
        public IActionResult CreateStorageEmployy(StorageEmployees se)
        {
            StorageEmployees.CreatrStorageEmployees(se);
            return View(_ViewForm, StorageEmployees.GetAllStorageEmployees());
        }

        //Edit
        [HttpPost]
        public IActionResult OpenEStorageEmployees(int id)
        {
            return View(_EditForm, StorageEmployees.GetStorageEmployeesById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesStorageEmployees(StorageEmployees p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;
            p.Active = true;

            StorageEmployees.UpdateStorageEmployees(p);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, StorageEmployees.GetAllStorageEmployees());
        }

        ////Delete
        [HttpPost]

        public IActionResult DStorageEmployees(int id)
        {
            StorageEmployees.DeleteStorageEmployees(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, StorageEmployees.GetAllStorageEmployees());
        }
        //ToDo
        public IActionResult DCascadeEmployees(int id)
        {
            return View("TMeasureUnits", StorageEmployees.GetAllStorageEmployees());
        }
        public IActionResult DCascade(int id)
        {
            StorageEmployees.DCascade(id);
            return View("TMeasureUnits", StorageEmployees.GetAllStorageEmployees());
        }
    }
}
