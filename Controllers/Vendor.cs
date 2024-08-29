using DistributerManagementSystemBusinessLogic.Interface;
using DistributerManagementSystemModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistributerManagementSystemWebUi.Controllers
{
    public class Vendor : Controller
    {

        private readonly IVendorService _vendorService;

        public Vendor(IVendorService vendorService)
        {
                _vendorService = vendorService;
        }
        // GET: Vendor
        public ActionResult Index()
        {
            var vendors = _vendorService.GetAll();
            return View(vendors);
        }

        // GET: Vendor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Vendor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributerManagementSystemModels.Vendor vendor)
        {
            try
            {
                _vendorService.Add(vendor);
                _vendorService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Vendor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vendor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
