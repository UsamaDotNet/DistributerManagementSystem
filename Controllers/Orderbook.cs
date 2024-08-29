
using DistributerManagementSystemBusinessLogic.Interface;
using DistributerManagementSystemModels;
using DistributerManagementSystemRepository.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DistributerManagementSystemWebUi.Controllers
{
    public class Orderbook : Controller
    {
        private readonly IOrderbookService _orderbookService;
        private readonly IProductsService _productsService;
        private readonly IRetailerService _retailerService;
        private readonly DistributerManagementSystemContext _context;

        public Orderbook(IOrderbookService orderbookService, IProductsService productsService, IRetailerService retailerService, DistributerManagementSystemContext context)
        {
            _context = context;
            _productsService = productsService;
            _retailerService = retailerService;
            _orderbookService = orderbookService;
        }
        // GET: Orderbook
        public ActionResult Index()
        {


            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }

            var orders = _orderbookService.FindBy(x=>x.OrderStatus != "Dispatched").ToList();
            return View(orders);
        }

        public ActionResult DispatchedOrders()
        {
            var orders = _orderbookService.FindBy(x => x.OrderStatus == "Dispatched").ToList();
            return View(orders);
        }

        // GET: Orderbook/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //Get orderbook by retailer
        public ActionResult GetOrdersByRetailer(string retailerName)
        {
            return View();
        }


        //Order Creation Success Page for field agent
        public ActionResult OrderCreated()
        {
            return View();
        }

        // GET: Orderbook/Create
        public ActionResult Create(string message = null)
        {
            if (message != null)
            {
                ViewBag.CustomMessage = message;
            }

            ViewBag.ProductName =  new SelectList(_context.Products.Select(x => new { x.Id, x.ProductName }), "Id", "ProductName");

            ViewBag.Retailer = new SelectList(_context.Retailers.Select(x => new { x.Id, x.ReailerName }), "Id", "ReailerName");

            ViewBag.Rate = new SelectList(_context.Products.Select(x => new { x.Id, x.Price }), "Id", "Price");
           
            return View();
        }



        // POST: Orderbook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributerManagementSystemModels.Orderbook orderbook, string? AddMore)
        {
            try
            {
                var prodId = Guid.Parse(orderbook.ProductName);

                var Product = _context.Products.Where(x => x.Id == prodId).FirstOrDefault();

                var retailerId = Guid.Parse(orderbook.RetailerName);

                var Retailer = _context.Retailers.Where(x=>x.Id == retailerId).FirstOrDefault();

                orderbook.ProductName = Product.ProductName;
                orderbook.RetailerName = Retailer.ReailerName;
                orderbook.OrderStatus = "Ordered";
                orderbook.OrderNumber = _context.Orderbooks.Count() - 1;
                orderbook.BalanceAmount= orderbook.TotalAmount - orderbook.AmountPaid;

               
                orderbook.PaymentDueDate = DateTime.Today.AddDays(14);

                Product.Quantity = Product.Quantity - orderbook.Quantity;
                _productsService.Save();

                _orderbookService.Add(orderbook);
                _orderbookService.Save();

                if (AddMore == "AddMore")
                {
                    string Msg = "Added Successfully";
                    return RedirectToAction("Create", new {message = Msg});
                }
                else if (AddMore == "ReviewOrder")
                {
                    string RetailerName = orderbook.RetailerName;
                    return RedirectToAction("ReviewOrder", new {message = "Review Order", RetailerName = RetailerName});
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //get review order

        public ActionResult ReviewOrder(string message = null, string RetailerName = null)
        {

            if (message != null)
            {

                var orders = _orderbookService.GetAll().Where(x => x.RetailerName == RetailerName && x.DateCreated == DateTime.Today).ToList();
                return View(orders);
            }

            return View("Index");
        }

        //Edit order from review
        public ActionResult EditOrderFromReview(string Id)
        {
            var OrderId = Guid.Parse(Id);
            var existingorder = _orderbookService.FindBy(x => x.Id == OrderId).FirstOrDefault();

            return View(existingorder);
        }

        [HttpPost]
        //post method for edit order
        public ActionResult EditOrderFromReview(DistributerManagementSystemModels.Orderbook orderbook)
        {
            var existingOrder = _orderbookService.FindBy(x => x.Id == orderbook.Id).FirstOrDefault();

            if (existingOrder != null)
            {
                try
                {
                    // Update the fields with the new values
                    existingOrder.ProductName = orderbook.ProductName;
                    existingOrder.Quantity = orderbook.Quantity;
                    existingOrder.TotalAmount = orderbook.TotalAmount;
                    existingOrder.IsPaid = orderbook.IsPaid;
                    existingOrder.OrderStatus = orderbook.OrderStatus;
                    existingOrder.DateCreated = DateTime.Today;

        

                    _orderbookService.EditOrder(existingOrder);

                    _orderbookService.Save();

                    return RedirectToAction("ReviewOrder", new { message = "Changes Saved", RetailerName = existingOrder.RetailerName });
                }
                catch (Exception e)
                {
                    // Handle any exceptions that occur during update
                    ModelState.AddModelError("", "Error while saving changes: " + e.Message);
                    return View(orderbook); // Return the view with the existing order data
                }
            }
            else
            {
                return RedirectToAction("ReviewOrder");
            }
        }


        //get orders for specific Field Agent
        public ActionResult FieldAgentOrders(string message = null)
        {
            if (message != null)
            {
                ViewBag.Message = message;
            }
            //add user criteria after creating login
            var Orders = _orderbookService.GetAll().Where(x=>x.DateCreated == DateTime.Today).ToList();
            return View(Orders);
        }


        //dispatch of order by admin
        public ActionResult Dispatched(string id)
        {
                var guid = Guid.Parse(id);
            var order = _orderbookService.FindBy(x=>x.Id == guid).FirstOrDefault();
            order.OrderStatus = "Dispatched";
            _orderbookService.Save();

            var successmessage = "Order number " + order.OrderNumber + " is Dipatched Successfully";

            TempData["Message"] = successmessage;

            return RedirectToAction("Index", new {message = successmessage});
        }

        // GET: Orderbook/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orderbook/Edit/5
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

        // GET: Orderbook/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }




        // POST: Orderbook/Delete/5
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
