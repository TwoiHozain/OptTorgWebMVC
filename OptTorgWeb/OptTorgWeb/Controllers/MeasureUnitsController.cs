using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class MeasureUnitsController : Controller
    {
        private string _ViewForm = "TMeasureUnits";
        private string _CreateForm = "CMeasureUnits";
        private string _EditForm = "EMeasureUnits";

        //Read
        [HttpGet]
        public IActionResult TMeasureUnits()
        {
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
            MeasureUnits.CreatrPosition(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, MeasureUnits.GetAllMeasureUnits());
        }

        [HttpPost]
        public IActionResult CFewMeasureUnits(MeasureUnits p)
        {
            MeasureUnits.CreatrPosition(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenEMeasureUnits(int id)
        {
            return View(_EditForm, MeasureUnits.GetPositionById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesMeasureUnits(MeasureUnits p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            MeasureUnits.UpdatePosition(p);

            return View(_ViewForm, MeasureUnits.GetAllMeasureUnits());
        }

        ////Delete
        [HttpPost]
        public IActionResult DPosition(int id)
        {
            MeasureUnits.DeletePosition(id);
            return View(_ViewForm, MeasureUnits.GetAllMeasureUnits());
        }
        //ToDo
        public IActionResult DCascadePosition(int id)
        {
            return View("TMeasureUnits", MeasureUnits.GetAllMeasureUnits());
        }
    }
}
