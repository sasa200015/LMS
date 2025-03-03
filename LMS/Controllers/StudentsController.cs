using LMS.DTO;
using LMS.Interface;
using LMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly Istudents studnetrepo;
        private readonly GeneralRes response;
        public StudentsController(Istudents studnetrepo, GeneralRes response)
        {
            this.studnetrepo = studnetrepo;
            this.response = response;
        }
        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                studentDto studentDto = new studentDto();
                students student = studnetrepo.GetByEmail(email);
                if (student != null)
                {
                    studentDto.id = student.id;
                    studentDto.name = student.name;
                    studentDto.email = student.email;
                    studentDto.created_at = student.created_at;
                    response.message = "Success";
                    response.data = studentDto;
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                response.message = "Failed";
                response.data = "Email is incorrect or not found";
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
        public IActionResult Add([FromForm] studentToAdd studentmodel)
        {
            try
            {
                students students = new students();
                students.name = studentmodel.name;
                students.email = studentmodel.email;
                students.isAdmin = false;
                students.created_at = DateTime.Now;
                studnetrepo.Add(students);
                studnetrepo.saveChange();
                response.message = "Success";
                var formated = new
                {
                    id=students.id,
                    name = students.name,
                    email = students.email,
                    isAdmin = students.isAdmin,
                    created_at = students.created_at,
                };
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
