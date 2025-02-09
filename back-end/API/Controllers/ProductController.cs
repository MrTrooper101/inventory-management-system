using back_end.Application.Features.Categories.Commands;
using back_end.Application.Features.Categories.Dtos;
using back_end.Application.Features.Categories.Queries;
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
        public async Task<IActionResult> AddCategory(AddCategoryDto addCategoryDto)
        {
            bool result = await _mediator.Send(new AddCategoryCommand { AddCategoryRequest = addCategoryDto });
            if (result) return Ok(new { message = "Category added successfully." });
            return BadRequest(new { message = "Failed to add category." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            bool result = await _mediator.Send(new UpdateCategoryCommand { UpdateCategoryRequest = updateCategoryDto });
            if (result)
                return Ok(new { message = "Category updated successfully." });
            else
                return BadRequest(new
                {
                    message = "Category update failed."
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleteCategoryDto = new DeleteCategoryDto { Id = id };

            var result = await _mediator.Send(new DeleteCategoryCommand { DeleteCategoryRequest = deleteCategoryDto });

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
