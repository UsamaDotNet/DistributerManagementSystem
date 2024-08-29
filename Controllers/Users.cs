using DistributerManagementSystemBusinessLogic.Interface;
using DistributerManagementSystemModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DistributerManagementSystemWebUi.Controllers
{
    public class Users : Controller
    {
        private readonly IUsersService _userService;


        public Users(IUsersService usersService)
        {
                _userService = usersService;
        }
        // GET: Users
        public ActionResult Index()
        {
            var users =  _userService.GetAll();
            return View(users);
        }

        public ActionResult Login()
        {
            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.UserTypeList = new SelectList(new[]
            {
                new { Value = "", Text = "Select User Type" },
                new { Value = "Admin", Text = "Admin" },
                new { Value = "Field Agent", Text = "Field Agent" }
            }, "Value", "Text");

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(DistributerManagementSystemModels.Users users)
        {
            try
            {
                users.CreatedBy = "Admin";
                _userService.Add(users);
                _userService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
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

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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
