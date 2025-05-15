using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using OptTorgWeb.Classes;
using OptTorgWeb.Models;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("LoginAsEmp");
        }

        public IActionResult OpenLoginAsCustomer()
        {
            return View("LoginAsCustomer");
        }

        public IActionResult OnLogOut()
        {
            CurrentUser.isCustomer = false;
            CurrentUser.layout = "Login";
            CurrentUser.EmployeeId = 0;
            CurrentUser.role = 1;

            return View("LoginAsCustomer");
        }

        [HttpPost]
        public IActionResult OpenLoginAsEmp()
        {
            return View("LoginAsEmp");
        }

        [HttpPost]
        public IActionResult OnLoginAsCustomer(Customers c)
        {
            Customers cus = Customers.GetCustomersById(c.IdCustomers);

            if (cus == null) 
            {
                ViewBag.Message = "Клиент не найден";
                return View("LoginAsCustomer");
            }

            if (cus.Company != c.Company)
            {
                ViewBag.Message = "Введены неверные данные";
                return View("LoginAsCustomer");
            }

            CurrentUser.isCustomer = true;
            CurrentUser.customerId = cus.IdCustomers;
            CurrentUser.layout = "_CustomersLayout";

            return View("Index");
        }

        public IActionResult OnLoginAsEmp(Employees e)
        {
            Employees emp = Employees.GetEmployeesByIdForLogin(e.IdEmployees);

            if (emp == null)
            {
                ViewBag.Message = "Сотрудник не найден";
                return View("LoginAsEmp");
            }

            if (emp.PassWord != e.PassWord)
            {
                ViewBag.Message = "Введены неверные данные";
                return View("LoginAsEmp");
            }

            switch (emp.PositionId)
            {
                //Менеджер по закупкам
                case (20):
                    CurrentUser.layout = "_SaleManLayout";
                    CurrentUser.isCustomer = false;
                    CurrentUser.EmployeeId = emp.IdEmployees;
                    break;
                //Менеджер по продажам
                case (21):
                    CurrentUser.layout = "_BuyManLayout";
                    CurrentUser.isCustomer = false;
                    CurrentUser.EmployeeId = emp.IdEmployees;
                    break;
                //Главю бух
                case (22):
                    CurrentUser.isCustomer = false;
                    CurrentUser.layout = "_GlBuhLayout";
                    CurrentUser.EmployeeId = emp.IdEmployees;
                    break;
                //Зав. склад
                case (24):
                    CurrentUser.isCustomer = false;
                    CurrentUser.layout = "_ZavSkladLayout";
                    CurrentUser.EmployeeId = emp.IdEmployees;
                    break;
                //Админ
                case (23):
                    CurrentUser.isCustomer = false;
                    CurrentUser.layout = "_Layout";
                    CurrentUser.EmployeeId = emp.IdEmployees;
                    CurrentUser.role = 0;
                    break;
                default:
                    ViewBag.Message = "Введены неверные данные";
                    return View("LoginAsEmp");
            }

            return View("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
