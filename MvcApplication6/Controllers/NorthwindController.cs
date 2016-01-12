using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using MvcApplication6.Models;

namespace MvcApplication6.Controllers
{
    public class NorthwindController : Controller
    {
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NORTHWND;Integrated Security=True";
        public ActionResult Categories()
        {
            NorthwindDb db = new NorthwindDb(connectionString);
            IEnumerable<Category> categories = db.GetAll();
            return View(categories);
        }

        public ActionResult Products(int catid)
        {
            NorthwindDb db = new NorthwindDb(connectionString);
            Category category = db.GetCategoryById(catid);
            IEnumerable<Product> products = db.GetProductsForCategory(catid);
            ProductsViewModel viewModel = new ProductsViewModel();
            viewModel.Products = products;
            viewModel.Category = category;
            return View(viewModel);
        }

    }
}
