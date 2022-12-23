using ModelPopup.Models;
using ModelPopup.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelPopup.Controllers
{
    public class HomeController : Controller
    {
        Database database = new Database();
        public ActionResult Index()
        {
            Employee emp = new Employee();
            emp.stateList.Add(new SelectListItem() { Text = "Gujarat", Value = "1", Selected = false });
            emp.stateList.Add(new SelectListItem() { Text = "Maharastra", Value = "2", Selected = false });
            emp.empList = database.GetEmployee();
            return View(emp);
        }

        [HttpPost]
        public JsonResult GetCity(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (id == 1)
            {
                list.Add(new SelectListItem() { Text = "Surat", Value = "11", Selected = false });
                list.Add(new SelectListItem() { Text = "Bardoli", Value = "12", Selected = false });
                list.Add(new SelectListItem() { Text = "Baroda", Value = "13", Selected = false });
                return Json(list);
            }
            else
            {
                list.Add(new SelectListItem() { Text = "Pune", Value = "21", Selected = false });
                list.Add(new SelectListItem() { Text = "Mumbai", Value = "22", Selected = false });
                return Json(list);
            }
        }

        [HttpPost]
        public ActionResult EmployeeDetails(Employee employee)
        {
            bool isInserted= database.uploadEmployeeDetail(employee);
            return RedirectToAction("Index","Home",isInserted);
        }

        [HttpPost]
        public JsonResult GetEmployeeData(String id)
        {
            return Json(database.GetEmployeeData(id));
        }

        [HttpPost]
        public ActionResult DeleteEmployeeData(String id)
        {
            database.DeleteEmployeeData(id);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}