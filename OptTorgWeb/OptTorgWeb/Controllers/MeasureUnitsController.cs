using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class MeasureUnitsController : Controller
    {
        private string _ViewForm = "TMeasureUnits";
        private string _CreateForm = "CMeasureUnits";
        private string _EditForm = "EMeasureUnits";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TMeasureUnits()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_ViewForm, MeasureUnits.GetAllMeasureUnits());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCMeasureUnits()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CMeasureUnits(MeasureUnits p)
        {
            MeasureUnits.CreatrMeasureUnits(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, MeasureUnits.GetAllMeasureUnits());
        }

        [HttpPost]
        public IActionResult CFewMeasureUnits(MeasureUnits p)
        {
            MeasureUnits.CreatrMeasureUnits(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenEMeasureUnits(int id)
        {
            return View(_EditForm, MeasureUnits.GetMeasureUnitsById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesMeasureUnits(MeasureUnits p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            MeasureUnits.UpdateMeasureUnits(p);

            return View(_ViewForm, MeasureUnits.GetAllMeasureUnits());
        }

        ////Delete
        [HttpPost]
        public IActionResult DMeasureUnits(int id)
        {
            MeasureUnits.DeleteMeasureUnits(id);
            return View(_ViewForm, MeasureUnits.GetAllMeasureUnits());
        }

        public IActionResult DCascade(int id)
        {
            MeasureUnits.DCascade(id);
            return View("TMeasureUnits", MeasureUnits.GetAllMeasureUnits());
        }
    }
}
