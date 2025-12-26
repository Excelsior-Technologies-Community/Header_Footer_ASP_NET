using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Header_Footer_ASP_NET.Models;
using Header_Footer_ASP_NET.Repository;

namespace Header_Footer_ASP_NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserRepository _employeeRepo;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _employeeRepo = new UserRepository(configuration);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetEmployees()
        {
            var data = _employeeRepo.UserList();
            return Json(data);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }


        [HttpPost]
        public IActionResult Add(long EmployeeId, string Name, int Age, string City, string State, string ActionMode)
        {

            if (_employeeRepo.IsNameExists(Name))
            {
                return Json("NameExists");
            }

            _employeeRepo.Upsert(EmployeeId, Name, Age, City, State, ActionMode);
            return Json("1");
        }


        [HttpGet]
        public IActionResult GetById(long id)
        {
            var emp = _employeeRepo.UserEdit(id);
            return Json(emp);
        }


        [HttpPost]
        public IActionResult Edit(long EmployeeId, string Name, int Age, string City, string State, string ActionMode)
        {

            if (_employeeRepo.IsNameExists(Name, EmployeeId))
            {
                return Json("NameExists");
            }

            _employeeRepo.Upsert(EmployeeId, Name, Age, City, State, ActionMode);
            return Json("1");
        }


        [HttpGet("Home/Edit/{id}")]
        public IActionResult EditPage(long id)
        {
            ViewBag.EmployeeId = id;
            return View("Edit");
        }


        [HttpPost]
        public IActionResult Delete(long id)
        {
            _employeeRepo.UserDelete(id);
            return Json("1");
        }
    }
}
