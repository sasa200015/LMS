using LMS.DTO;
using LMS.Interface;
using LMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly Icourses courseRepo;
        private readonly Icategories categoryRepo;
        private readonly ProjectContext context;
        private readonly GeneralRes response;

        public CoursesController(Icourses courseRepo, Icategories categoryRepo, ProjectContext context, GeneralRes response)
        {
            this.courseRepo = courseRepo;
            this.categoryRepo = categoryRepo;
            this.context = context;
            this.response = response;
        }
        [HttpGet("GetByLevelId/{id:int}")]
        public IActionResult GetByLevelId(int id)
        {
            try
            {
                var course = courseRepo.getByLevelId(id);
                List<courseDto> courselist = new List<courseDto>();
                if (course.Any())
                {
                    foreach (var item in course)
                    {
                        courseDto courseDto = new courseDto();
                        courseDto.id = id;
                        courseDto.course_name = item.course_name;
                        courseDto.course_des = item.course_des;
                        courseDto.dep_name = item.dep_name;
                        courseDto.course_img = item.course_img;
                        courseDto.level_id = item.level_id;
                        courselist.Add(courseDto);
                    }
                    response.message = "Success";
                    response.data = courselist;
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                response.message = "Failed";
                response.data = "Not found";
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                response.message = "Failed";
                response.data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost]
        public IActionResult Add([FromForm] courseAdd courseModel)
        {
            try
            {
                var course = new courses();
                var check = context.Categories.SingleOrDefault(x => x.id == courseModel.level_id);
                if (check == null)
                {
                    response.message = "failed";
                    response.data = "The level id does not exist";
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }
                course.course_name = courseModel.course_name;
                course.course_des = courseModel.course_des;
                course.course_img = courseModel.course_img;
                course.dep_name = courseModel.dep_name;
                course.level_id = courseModel.level_id;
                courseRepo.addCourse(course);
                courseRepo.saveChange();
                var formated = new
                {
                    id = course.id,
                    course_name = courseModel.course_name,
                    course_des = courseModel.course_des,
                    course_img = courseModel.course_img,
                    dep_name = courseModel.dep_name,
                    level_id = courseModel.level_id,
                };
                response.message = "Success";
                response.data = formated;
                return StatusCode(StatusCodes.Status201Created, response);
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
