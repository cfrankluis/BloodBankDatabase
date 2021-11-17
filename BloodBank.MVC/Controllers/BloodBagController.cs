using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBank.Models.BloodBag;
using BloodBank.Service;
using BloodBank.Contracts;

namespace BloodBank.MVC.Controllers
{
    [Authorize]
    public class BloodBagController : Controller
    {
        private readonly IBloodBagService _service = new BloodBagService();

        // GET: BloodBag
        public ActionResult Index()
        {
            var model = _service.GetAllBloodBags();
            var sortedModel = model.OrderBy(e => e.BloodType);
            return View(sortedModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetBloodBagByID((int)id);

            if (model is null)
                return HttpNotFound();

            return View(model);
        }

        [HttpGet, ActionName("Detail"), ]
        public ActionResult Detail(int? id)
        {
/*            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);*/

            var model = _service.GetBloodBagByID((int)id);

            if (model is null)
                return HttpNotFound();

            return PartialView("Detail",model);
        }

        public ActionResult Create()
        {
            var model = new BloodBagCreate();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BloodBagCreate model)
        {
            if(ModelState.IsValid)
            {
                if(_service.CreateBloodBag(model))
                {
                    TempData["SaveResult"] = "A new blood donation was created.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Donation could not be created.");

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetBloodBagByID((int)id);

            if (model is null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (_service.DeleteBloodBag(id))
            {
                TempData["SaveResult"] = "A donation was deleted.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Donation could not be created.");

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var detail = _service.GetBloodBagByID((int)id);

            if (detail is null)
                return HttpNotFound();

            var viewModel = new BloodBagEdit
            {
                BloodID = detail.BloodID,
                BloodType = detail.BloodType,
                Volume = detail.Volume
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BloodBagEdit model)
        {
            if (ModelState.IsValid)
            {
                if (model.BloodID != id)
                {
                    ModelState.AddModelError("", "ID Mismatch");
                    return View(model);
                }

                if (_service.UpdateBloodBag(model))
                {
                    TempData["SaveResult"] = "A donation was updated";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Donation could not be updated");
            return View(model);
        }
    }
}