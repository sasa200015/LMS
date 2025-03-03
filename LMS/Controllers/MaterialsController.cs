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
    public class MaterialsController : ControllerBase
    {
        private readonly Imaterial materialRepo;
        private readonly ProjectContext context;
        private readonly Ilectures lectureRepo;
        private readonly GeneralRes response;

        public MaterialsController(Imaterial materialRepo, ProjectContext context, Ilectures lectureRepo, GeneralRes response)
        {
            this.materialRepo = materialRepo;
            this.context = context;
            this.lectureRepo = lectureRepo;
            this.response = response;
        }
        [HttpGet("ByLectureId/{id:int}")]
        public IActionResult GetByLectureId(int id)
        {
            try
            {
                var material = materialRepo.GetBylectureId(id);
                List<materialDto> materiallist = new List<materialDto>();
                if (material.Any())
                {
                    foreach (var item in material)
                    {
                        materialDto materialDto = new materialDto();
                        materialDto.id = item.id;
                        materialDto.material_title = item.material_title;
                        materialDto.material_video = item.material_video;
                        materialDto.material_text = item.material_text;
                        materialDto.material_pdf = item.material_pdf;
                        materialDto.lecture_id = item.lecture_id;
                        materiallist.Add(materialDto);
                    }
                    response.message = "Success";
                    response.data = materiallist;
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
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var material = materialRepo.getById(id);
                if (material != null)
                {
                    materialDto materialDto = new materialDto();
                    materialDto.id = id;
                    materialDto.material_title = material.material_title;
                    materialDto.material_video = material.material_video;
                    materialDto.material_text = material.material_text;
                    materialDto.material_pdf = material.material_pdf;
                    materialDto.lecture_id = material.lecture_id;
                    response.message = "Success";
                    response.data = materialDto;
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
        public IActionResult Add([FromForm] materialAdd materialModel)
        {
            try
            {
                lecture_materials material = new lecture_materials();
                var check = lectureRepo.GetById(materialModel.lecture_id);
                if (check == null)
                {
                    response.message = "failed";
                    response.data = "The lecture id does not exist";
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }
                material.material_title = materialModel.material_title;
                material.material_video = materialModel.material_video;
                material.material_text = materialModel.material_text;
                material.material_pdf = materialModel.material_pdf;
                material.lecture_id = materialModel.lecture_id;
                context.LectureMaterials.Add(material);
                materialRepo.saveChange();
                var format = new
                {
                    id = material.id,
                    material_title = materialModel.material_title,
                    material_video = materialModel.material_video,
                    material_text = materialModel.material_text,
                    material_pdf = materialModel.material_pdf,
                    lecture_id = materialModel.lecture_id,
                };
                response.message = "Success";
                response.data = format;
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                response.message = "Failed";
                response.data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteLecture(int id)
        {
            try
            {
                var material = materialRepo.getById(id);
                if (material != null)
                {
                    materialRepo.Delete(material);
                    materialRepo.saveChange();
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
        [HttpPut("{id:int}")]
        public IActionResult update(int id, [FromForm]materialUpdate materialmodel)
        {
            try
            {
                var materials = context.LectureMaterials.SingleOrDefault(x => x.id == id);
                if (materials != null)
                {
                    materials.material_title = materialmodel.material_title ?? materials.material_title;
                    materials.material_text = materialmodel.material_text ?? materials.material_text;
                    materials.material_video = materialmodel.material_video ?? materials.material_video;
                    materials.material_pdf = materialmodel.material_pdf ?? materials.material_pdf;
                    context.LectureMaterials.Update(materials);
                    materialRepo.saveChange();
                    var format = new
                    {
                        id = materials.id,
                        material_title = materials.material_title,
                        material_text = materials.material_text,
                        material_video = materials.material_video,
                        material_pdf= materials.material_pdf,
                        lecture_id= materials.lecture_id,
                    };
                    response.message = "Success";
                    response.data = format;
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                response.message = "Failed";
                response.data = "This material Not found";
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
