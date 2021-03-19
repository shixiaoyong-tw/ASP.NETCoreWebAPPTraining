using System.Collections.Generic;
using ASP.NETCoreWebAPPTraining.Dtos;
using ASP.NETCoreWebAPPTraining.Entities;

namespace ASP.NETCoreWebAPPTraining.Services
{
    public interface IStudentService
    {
        Student AddStudent(Student student);
        
        void EditStudent(Student student);
        
        void DeleteStudent(Student student);
        
        Student QueryStudentById(int id);
        
        IList<Student> QueryStudents(StudentQueryDto studentQueryDto);
    }
}