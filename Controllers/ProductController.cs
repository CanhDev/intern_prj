using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> getProducts(int? typeId, string? sortString, string? filterString, int pageNumber = 1, int pageSize = 9)
        {
            try
            {
                string role = User?.FindFirst(ClaimTypes.Role)?.Value ?? "";
                if(role != null && role == "Administrator")
                {
                    var res = await _productService.GetProductsAsync(typeId, sortString, filterString, pageNumber, pageSize, role);
                    return Ok(res);
                }
                else
                {
                    var res = await _productService.GetProductsAsync(typeId, sortString, filterString, pageNumber, pageSize);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getProduct(int id)
        {
            try
            {
                var res = await _productService.GetProductAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Add([FromForm] productRes productRes) 
        {
            try
            {
                var api_res = await _productService.AddProductAsync(productRes);
                return Ok(api_res);

            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Edit(int id, [FromForm] productRes productRes)
        {
            try
            {
                
                var api_res = await _productService.EditProductAsync(productRes, id);
                return Ok(api_res);
            }
            catch (Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success= false,
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var api_res = await _productService.DeleteProductAsync(id);
                return Ok(api_res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
