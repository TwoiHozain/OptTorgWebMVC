using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class DriversController : Controller
    {
        private string _ViewForm = "TDrivers";
        private string _EditForm = "EDriver";

        private string _CreateFormPickDriverData = "PickDriverData";
        private string _CreateFormPickDrivers = "DriversPickEmployee";

        //Read
        [HttpGet]
        public IActionResult TDrivers()
        {
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
        //ToDo
        public IActionResult DCascadeDriverss(int id)
        {
            return View("TMeasureUnits", Drivers.GetAllDrivers());
        }
    }
}
