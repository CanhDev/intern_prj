using AutoMapper;
using Azure.Core;
using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Entities;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repos;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ImageHepler _imageHepler;

        public ProductController(IProductRepo repos, IMapper mapper, IWebHostEnvironment environment, ImageHepler imageHepler) {
            _repos = repos;
            _mapper = mapper;
            _environment = environment;
            _imageHepler = imageHepler;
        }
        [HttpGet]
        public async Task<IActionResult> getProducts(int? typeId, string? sortString, string? filterString, int pageNumber = 1, int pageSize = 9)
        {
            try
            {
                string role = User?.FindFirst(ClaimTypes.Role)?.Value ?? "";
                if(role != null && role == "Administrator")
                {
                    var res = await _repos.GetProductsAsync(typeId, sortString, filterString, pageNumber, pageSize, role);
                    return Ok(res);
                }
                else
                {
                    var res = await _repos.GetProductsAsync(typeId, sortString, filterString, pageNumber, pageSize);
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
                var res = await _repos.GetProductAsync(id);
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
                var api_res = await _repos.AddProductAsync(productRes);
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
                
                var api_res = await _repos.EditProductAsync(productRes, id);
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
                var api_res = await _repos.DeleteProductAsync(id);
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
