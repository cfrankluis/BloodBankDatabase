using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBank.Models.BloodOrder;
using BloodBank.Service;
using BloodBank.Contracts;

namespace BloodBank.MVC.Controllers
{
    [Authorize]
    public class BloodOrderController : Controller
    {
        private readonly IBloodOrderService _service = new BloodOrderService();

        public BloodOrderController()
        {

        }

        // GET: BloodOrder
        public ActionResult Index()
        {
            return RedirectToAction("Index","Patient");
        }

        public ActionResult _Index(int? id)
        {
            var model = _service.GetOrdersByPatient((int) id);

            if (model is null)
                return PartialView("_Index");

            TempData["PatientID"] = (int)id;

            return PartialView("_Index", model);
        }

        [HttpPost]
        public ActionResult _PartialCreate(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = new BloodOrderCreate();
            model.PatientID = (int)id;
            return PartialView("_PartialCreate",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
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

            return PartialView("_PartialCreate",model);
        }

        [HttpPost]
        public ActionResult _PartialDetails(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetOrderByID((int)id);

            if (model is null)
                return HttpNotFound();

            return PartialView("_PartialDetails",model);
        }

        [HttpPost]
        public ActionResult _PartialDelete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = _service.GetOrderByID((int)id);

            if (model is null)
                return HttpNotFound();

            return PartialView("_PartialDelete", model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (_service.DeleteOrder(id))
            {
                TempData["SaveResult"] = "An order was deleted.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Order could not be deleted.");

            return PartialView("_PartialDelete");
        }

        [HttpPost]
        public ActionResult _PartialEdit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var detail = _service.GetOrderByID((int)id);

            if (detail is null)
                return HttpNotFound();

            var viewModel = new BloodOrderEdit
            {
                Amount = detail.Amount,
                BloodType = detail.BloodType,
                ID = detail.ID,
                PatientID = detail.PatientID
            };

            return PartialView("_PartialEdit", viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult Edit(int id, BloodOrderEdit model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID != id)
                {
                    ModelState.AddModelError("", "ID Mismatch");
                    return PartialView("_PartialEdit", model);
                }

                if (_service.EditOrder(model))
                {
                    TempData["SaveResult"] = "An order was updated";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Order could not be updated");
            return PartialView("_PartialEdit", model);
        }
    }
}