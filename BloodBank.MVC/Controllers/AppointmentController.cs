using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBank.Models.DonorAppointment;
using BloodBank.Service;
using BloodBank.Contracts;

namespace BloodBank.MVC.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IDonorAppointmentService _service = new DonorAppointmentService();

        // GET: Appointment
        public ActionResult Index()
        {
            var viewModel = _service.GetAllAppointments();
            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var viewModel = _service.GetAppointmentById((int)id);

            if (viewModel is null)
                return HttpNotFound();

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new AppointmentCreate();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentCreate model)
        {
            if (ModelState.IsValid)
            {
                if (_service.AppointmentCreate(model))
                {
                    TempData["SaveResult"] = "A new appointment was created.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Appointment could not be created.");

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var viewModel = _service.GetAppointmentById((int)id);

            if (viewModel is null)
                return HttpNotFound();

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if(_service.DeleteAppointment(id))
            {
                TempData["SaveResult"] = "An appointment was deleted.";
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", "Appointment could not be deleted.");

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var detail = _service.GetAppointmentById((int)id);

            if (detail is null)
                return HttpNotFound();

            var viewModel = new AppointmentEdit
            {
                AppointmentID = detail.AppointmentID,
                AppointmentTime = detail.AppoinmentTime,
                DonorId = detail.DonorId,
                Status = detail.Status
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AppointmentEdit model)
        {
            if(ModelState.IsValid)
            {
                if (model.AppointmentID != id)
                {
                    ModelState.AddModelError("", "ID Mismatch");
                    return View(model);
                }

                if(_service.UpdateAppointment(model))
                {
                    TempData["SaveResult"] = "An Appointment was updated";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Appointment could not be updated");
            return View(model);
        }
    }
}