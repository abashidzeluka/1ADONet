using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using University.Domain.Interfaces;
using University.Domain.Models;


namespace University.Infrastructure.Repositories

{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Student> GetAll()
        {
            var students = new List<Student>();

            var query = "SELECT * FROM Students";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                if(reader.HasRows)
                {
                    students.Add(MapStudent(reader));
                }
            }
            return students;
        }

        public Student GetById(int id)
        {
            var student = new Student();
            var query = "SELECT * FROM Students where Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapStudent(reader);
            }

            return null;
        }

        public bool Add(Student student)
        {
            var query = @"INSERT INTO Students (FirstName, LastName, Email, Age, GPA, PhoneNumber, IsActive, RegisteredAt, DepartmentId)
                        VALUES (@FirstName, @LastName, @Email, @Age, @GPA, @PhoneNumber, @IsActive, @RegisteredAt, @DepartmentId)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Email", student.Email);
            command.Parameters.AddWithValue("@Age", student.Age);
            command.Parameters.AddWithValue("@GPA", (object?)student.GPA ?? DBNull.Value);
            command.Parameters.AddWithValue("@PhoneNumber", (object?)student.PhoneNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", (object?)student.IsActive ?? DBNull.Value);
            command.Parameters.AddWithValue("@RegisteredAt", (object?)student.RegisteredAt ?? DBNull.Value);
            command.Parameters.AddWithValue("@DepartmentId", (object?)student.DepartmentId ?? DBNull.Value);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool Update(Student student)
        {
            var query = @"UPDATE Students 
                  SET FirstName = @FirstName,
                      LastName = @LastName,
                      Email = @Email,
                      Age = @Age,
                      GPA = @GPA,
                      PhoneNumber = @PhoneNumber,
                      IsActive = @IsActive,
                      DepartmentId = @DepartmentId
                  WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Email", student.Email);
            command.Parameters.AddWithValue("@Age", student.Age);
            command.Parameters.AddWithValue("@GPA", (object?)student.GPA ?? DBNull.Value);
            command.Parameters.AddWithValue("@PhoneNumber", (object?)student.PhoneNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", (object?)student.IsActive ?? DBNull.Value);
            command.Parameters.AddWithValue("@DepartmentId", (object?)student.DepartmentId ?? DBNull.Value);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
        public bool Delete(int id)
        {
            var query = "DELETE FROM Students WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        private Student MapStudent(SqlDataReader reader)
        {
            return new Student
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                Age = reader.GetInt32(4),
                GPA = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                PhoneNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                IsActive = reader.IsDBNull(7) ? null : reader.GetBoolean(7),
                RegisteredAt = reader.IsDBNull(8) ? null : reader.GetDateTime(8),
                DepartmentId = reader.IsDBNull(9) ? null : reader.GetInt32(9)
            };
        }

        
    }
}
