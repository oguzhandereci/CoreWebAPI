using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebAPI.Data;
using CoreWebAPI.Models;
using CoreWebAPI.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CoreWebAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;

            if (_context.Categories.Count() == 0)
            {
                _context.Categories.Add(new Category()
                {
                    CategoryName = "Beverages"
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var data = _context.Categories.Find(id);
            if (data == null)
                return NotFound();
            return data;
        }

        public ActionResult Add(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = _context.Categories.Add(new Category()
                {
                    CategoryName = model.CategoryName
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message",$"Bir hata oluştu {ex.Message}");
            }

            //return CreatedAtAction(nameof(GetById), new {id = model.Id}, model);
            return Ok();
        }


    }
}