using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;

namespace EURISTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager productManager;

        /// <summary>
        /// Costructor
        /// </summary>
        public ProductController()
        {
            productManager = new ProductManager();
        }

        /// <summary>
        /// Products index page
        /// </summary>
        /// <returns>Products index page</returns>
        public ActionResult Index()
        {
            List<Product> products = productManager.GetProducts();

            return View(products);
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <returns>Create product view</returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }

        /// <summary>
        /// Create product form post
        /// </summary>
        /// <param name="product"></param>
        /// <returns>View with error message if not succeded</returns>
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);

            var result = productManager.CreateProduct(product);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(product);
            }
        }

        /// <summary>
        /// Displays product info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Info(int id)
        {
            var product = productManager.FindProduct(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(product);
        }

        /// <summary>
        /// Edit product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = productManager.FindProduct(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var result = productManager.UpdateProduct(product);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(product);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = productManager.FindProduct(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = productManager.FindProduct(id);

            if (product == null)
                return View();

            var result = productManager.DeleteProduct(product);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(product);
            }
        }
    }
}
