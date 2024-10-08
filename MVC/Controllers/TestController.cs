﻿
using BLL.DAL;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class TestController : Controller
    {
        private readonly Db _db;

        public TestController(Db db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Product> list = _db.Products.ToList();
            return View("ProductList", list);
        }
    }
}
