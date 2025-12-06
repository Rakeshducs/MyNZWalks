using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyNZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] {
                "Alice Johnson",
                "Bob Smith",
                "Charlie Brown",
                "Diana Prince"
            };

            return Ok(studentNames);
        }
    }
}
