using DistributerManagementSystemBusinessLogic.Interface;
using DistributerManagementSystemModels;
using DistributerManagementSystemRepository.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace DistributerManagementSystemWebUi.Controllers
{
    public class ProductPurchaseOrderbook : Controller
    {
        private readonly IProductPurchaseOrderbookService _productPurchaseOrderbookService;
        private readonly DistributerManagementSystemContext _context;
        private readonly IProductsService _productsService;


        public ProductPurchaseOrderbook(IProductPurchaseOrderbookService productPurchaseOrderbookService, DistributerManagementSystemContext context, IProductsService productsService)
        {
            _productPurchaseOrderbookService = productPurchaseOrderbookService;
            _context = context;
            _productsService = productsService;
        }
        // GET: ProductPurchaseOrderbook
        public ActionResult Index()
        {
            var order = _productPurchaseOrderbookService.GetAll();
            return View(order);
        }

        // GET: ProductPurchaseOrderbook/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: ProductPurchaseOrderbook/Create
        public ActionResult Create()
        {
            // ViewBag for the first dropdown
            ViewBag.ProductDropdown = new SelectList(_context.Products.Select(x => new { x.Id, x.ProductName }), "Id", "ProductName");

            // ViewBag for the Category dropdown
            ViewBag.CategoryDropdown = new SelectList(_context.Products.Select(x => new { x.Id, x.ProductCategory }), "Id", "ProductCategory");

            // ViewBag for the Unit dropdown
            ViewBag.UnitDropdown = new SelectList(_context.Products.Select(x => new { x.Id, x.Unit }), "Id", "Unit");

            ViewBag.Vendor = new SelectList(_context.Vendors.Select(x => new {x.Id, x.Name}), "Id", "Name");

            
            return View();
        }


        // POST: ProductPurchaseOrderbook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributerManagementSystemModels.ProductPurchaseOrderbook purchaseOrder)
        {
            try
            {
                var pProductName = Guid.Parse(purchaseOrder.ProductName);

                var vendor = Guid.Parse(purchaseOrder.PurchasedByVendor);
                var vendorName = _context.Vendors.Where(x => x.Id == vendor).FirstOrDefault();

                var tempPName = _context.Products.Where(x=>x.Id == pProductName).FirstOrDefault();
                purchaseOrder.ProductName = tempPName.ProductName;
                purchaseOrder.ProductCategory = tempPName.ProductCategory;
                purchaseOrder.PurchasedByVendor = vendorName.Name;


                _productPurchaseOrderbookService.Add(purchaseOrder);
                _productPurchaseOrderbookService.Save();

                var pr = _productsService.FindBy(x => x.ProductName == purchaseOrder.ProductName).FirstOrDefault();

                if (pr != null && purchaseOrder.Quantity > 0)
                {
                    // Increment the quantity
                    pr.Quantity += purchaseOrder.Quantity;

                    // Save changes
                    _productsService.Save();
                }
                





                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductPurchaseOrderbook/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductPurchaseOrderbook/Edit/5
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

        // GET: ProductPurchaseOrderbook/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductPurchaseOrderbook/Delete/5
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
