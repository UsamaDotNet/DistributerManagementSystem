using DistributerManagementSystemBusinessLogic.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistributerManagementSystemWebUi.Controllers
{
    public class Retailer : Controller
    {

        private readonly IRetailerService _retailerService;

        public Retailer(IRetailerService retailerService)
        {
            _retailerService = retailerService;
        }

        // GET: Retailer
        public ActionResult Index()
        {
            var retailers = _retailerService.GetAll();
            return View(retailers);
        }

        // GET: Retailer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Retailer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Retailer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributerManagementSystemModels.Retailer retailer)
        {
            try
            {
                _retailerService.Add(retailer);
                _retailerService.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Retailer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Retailer/Edit/5
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

        // GET: Retailer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Retailer/Delete/5
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
