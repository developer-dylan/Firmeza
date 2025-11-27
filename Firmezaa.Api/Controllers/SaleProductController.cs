using Firmezaa.Api.DTOs.SaleProduct;
using Firmezaa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Firmezaa.Api.Controllers
{
    [Route("api/sale-products")]
    [ApiController]
    public class SaleProductController : ControllerBase
    {
        private readonly ISaleProductService _service;

        public SaleProductController(ISaleProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _service.GetByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleProductCreateDto request)
        {
            var response = await _service.CreateAsync(request);
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SaleProductUpdateDto request)
        {
            var response = await _service.UpdateAsync(id, request);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}