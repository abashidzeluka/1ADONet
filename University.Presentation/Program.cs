using System.Text;
using University.Application.Services;
using University.Domain.Interfaces;
using University.Domain.Models;
using University.Infrastructure.Repositories;

namespace University.Presentation
{
    internal class Program
    {

        private static readonly string _connectionString = "Server=LUKA-PC;Database=UNIVERSITY;Trusted_Connection=True; TrustServerCertificate=True";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IStudentRepository studentRepository = new StudentRepository(_connectionString);
            var studentService = new StudentService(studentRepository);

            Console.WriteLine("All students:");
            studentService.GetAllStudents();

            Console.WriteLine("Student by id 1");
            studentService.GetStudentById(1);

            Console.WriteLine("Adding a new student:");
            studentService.AddStudent(new Student
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@gmail.com",
                Age = 22,
                GPA = 3.8m,
                PhoneNumber = "+995555123456",
                IsActive = true,
                DepartmentId = 1
            });

            Console.WriteLine("Updating student:");
            studentService.UpdateStudent(new Student
            {
                Id = 1,
                FirstName = "John",
                LastName = "Updated",
                Email = "john.updated@gmail.com",
                Age = 23,
                GPA = 3.9m,
                PhoneNumber = "+995555123456",
                IsActive = true,
                DepartmentId = 1
            });

            Console.WriteLine("Deleting student with id 1:");
            studentService.DeleteStudent(1);

            Console.WriteLine("All students after changes:");
            studentService.GetAllStudents();
        }
    }
}
