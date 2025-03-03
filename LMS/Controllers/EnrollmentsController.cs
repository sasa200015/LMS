using System.Linq;
using LMS.DTO;
using LMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly ProjectContext context;
        private readonly GeneralRes response;

        public EnrollmentsController(ProjectContext context, GeneralRes response)
        {
            this.context = context;
            this.response = response;
        }
        [HttpGet("check/{id:int}/{courseId:int}")]
        public IActionResult CheckEnroll(int id, int courseId)
        {
            try
            {
                var enrollment = context.Enrollments.SingleOrDefault(x => x.student_id == id && x.course_id == courseId);
                if (enrollment != null)
                {
                    var format = new
                    {
                        CourseId = enrollment.course_id,
                        StudentId = enrollment.student_id
                    };
                    response.message = "Success";
                    response.data = format;
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                response.message = "Failed";
                response.data = "Not enrolled , or student id and course id inncorrect";
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                response.message = "Failed";
                response.data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpGet("GetByStudentId/{id:int}")]
        public IActionResult GetByStudent(int id)
        {
            try
            {
                var enroll = context.Enrollments.Where(x => x.student_id == id).ToList();
                if (enroll.Any())
                {
                    List<dynamic> format = new List<dynamic>();
                    foreach (var item in enroll)
                    {
                        format.Add(new
                        {
                            courseId = item.course_id,
                            StudentId = item.student_id,
                        });
                    }
                    response.message = "Success";
                    response.data = format;
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                response.message = "Failed";
                response.data = "Not enrolled , or student id inncorrect";
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                response.message = "Failed";
                response.data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpGet("GetByCourseId/{id:int}")]
        public IActionResult GetByCourse(int id)
        {
            try
            {
                var enroll = context.Enrollments.Where(x => x.course_id == id).ToList();
                if (enroll.Any())
                {
                    List<dynamic> format = new List<dynamic>();
                    foreach (var item in enroll)
                    {
                        format.Add(new
                        {
                            courseId = item.course_id,
                            StudentId = item.student_id,
                        });
                    }
                    response.message = "Success";
                    response.data = format;
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                response.message = "Failed";
                response.data = "Not enrolled , or course id inncorrect";
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
        public IActionResult AddEnroll([FromForm] enrollAdd enrollModel)
        {
            try
            {
                var enroll = new enrollments();
                var student = context.Students.SingleOrDefault(x => x.id == enrollModel.student_id);
                var course = context.Courses.SingleOrDefault(x => x.id == enrollModel.course_id);
                if (course != null && student != null)
                {
                    enroll.student_id = enrollModel.student_id;
                    enroll.course_id = enrollModel.course_id;
                    context.Enrollments.Add(enroll);
                    context.SaveChanges();
                    var format = new { id = enroll.id, coureseId = enroll.course_id, studentId = enroll.student_id };
                    response.message = "Success";
                    response.data = format;
                    return StatusCode(StatusCodes.Status201Created, response);
                }
                response.message = "Failed";
                response.data = "Student id or course id inncorrect";
                return StatusCode(StatusCodes.Status400BadRequest, response);
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
