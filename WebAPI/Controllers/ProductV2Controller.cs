using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/product/v2")]
    [ApiController]
    public class ProductV2Controller : ControllerBase
    {
        AppDbContext _db;
        public ProductV2Controller(AppDbContext db)
        {
            _db = db;
        }

        //GET: api/product/v2/2
        [HttpGet("{id}")]
        public IEnumerable<Product> GetAll(int id)
        {
            return _db.Products;
        }

    }
}
