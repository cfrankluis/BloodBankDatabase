using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBank.Data;
using BloodBank.Models.PatientModels;
using BloodBank.Service;

namespace BloodBank.MVC.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly PatientService _service = new PatientService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialIndex()
        {
            return PartialView("_Index",_service.GetAllPatients());
        }

        public ActionResult _PartialCreate()
        {
            var viewModel = new PatientCreate();

            return PartialView("_PartialCreate",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientCreate model)
        {
            if (ModelState.IsValid)
            {
                if (_service.CreatePatient(model))
                {
                    TempData["SaveResult"] = "A new patient was created.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Patient could not be created.");

            return PartialView("_PartialCreate",model);
        }

        public ActionResult _PartialDetails(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            TempData["PatientDetails"] = _service.GetPatientByID((int)id);
            if (TempData["PatientDetails"] is null)
                return HttpNotFound();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetPatientByID((int)id);

            if (model is null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if(_service.DeletePatient(id))
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

            var detail = _service.GetPatientByID((int)id);

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

                if(_service.UpdatePatient(model))
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