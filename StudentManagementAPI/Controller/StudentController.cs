using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Interface;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controller
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        #region Dependency Injection
        private readonly IStudentInterface _studentInterface;
        public StudentController(IStudentInterface studentInterface)
        {
            _studentInterface = studentInterface;   
        }
        #endregion

        #region Create Student API
        [Route("Api/CreateStudent"), HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentRequest request)
        {
            var res = await _studentInterface.CreateStudent(request);
            return Ok(res);
        }
        #endregion

        #region Delete Student API
        [Authorize]
        [Route("Api/DeleteStudent"), HttpPost]
        public async Task<IActionResult> DeleteStudent(int? StudentID)
        {
            var res = await _studentInterface.DeleteStudent(StudentID);
            return Ok(res);
        }
        #endregion

        #region Get Student By ID API
        [Authorize]
        [Route("Api/GetStudentByID"), HttpPost]
        public async Task<IActionResult> GetStudentByID(int? StudentID)
        {
            var res = await _studentInterface.GetStudentByID(StudentID);
            return Ok(res);
        }
        #endregion

        #region Get All Student API
        [Authorize]
        [Route("Api/GetAllStudent"), HttpPost]
        public async Task<IActionResult> GetAllStudent()
        {
            var res = await _studentInterface.GetAllStudent();
            return Ok(res);
        }
        #endregion

        #region Update Student API
        [Authorize]
        [Route("Api/UpdateStudent"), HttpPost]
        public async Task<IActionResult> UpdateStudent(OneStudentData request)
        {
            var res = await _studentInterface.UpdateStudent(request);
            return Ok(res);
        }
        #endregion

    }
}
