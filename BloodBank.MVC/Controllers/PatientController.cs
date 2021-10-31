using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBank.Data;
using BloodBank.Models.Patient;
using BloodBank.Service;

namespace BloodBank.MVC.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            var service = new PatientServices();

            return View(service.GetAllPatients());
        }

        public ActionResult Create()
        {
            var viewModel = new PatientCreate();

            return View(viewModel);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = new PatientServices();
                if (service.CreatePatient(model))
                {
                    TempData["SaveResult"] = "A new patient was created.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Patient could not be created.");

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var service = new PatientServices();

            var model = service.GetPatientByID((int)id);

            if (model is null)
                return HttpNotFound();

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var service = new PatientServices();

            var model = service.GetPatientByID((int)id);

            if (model is null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var service = new PatientServices();
            if(service.DeletePatient(id))
            {
                TempData["SaveResult"] = "A patient was deleted.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Patient could not be created.");

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var service = new PatientServices();
            var detail = service.GetPatientByID((int)id);

            if (detail is null)
                return HttpNotFound();

            var viewModel = new PatientEdit
            {
                PatientID = detail.PatientID,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                BloodType = detail.BloodType,
                BirthDate = detail.BirthDate
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientEdit model)
        {
            if (ModelState.IsValid)
            {
                if (model.PatientID != id)
                {
                    ModelState.AddModelError("", "ID Mismatch");
                    return View(model);
                }

                var service = new PatientServices();

                if(service.UpdatePatient(model))
                {
                    TempData["SaveResult"] = "A patient was updated";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Patient could not be updated");
            return View(model);
        }
    }
}