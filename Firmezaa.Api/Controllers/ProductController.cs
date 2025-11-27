using Firmezaa.Api.DTOs;
using Firmezaa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firmezaa.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _service.GetByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
        {
            var response = await _service.CreateAsync(dto);
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto dto)
        {
            var response = await _service.UpdateAsync(id, dto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}