using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlInterface;

namespace SqlTestApp
{
    class Program
    {
        // Entry point into program
        static void Main(string[] args)
        {
            new Program().Run();
        }

        // Here we run all our program logic
        void Run()
        {
            string connectionString = @"server=localhost;database=DotNetDatabase;Trusted_Connection=yes";
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connectionString;

            sqlConnection.Open();
            if (sqlConnection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection failed");
                return;
            }
            else
            {
                Console.WriteLine("Connection successful");
            }

            // Do your SQL stuff here /////////////////////////////////////////////////////////////
            string commandString = "SELECT * FROM STUDENT";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            Student student = new Student();

            while (sqlDataReader.Read())
            {
                student.FirstName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName"));
                student.LastName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("LastName"));
                student.Id = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Id"));
                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("MajorID")))
                {
                    student.MajorId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("MajorId"));
                }
                else
                {
                    student.MajorId = 0;
                }
                //Console.WriteLine(int.Parse(sqlDataReader["MajorId"].ToString()));
            }
            // End SQL stuff, OK to close /////////////////////////////////////////////////////////

            sqlConnection.Close();
            Console.WriteLine("Connection closed");
        }
    }
}
