using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;

namespace CRUDExample.Controllers
{
 [Route("[controller]")]
 public class StudentController : Controller
 {
  private readonly IStudentsService _studentService;

  public StudentController(IStudentsService studentService)
  {
   _studentService = studentService;
  }

  [HttpPost]
  [Route("addstudent")]
  public async Task<IActionResult> AddStudent(StudentAddRequest student)
  {
        if (!ModelState.IsValid)
        {
                return BadRequest(ModelState);
        }
         await _studentService.AddStudent(student);
         return Ok();

        
    }


  [HttpGet]
  [Route("getstudent")]
  public async Task<IActionResult> GetStudents()
  {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _studentService.GetAllStudents();
            return Ok(result);
        }
 }
}
