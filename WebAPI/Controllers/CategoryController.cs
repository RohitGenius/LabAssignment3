using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        //GET: api/category
        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }
    }
}
