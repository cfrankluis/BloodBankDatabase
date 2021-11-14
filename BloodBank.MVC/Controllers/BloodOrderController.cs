using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBank.Models.BloodOrder;
using BloodBank.Service;
using BloodBank.Data;

namespace BloodBank.MVC.Controllers
{
    [Authorize]
    public class BloodOrderController : Controller
    {
        private readonly BloodOrderService _service = new BloodOrderService();
        
        // GET: BloodOrder
/*        public ActionResult Index()
        {
            var model = _service.GetAllOrders();
            return PartialView("_Index",model);
        }*/

        [ChildActionOnly]
        public ActionResult Index(int? id)
        {
            var model = _service.GetOrdersByPatient((int) id);

            if (model is null)
                return PartialView("_Index");

            return PartialView("_Index",model);
        }

        public ActionResult Create()
        {
            var model = new BloodOrderCreate();
            var patientService = new PatientService();
            TempData["Patients"] =
                patientService
                .GetAllPatients()
                .Select(
                    e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.PatientID.ToString()
                    });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BloodOrderCreate model)
        {
            if (ModelState.IsValid)
            {
                if (_service.CreateOrder(model))
                {
                    TempData["SaveResult"] = "A new order was created.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Order could not be created.");

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetOrderByID((int)id);

            if (model is null)
                return HttpNotFound();

            return View(model);
        }
    }
}