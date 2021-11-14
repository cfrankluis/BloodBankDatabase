using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBank.Models.Donor;
using BloodBank.Service;

namespace BloodBank.MVC.Controllers
{
    [Authorize]
    public class DonorController : Controller
    {
        private readonly DonorService _service = new DonorService(); 

        // GET: Donor
        public ActionResult Index()
        {
            var model = _service.GetAllDonors();
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetDonorByID(id);

            if (model is null)
                return HttpNotFound();

            return View(model);
        }

        public ActionResult Create()
        {
            var viewModel = new DonorCreate();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DonorCreate model)
        {
            if(ModelState.IsValid)
            {
                if(_service.CreateDonor(model))
                {
                    TempData["SaveResult"] = "A new donor was created.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Donor could not be created.");

            return View(model);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetDonorByID( (Guid) id);

            if (model is null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            if(_service.DeleteDonor(id))
            {
                TempData["SaveResult"] = "A donor was deleted.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Donor could not be deleted.");

            return View();
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var detail = _service.GetDonorByID((Guid)id);

            if (detail is null)
                return HttpNotFound();

            var viewModel = new DonorEdit
            {
                DonorID = detail.DonorID,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                BloodType = detail.BloodType,
                BirthDate = detail.BirthDate
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, DonorEdit model)
        {
            if (ModelState.IsValid)
            {
                if (model.DonorID != id)
                {
                    ModelState.AddModelError("", "ID Mismatch");
                    return View(model);
                }

                if (_service.UpdateDonor(model))
                {
                    TempData["SaveResult"] = "A donor was updated";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Donor could not be updated");
            return View(model);
        }
    }
}