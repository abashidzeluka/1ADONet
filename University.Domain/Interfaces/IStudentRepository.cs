using System;
using System.Collections.Generic;
using System.Text;
using University.Domain.Models;

namespace University.Domain.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student GetById(int id);
        bool Add(Student student);
    }
}
