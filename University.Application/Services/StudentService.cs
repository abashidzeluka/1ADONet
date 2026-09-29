using System;
using System.Collections.Generic;
using System.Text;
using University.Domain.Interfaces;
using University.Domain.Models;

namespace University.Application.Services
{
    public class StudentService
    {

        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void GetAllStudents()
        {
            var students = _studentRepository.GetAll();

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        public void GetStudentById(int id)
        {
            var student = _studentRepository.GetById(id);

            if (student != null)
            {
                Console.WriteLine(student);
            }
            else
            {
                Console.WriteLine("Student not found");
            }
        }

        public void AddStudent(Student student)
        {
            var success = _studentRepository.Add(student);
            if (success)
            {
                Console.WriteLine("Student added successfully");
            }
            else
            {
                Console.WriteLine("Failed to add student");
            }
        }

        public void UpdateStudent(Student student)
        {
            var success = _studentRepository.Update(student);
            if (success)
            {
                Console.WriteLine("Student updated successfully");
            }
            else
            {
                Console.WriteLine("Failed to update student");
            }
        }
        public void DeleteStudent(int id)
        {
            var success = _studentRepository.Delete(id);
            if (success)
            {
                Console.WriteLine("Student deleted successfully");
            }
            else
            {
                Console.WriteLine("Student not found");
            }
        }
    }
}
