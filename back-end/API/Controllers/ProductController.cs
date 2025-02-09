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
        public async Task<IActionResult> AddProduct(ProductDto addProductDto)
        {
            bool result = await _mediator.Send(new AddProductCommandRequest { AddProduct = addProductDto });
            if (result) return Ok(new { message = "Product added successfully." });
            return BadRequest(new { message = "Failed to add product." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            bool result = await _mediator.Send(new UpdateProductCommand { UpdateProductRequest = updateProductDto });
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
            var deleteProductDto = new DeleteProductDto { Id = id };

            var result = await _mediator.Send(new DeleteProductCommand { DeleteProductRequest = deleteProductDto });

            if (result)
                return Ok(new
                {
                    message = "User registered successfully."
                });
            else
                return BadRequest(new
                {
                    message = "User registration failed."
                });
        }
    }
}
