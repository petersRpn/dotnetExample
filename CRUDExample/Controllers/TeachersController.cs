using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;

namespace CRUDExample.Controllers
{
    [Route("[controller]")]
    public class TeacherController : Controller
    {
        private readonly ITeachersService _teacherService;

        public TeacherController(ITeachersService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        [Route("addteacher")]
        public async Task<IActionResult> AddTeacher(TeacherAddRequest teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _teacherService.AddTeacher(teacher);
            return Ok();


        }


        [HttpGet]
        [Route("getteacher")]
        public async Task<IActionResult> GetTeacher()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _teacherService.GetAllTeachers();
            return Ok(result);
        }
    }
}
