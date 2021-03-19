using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NETCoreWebAPPTraining.Common;
using ASP.NETCoreWebAPPTraining.Dtos;
using ASP.NETCoreWebAPPTraining.Entities;
using ASP.NETCoreWebAPPTraining.Filters;
using ASP.NETCoreWebAPPTraining.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NETCoreWebAPPTraining.Controllers
{
    [Route("api/v1/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger _logger;

        public StudentController(
            IStudentService studentService,
            ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetStudentList([FromQuery] StudentQueryDto queryDto)
        {
            var students = _studentService.QueryStudents(queryDto).ToList();
            return Ok(GeneralResponse<List<Student>>.Ok(students));
        }

        [HttpGet("{studentId}")]
        public ActionResult<Student> GetStudent([FromRoute] int studentId)
        {
            var student = _studentService.QueryStudentById(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(GeneralResponse<Student>.Ok(student));
        }

        [HttpPost]
        [MyAuthorizationFilter]
        public ActionResult<Student> CreateStudent([FromBody] StudentAddDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = new List<KeyValuePair<string, string>>();
                foreach (var key in ModelState.Keys)
                {
                    errorMessages.AddRange(ModelState[key].Errors
                        .Select(error => new KeyValuePair<string, string>(key, error.ErrorMessage)));
                }

                return BadRequest(GeneralResponse<Student>.ValidationError(errorMessages));
            }

            var studentEntity = new Student
            {
                Name = studentDto.Name,
                StudentNo = studentDto.StudentNo,
                Age = studentDto.Age,
                PhoneNumber = studentDto.PhoneNumber
            };

            var studentAfterAdd = _studentService.AddStudent(studentEntity);

            return Created(new Uri($"{Request.Path}/{studentAfterAdd.Id}", UriKind.Relative),
                GeneralResponse<Student>.Ok(studentAfterAdd));
        }

        [HttpPut]
        [MyAuthorizationFilter]
        public ActionResult UpdateStudent([FromBody] StudentEditDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = new List<KeyValuePair<string, string>>();
                foreach (var key in ModelState.Keys)
                {
                    errorMessages.AddRange(ModelState[key].Errors
                        .Select(error => new KeyValuePair<string, string>(key, error.ErrorMessage)));
                }

                return BadRequest(GeneralResponse<Student>.ValidationError(errorMessages));
            }

            var studentEntity = _studentService.QueryStudentById(studentDto.Id);
            if (studentEntity == null) return NotFound();

            studentEntity.Name = studentDto.Name;
            studentEntity.Age = studentDto.Age;
            studentEntity.StudentNo = studentDto.StudentNo;
            studentEntity.PhoneNumber = studentDto.PhoneNumber;

            _studentService.EditStudent(studentEntity);
            return NoContent();
        }

        [HttpDelete("{studentId}")]
        [MyAuthorizationFilter]
        public ActionResult DeleteStudent([FromRoute] int studentId)
        {
            var student = _studentService.QueryStudentById(studentId);
            if (student == null) return NotFound();
            _studentService.DeleteStudent(student);
            _logger.LogInformation($"{student.Name} is deleted success");
            return NoContent();
        }
    }
}