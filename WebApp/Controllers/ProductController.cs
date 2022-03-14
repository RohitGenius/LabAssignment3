using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client;
        IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            Uri apiAddress = new Uri(_configuration["ApiAddress"]);
            client.BaseAddress = apiAddress;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> model = new List<ProductModel>();
            var response = client.GetAsync(client.BaseAddress + "/product").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(data);
            }
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            ModelState.Remove("ProductId");
            if (ModelState.IsValid)
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.PostAsJsonAsync(client.BaseAddress + "/product", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetCategories().ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            ProductModel model = new ProductModel();
            ViewBag.Categories = GetCategories().ToList();
            var response = client.GetAsync(client.BaseAddress + "/product/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<ProductModel>(data);
            }
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = client.PutAsJsonAsync(client.BaseAddress + "/product/" + model.ProductId, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetCategories().ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {
            var response = client.DeleteAsync(client.BaseAddress + "/product/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
            }
            return RedirectToAction("Index");
        }

        private IEnumerable<CategoryModel> GetCategories()
        {
            IEnumerable<CategoryModel> model = new List<CategoryModel>();
            var response = client.GetAsync(client.BaseAddress + "/category").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<IEnumerable<CategoryModel>>(data);
            }
            return model;
        }
    }
}
