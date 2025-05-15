using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private string _ContactsViewForm = "TCEmployees";
        private string _AdressViewForm = "TAEmployees";
        private string _CreateForm = "CEmployees";
        private string _EditForm = "EEmployees";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TAEmployees()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_AdressViewForm, Employees.GetAllEmployees());
        }

        public IActionResult TCEmployees()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_ContactsViewForm, Employees.GetAllEmployees());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCEmployees()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CEmployees(Employees p)
        {
            Employees.CreatrEmployees(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ContactsViewForm, Employees.GetAllEmployees());
        }

        [HttpPost]
        public IActionResult CFewEmployees(Employees p)
        {
            Employees.CreatrEmployees(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }

        //Edit
        [HttpPost]
        public IActionResult OpenEEmployees(int id)
        {
            return View(_EditForm, Employees.GetEmployeesById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesEmployees(Employees p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;
            p.Active = true;

            Employees.UpdateEmployees(p);

            return View(_ContactsViewForm, Employees.GetAllEmployees());
        }

        ////Delete
        [HttpPost]

        public IActionResult DEmployees(int id)
        {
            Employees.DeleteEmployees(id);

            return View(_ContactsViewForm, Employees.GetAllEmployees());
        }

        public IActionResult DCascade(int id)
        {
            Employees.DCascade(id);
            return View("TMeasureUnits", Employees.GetAllEmployees());
        }
    }
}
