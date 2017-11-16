using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlInterface
{
    /// <summary>
    /// A list for handling Student class objects
    /// </summary>
    public class StudentCollection : List<Student>
    {
        /// <summary>
        /// Selects all students from the SQL database
        /// </summary>
        /// <returns>List of all students</returns>
        public static StudentCollection Select()
        {
            StudentCollection students = new StudentCollection();

            string connectionString = @"server=localhost;database=DotNetDatabase;Trusted_Connection=yes";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                if (!sqlConnection.State.Equals(ConnectionState.Open))
                {
                    Console.WriteLine("Something has gone wrong.");
                    return null;
                }

                string commandString = "SELECT * FROM Student";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                Student student = null;
                while (sqlDataReader.Read())
                {
                    student = new Student();
                    student.Read(sqlDataReader);

                    students.Add(student);
                }
            }
            return students;
        }

        /// <summary>
        /// Selects a single student from the SQL database
        /// </summary>
        /// <param name="id">ID number for the student to select</param>
        /// <returns>Student object with Id id</returns>
        public static Student Select(int id)
        {
            StudentCollection students = new StudentCollection();

            string connectionString = @"server=localhost;database=DotNetDatabase;Trusted_Connection=yes";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string commandString = $"SELECT * FROM Student WHERE Id = {id}";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                Student student = null;
                while (sqlDataReader.Read())
                {
                    student = new Student();
                    student.Read(sqlDataReader);

                    students.Add(student);
                }
            }

            if (students.Count == 0)
            {
                return null;
            }
            else
            {
                return students[0]; // In theory, Student.Id is unique and there can only be one student
            }
        }

        /// <summary>
        /// Deletes rows in Student table with matching Id
        /// </summary>
        /// <param name="id">Id of student to delete</param>
        /// <returns>True for single record changed; otherwise false</returns>
        public bool Delete(int id)
        {
            int result = 0;
            string connectionString = @"server=localhost;database=DotNetDatabase;Trusted_Connection=yes";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                if (!sqlConnection.State.Equals(ConnectionState.Open))
                {
                    Console.WriteLine("Something has gone wrong.");
                    return false;
                }

                string commandString = $"DELETE from Student WHERE Id = {id}";

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                result = sqlCommand.ExecuteNonQuery();
            }

            return (result == 1);
        }

        /// <summary>
        /// Inserts a single Student row into the database
        /// </summary>
        /// <param name="student">Student to add</param>
        /// <returns>Whether the INSERT succeeded or failed</returns>
        public bool Insert(Student student)
        {
            int result = 0;
            string connectionString = @"server=localhost;database=DotNetDatabase;Trusted_Connection=yes";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                if (!sqlConnection.State.Equals(ConnectionState.Open))
                {
                    Console.WriteLine("Something has gone wrong.");
                    return false;
                }

                string commandString = $"exec InsertStudent " +
                    $"'{student.FirstName}', " +
                    $"'{student.LastName}', " +
                    $"'{student.Address}', " +
                    $"'{student.City}', " +
                    $"'{student.StateCode}', " +
                    $"'{student.ZipCode}', " +
                    $"'{student.PhoneNumber}', " +
                    $"'{student.Email}', " +
                    $"'{student.BirthDay.ToShortDateString()}', " +
                    $"{student.MajorId}, " +
                    $"{student.SatScore}, " +
                    $"{student.GPA}";

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                result = sqlCommand.ExecuteNonQuery();
            }

            return (result == 1);
        }

        /// <summary>
        /// Replaces all data in rows with matching Student.Id
        /// </summary>
        /// <param name="student">Contains information to be replaced</param>
        /// <returns>True for single record changed; otherwise false</returns>
        public bool Update(Student student)
        {
            int result = 0;
            string connectionString = @"server=localhost;database=DotNetDatabase;Trusted_Connection=yes";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                if (!sqlConnection.State.Equals(ConnectionState.Open))
                {
                    Console.WriteLine("Something has gone wrong.");
                    return false;
                }

                string commandString = $"UPDATE Student Set " +
                    $"FirstName = '{student.FirstName}', " +
                    $"LastName = '{student.LastName}', " +
                    $"Address = '{student.Address}', " +
                    $"City = '{student.City}', " +
                    $"StateCode = '{student.StateCode}', " +
                    $"ZipCode = '{student.ZipCode}', " +
                    $"PhoneNumber = '{student.PhoneNumber}', " +
                    $"Email = '{student.Email}', " +
                    $"Birthday = '{student.BirthDay.ToShortDateString()}', " +
                    $"MajorId = {student.MajorId}, " +
                    $"SAT = {student.SatScore}, " +
                    $"GPA = {student.GPA} " +
                    $"WHERE Id = {student.Id}";

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                result = sqlCommand.ExecuteNonQuery();
            }

            return (result == 1);
        }
    }
}
