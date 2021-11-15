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

        [HttpPost]
        public ActionResult _PartialCreate()
        {
            var viewModel = new PatientCreate();

            return PartialView("_PartialCreate",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
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

        [HttpPost]
        public ActionResult _PartialDetails(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetPatientByID((int)id);
            if (model is null)
                return HttpNotFound();

            return PartialView("_PartialDetails", model);
        }

        [HttpPost]
        public ActionResult _PartialDelete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetPatientByID((int)id);

            if (model is null)
                return HttpNotFound();

            return PartialView("_PartialDelete", model);
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

            return PartialView("_PartialDelete");
        }

        [HttpPost]
        public ActionResult _PartialEdit(int? id)
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

            return PartialView("_PartialEdit", viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult Edit(int id, PatientEdit model)
        {
            if (ModelState.IsValid)
            {
                if (model.PatientID != id)
                {
                    ModelState.AddModelError("", "ID Mismatch");
                    return PartialView("_PartialEdit", model);
                }

                if(_service.UpdatePatient(model))
                {
                    TempData["SaveResult"] = "A patient was updated";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Patient could not be updated");
            return PartialView("_PartialEdit",model);
        }
    }
}