using Firmezaa.Api.Data.Entities;
using Firmezaa.Api.DTOs.Sale;
using Firmezaa.Api.DTOs.SaleProduct;
using Firmezaa.Api.Services.Interfaces;
using Firmezaa.Api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Firmezaa.Api.Controllers
{
    [Route("api/sales")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ISaleProductService _saleProductService;
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;

        public SaleController(ISaleService saleService, ISaleProductService saleProductService, IEmailService emailService, UserManager<IdentityUser> userManager)
        {
            _saleService = saleService;
            _saleProductService = saleProductService;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _saleService.GetAllAsync();
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _saleService.GetByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleCreateDto dto)
        {
            var response = await _saleService.CreateAsync(dto);
            return StatusCode(response.Code, response);
        }


        [HttpPost("full")]
        public async Task<IActionResult> CreateFullSale([FromBody] SaleFullCreateDto dto)
        {
            var response = new ApiResponse<object>();

            if (dto == null || dto.Products.Count == 0)
            {
                response.Code = 400;
                response.Success = false;
                response.Message = "Debe enviar al menos un producto.";
                response.Payload = null;
                return StatusCode(response.Code, response);
            }

            // Obtener el usuario desde el JWT
            var userId = User?.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                response.Code = 401;
                response.Success = false;
                response.Message = "Usuario no autorizado.";
                response.Payload = null;
                return StatusCode(response.Code, response);
            }

            var saleCreate = new SaleCreateDto
            {
                ClientId = userId
            };

            var saleResp = await _saleService.CreateAsync(saleCreate);

            if (!saleResp.Success || saleResp.Payload is not SaleResponseDto saleData)
            {
                response.Code = saleResp.Code;
                response.Success = false;
                response.Message = saleResp.Message;
                response.Payload = null;
                return StatusCode(response.Code, response);
            }

            var saleId = saleData.Id;
            var createdProducts = new List<SaleProduct>();

            foreach (var p in dto.Products)
            {
                var spRequest = new SaleProductCreateDto
                {
                    SaleId = saleId,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice
                };

                var spResp = await _saleProductService.CreateAsync(spRequest);
                if (!spResp.Success)
                {
                    response.Code = spResp.Code;
                    response.Success = false;
                    response.Message = $"Error creando un producto: {spResp.Message}";
                    response.Payload = null;
                    return StatusCode(response.Code, response);
                }

                createdProducts.Add(spResp.Payload!);
            }

            response.Code = 201;
            response.Success = true;
            response.Message = "Compra realizada correctamente.";
            response.Payload = new
            {
                Sale = saleData,
                Products = createdProducts
            };

  
            var client = _userManager.FindByIdAsync(userId);

            if (client.Result != null)
            {
                _emailService.SendPurcharseConfirmation(client.Result.Email);
            }

            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SaleUpdateDto dto)
        {
            var response = await _saleService.UpdateAsync(id, dto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _saleService.DeleteAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
