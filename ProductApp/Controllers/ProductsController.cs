using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Interface;
using ProductApp.Model;
using ProductApp.Services;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _productServices.GetProducts();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = _productServices.GetproductById(id);

            if (product is null)
                return NotFound($"Product with id={id} was not found.");

            return Ok(product);
        }


        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Product data is invalid.");
            }

            var createdProduct = _productServices.AddItem(newProduct);

            // برگرداندن وضعیت 201 (Created) به همراه محصول ساخته شده
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }




        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public IActionResult Update(int id ,Product newProduct) 
        {
           if (id != newProduct.Id)
            {
                return BadRequest();
            }
             var UpdateProduct =_productServices.UpdateItem(newProduct);
            if (UpdateProduct == null)
            {
                return NotFound();
            }
            return Ok(UpdateProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id )
        {
            
            var deletedProduct = _productServices.DeleteItem(id);
            if (deletedProduct == null)
            {
                return NotFound("Product not found");
            }
            return Ok(deletedProduct);
        }

        














    }
}
