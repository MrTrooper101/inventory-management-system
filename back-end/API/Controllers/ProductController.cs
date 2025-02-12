using back_end.Application.Features.Products.Requests.Commands;
using back_end.Application.Features.Products.Dtos;
using back_end.Application.Features.Products.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace back_end.API.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQueryRequest { });

            if (result != null && result.Any())
            {
                return Ok(result);
            }

            return NotFound("No categories found.");
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto productDto)
        {
            bool result = await _mediator.Send(new AddProductCommandRequest { AddProduct = productDto });
            if (result) return Ok(new { message = "Product added successfully." });
            return BadRequest(new { message = "Failed to add product." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            bool result = await _mediator.Send(new UpdateProductCommandRequest { UpdateProduct = productDto });
            if (result)
                return Ok(new { message = "Product updated successfully." });
            else
                return BadRequest(new
                {
                    message = "Product update failed."
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommandRequest(id));

            if (result)
                return Ok(new
                {
                    message = "Product deleted successfully."
                });
            else
                return BadRequest(new
                {
                    message = "Failed to delete product."
                });
        }
    }
}
