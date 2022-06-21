using FirstApi.DAL;
using FirstApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        //private List<Product> _products = new List<Product>()
        //{
        //    new Product
        //    {
        //        Id=1,
        //        Name="Phone",
        //        Price = 2000.9m
        //    },
        //    new Product
        //    {
        //        Id=2,
        //        Name="Computer",
        //        Price = 3000.9m
        //    },
        //    new Product
        //    {
        //        Id=1,
        //        Name="Book",
        //        Price = 5.5m
        //    },
        //    new Product
        //    {
        //        Id=1,
        //        Name="Bag",
        //        Price = 50.8m
        //    },
        //    new Product
        //    {
        //        Id=1,
        //        Name="Earphone",
        //        Price = 600.9m
        //    }

        //};
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        //[Route("get/{id}")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.FirstOrDefault(p=>p.Id==id);
            if (product == null) return NotFound();
            
            return Ok(product);
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<Product> model = _context.Products.ToList();
            return Ok(model);
        }
        [HttpPost("create")]
        public IActionResult Create(Product product)
        {
            if (product.Name.Length > 5) return StatusCode(StatusCodes.Status404NotFound);
            _context.Products.Add(product);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut("update")]
        public IActionResult Update(Product product)
        {
            Product existed = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existed == null) return NotFound();
            _context.Entry(existed).CurrentValues.SetValues(product);
            _context.SaveChanges();
            return Ok(existed);
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            Product existed = _context.Products.Find(id);
            if (existed == null) return NotFound();
            _context.Remove(existed);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK,new { id });
        }
    }
}
