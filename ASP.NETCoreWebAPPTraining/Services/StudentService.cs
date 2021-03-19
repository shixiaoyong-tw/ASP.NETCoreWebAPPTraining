using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NETCoreWebAPPTraining.Dtos;
using ASP.NETCoreWebAPPTraining.Entities;
using Microsoft.Extensions.Configuration;

namespace ASP.NETCoreWebAPPTraining.Services
{
    public class StudentService : IStudentService
    {
        private readonly IConfiguration _configuration;

        private static readonly List<Student> Students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "alex",
                StudentNo = "1314011524",
                Age = 15,
                PhoneNumber = "13259769759",
                Email = "alex@thoughtworks.com",
                IsDeleted = false
            },
            new Student
            {
                Id = 2,
                Name = "chris",
                StudentNo = "1314011523",
                Age = 10,
                PhoneNumber = "13098766555",
                Email = "chris@thoughtworks.com",
                IsDeleted = false
            }
        };

        public StudentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Student AddStudent(Student student)
        {
            var email = $"{student.Name}@{_configuration.GetValue<string>("EmailDomain")}";
            student.Email = email;
            student.Id = Students.OrderByDescending(s => s.Id).FirstOrDefault()?.Id ?? 0 + 1;
            student.IsDeleted = false;

            Students.Add(student);

            return student;
        }

        public void EditStudent(Student student)
        {
            var originStudent = Students.First(s => s.Id == student.Id);
            originStudent.Name = student.Name;
            originStudent.Age = student.Age;
            originStudent.StudentNo = student.StudentNo;
            originStudent.PhoneNumber = student.PhoneNumber;
            originStudent.Email = $"{student.Name}@{_configuration.GetValue<string>("EmailDomain")}";
        }

        public void DeleteStudent(Student student)
        {
            var originStudent = Students.First(s => s.Id == student.Id);
            originStudent.IsDeleted = true;
        }

        public Student QueryStudentById(int id)
        {
            return Students.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
        }

        public IList<Student> QueryStudents(StudentQueryDto studentQueryDto)
        {
            Func<Student, bool> func = s => !s.IsDeleted;

            if (!string.IsNullOrEmpty(studentQueryDto.Name))
            {
                func += s => s.Name.Equals(studentQueryDto.Name, StringComparison.InvariantCultureIgnoreCase);
            }

            if (!string.IsNullOrEmpty(studentQueryDto.StudentNo))
            {
                func += s => s.StudentNo.Equals(studentQueryDto.StudentNo, StringComparison.InvariantCultureIgnoreCase);
            }

            var studentsAfterFilter = Students.Where(func).ToList();

            return studentsAfterFilter;
        }
    }
}