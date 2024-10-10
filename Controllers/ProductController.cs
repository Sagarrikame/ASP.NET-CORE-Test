using System;
using Microsoft.AspNetCore.Mvc;
using QHRMProject.Models;

namespace QHRMProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAllProducts();
            return View(products);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            _productRepository.AddProduct(product);

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _productRepository.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
