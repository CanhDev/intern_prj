using intern_prj.Data_request;
using intern_prj.Data_response;
using intern_prj.Helper;
using intern_prj.Helper.jwtSerivce;
using intern_prj.Repositories;
using intern_prj.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace intern_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo) {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getCategories()
        {
            try
            {
                var res = await _categoryRepo.getCategories();
                return Ok(res); 
            }
            catch (Exception ex)
            {
                return BadRequest(new Api_response { 
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getCategory(int id)
        {
            try
            {
                var res = await _categoryRepo.getCategory(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(new Api_response
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Add([FromForm]CategoryRes categoryRes)
        {
            try
            {
                var res = await _categoryRepo.addCategory(categoryRes);
                return Ok(res);
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
        public async Task<IActionResult> Edit( int id, [FromForm] CategoryRes categoryRes)
        {
            try
            {
                var res = await _categoryRepo.editCategory(id, categoryRes);
                return Ok(res);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _categoryRepo.deleteCategory(id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(new Api_response
                {
                    success= false,
                    message=ex.Message
                });
            }
        }
    }
}
