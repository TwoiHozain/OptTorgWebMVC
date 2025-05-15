using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class DriversController : Controller
    {
        private string _ViewForm = "TDrivers";
        private string _EditForm = "EDriver";

        private string _CreateFormPickDriverData = "PickDriverData";
        private string _CreateFormPickDrivers = "DriversPickEmployee";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TDrivers()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_ViewForm, Drivers.GetAllDrivers());
        }

        //Create
        [HttpGet]
        public IActionResult OpenPickEmployee()
        {
            return View(_CreateFormPickDrivers);
        }

        [HttpPost]
        public IActionResult OpenPickDriverData(StorageEmployees se)
        {
            Drivers d = new Drivers();
            d.SeId = se.IdSe;
            d.SeNavigation = StorageEmployees.GetStorageEmployeesById(se.IdSe);

            return View(_CreateFormPickDriverData, d);
        }

        [HttpPost]
        public IActionResult CreateDrivers(Drivers se)
        {
            Drivers d = new Drivers();
            d.License = se.License;
            d.SeId = se.SeId;

            Drivers.CreatrDrivers(d);
            return View(_ViewForm, Drivers.GetAllDrivers());
        }

        //Edit
        [HttpPost]
        public IActionResult OpenEDrivers(int id)
        {
            return View(_EditForm, Drivers.GetDriversById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesDrivers(Drivers p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;
            p.Active = true;

            Drivers.UpdateDrivers(p);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, Drivers.GetAllDrivers());
        }

        ////Delete
        [HttpPost]

        public IActionResult DDrivers(int id)
        {
            Drivers.DeleteDrivers(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, Drivers.GetAllDrivers());
        }

        public IActionResult DCascade(int id)
        {
            Drivers.DCascade(id);
            return View("TMeasureUnits", Drivers.GetAllDrivers());
        }
    }
}
