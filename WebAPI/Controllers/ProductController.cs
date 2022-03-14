using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AppDbContext _db;
        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        //GET: api/product
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        //GET: api/product/{id}
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _db.Products.Find(id);
        }

        [HttpPost]
        public IActionResult Add(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Products.Add(model);
                    _db.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created, "Record Created Successfully.");
                }
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //PUT:api/product/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product model)
        {
            try
            {
                if (id != model.ProductId)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                _db.Products.Update(model);
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Record Updated Successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        //GET: api/product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product model = _db.Products.Find(id);
            if (model != null)
            {
                _db.Products.Remove(model);
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Record Deleted Successfully.");
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Bad Request.");
        }

        
    }
}
