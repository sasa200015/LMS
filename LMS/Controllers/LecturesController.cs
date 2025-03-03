using Azure;
using LMS.DTO;
using LMS.Interface;
using LMS.Model;
using LMS.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly Ilectures lectureRepo;
        private readonly ProjectContext context;
        private readonly GeneralRes response;

        public LecturesController(Ilectures lectureRepo, ProjectContext context, GeneralRes response)
        {
            this.lectureRepo = lectureRepo;
            this.context = context;
            this.response = response;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<lectureDto> lecs = new List<lectureDto>();
                List<lectures> lectures = lectureRepo.GetAll();
                foreach (var lecture in lectures)
                {
                    lectureDto lectureDto = new lectureDto();
                    lectureDto.id = lecture.id;
                    lectureDto.lecture_title = lecture.lecture_title;
                    lectureDto.lecture_description = lecture.lecture_description;
                    lectureDto.course_id = lecture.course_id;
                    lecs.Add(lectureDto);
                }
                response.message = "Success";
                response.data = lecs;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response.message = "Failed";
                response.data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpGet("{Id:int}")]
        public IActionResult getById(int Id)
        {
            try
            {
                lectures lecture = lectureRepo.GetById(Id);
                if (lecture != null)
                {
                    lectureDto lec = new lectureDto();
                    lec.id = lecture.id;
                    lec.lecture_title = lecture.lecture_title;
                    lec.lecture_description = lecture.lecture_description;
                    lec.course_id = lecture.course_id;
                    response.message = "Success";
                    response.data = lec;
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                response.message = "Failed";
                response.data = "Lecture not found";
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
        public IActionResult Add([FromForm] lectureAdd lectureModel)
        {
            try
            {
                lectures lecture = new lectures();
                var check = context.Courses.SingleOrDefault(c => c.id == lectureModel.course_id);
                if (check == null)
                {
                    response.message = "failed";
                    response.data = "The course id does not exist";
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }
                lecture.lecture_title = lectureModel.lecture_title;
                lecture.lecture_description = lectureModel.lecture_description;
                lecture.course_id = lectureModel.course_id;
                lectureRepo.Add(lecture);
                lectureRepo.saveChange();
                var format = new
                {
                    id = lecture.id,
                    lecture_title = lectureModel.lecture_title,
                    lecture_description = lectureModel.lecture_description,
                    course_id = lectureModel.course_id,

                };
                response.message = "Success";
                response.data = format;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response.message = "Failed";
                response.data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromForm] lectureAdd lectureModel)
        {
            try
            {
                var lecture = lectureRepo.GetById(id);
                if (lecture != null)
                {
                    if (lectureModel.course_id == 0)
                    {
                        lecture.lecture_title = lectureModel.lecture_title ?? lecture.lecture_title;
                        lecture.lecture_description = lectureModel.lecture_description ?? lecture.lecture_description;
                        lectureRepo.Update(lecture);
                        lectureRepo.saveChange();
                        var format2 = new
                        {
                            id = lecture.id,
                            lecture_title = lecture.lecture_title,
                            lecture_description = lecture.lecture_description,
                            course_id = lecture.course_id,
                        };
                        response.message = "Success";
                        response.data = format2;
                        return StatusCode(StatusCodes.Status200OK, response);
                    }
                    lecture.lecture_title = lectureModel.lecture_title ?? lecture.lecture_title;
                    lecture.lecture_description = lectureModel.lecture_description ?? lecture.lecture_description;
                    lecture.course_id = lectureModel.course_id;
                    if (context.Courses.SingleOrDefault(x => x.id == lecture.course_id) == null)
                    {
                        response.message = "failed";
                        response.data = "The course id does not exist";
                        return StatusCode(StatusCodes.Status404NotFound, response);
                    }
                    lectureRepo.Update(lecture);
                    lectureRepo.saveChange();
                    var format = new
                    {
                        id = lecture.id,
                        lecture_title = lecture.lecture_title,
                        lecture_description = lecture.lecture_description,
                        course_id = lecture.course_id,
                    };
                    response.message = "Success";
                    response.data = format;
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
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var lecture = lectureRepo.GetById(id);
                if (lecture != null)
                {
                    lectureRepo.Delete(lecture);
                    lectureRepo.saveChange();
                    response.message = "Success";
                    response.data = "Deleted Successfully";
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
    }
}
