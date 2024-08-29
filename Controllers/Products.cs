using DistributerManagementSystemBusinessLogic.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace DistributerManagementSystemWebUi.Controllers
{
    public class Products : Controller
    {
        // GET: Products

        private readonly IProductsService _productsService;

        public Products(IProductsService productsService)
        {
                _productsService = productsService;
        }
        public ActionResult Index()
        {
            var products = _productsService.GetAll();

            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Unit = new SelectList(new[]
            {
                new {Value = "", Text = "Select Type"},
                new {Value="Length" , Text = "Length"},
                new {Value = "Mass", Text = "Mass"},
                new {Value ="Volume" , Text = "Volume"},
                new {Value = "Size", Text = "Size"},
            }, "Value", "Text");


            ViewBag.UnitValues = new Dictionary<string, List<SelectListItem>>()
            {
                { "Length", new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Meters", Text = "Meters" }
                    }
                },
                { "Mass", new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Kilogram", Text = "Kilogram" }
                    }
                },
                { "Volume", new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Liters", Text = "Liters" }
                    }
                }
            };

            ViewBag.Size = new SelectList(new[]
            {
                new {Value = "Small", Text = "Small"},
                new {Value = "Medium", Text = "Medium"},
                new {Value = "Large", Text = "Large"},
            },"Value","Text");


            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributerManagementSystemModels.Products product)
        {
            try
            {
                //more work to do here
                _productsService.Add(product);
                _productsService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
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

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
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
