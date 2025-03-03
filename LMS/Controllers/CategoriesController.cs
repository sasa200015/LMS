using LMS.DTO;
using LMS.Interface;
using LMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly Icategories categoryRepo;
        private readonly GeneralRes response;

        public CategoriesController(Icategories categoryRepo, GeneralRes response)
        {
            this.categoryRepo = categoryRepo;
            this.response = response;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var category = categoryRepo.GetAll();
                response.message = "Success";
                response.data = category;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response.message = "Failed";
                response.data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost]
        public IActionResult Add([FromForm] categoryAdd category)
        {
            try
            {
                var categories = new categories();
                categories.name = category.name;
                categories.color = category.color;
                categoryRepo.AddCategory(categories);
                categoryRepo.saveChange();
                response.message = "Success";
                response.data = categories;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response.message = "Failed";
                response.data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }
    }
}
