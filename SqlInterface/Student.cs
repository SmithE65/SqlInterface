using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlInterface
{
    /// <summary>
    /// Container for a row in the Student table in DotNetDatabase
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int MajorId { get; set; }
        public int SatScore { get; set; }
        public double GPA { get; set; }

        // Private fields
        private SqlDataReader mySqlDataReader;

        public void Read(SqlDataReader sqlDataReader)
        {
            mySqlDataReader = sqlDataReader;        // Capture the SqlDataReader

            Id = ReadInt("Id", sqlDataReader);
            FirstName = ReadString("FirstName", sqlDataReader);
            LastName = ReadString("LastName", sqlDataReader);
            Address = ReadString("Address", sqlDataReader);
            City = ReadString("City", sqlDataReader);
            StateCode = ReadString("StateCode", sqlDataReader);
            ZipCode = ReadString("ZipCode", sqlDataReader);
            PhoneNumber = ReadString("PhoneNumber", sqlDataReader);
            Email = ReadString("Email", sqlDataReader);
            SatScore = ReadInt("SAT", sqlDataReader);
            GPA = ReadDbl("GPA", sqlDataReader);
            BirthDay = ReadDate("Birthday", sqlDataReader);
            MajorId = ReadInt("MajorId", sqlDataReader);
        }

        private int ReadInt(string colName, SqlDataReader sqlDataReader)
        {
            if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(colName)))
            {
                return sqlDataReader.GetInt32(sqlDataReader.GetOrdinal(colName));
            }
            else return 0;
        }

        private DateTime ReadDate(string colName, SqlDataReader sqlDataReader)
        {
            if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(colName)))
            {
                return sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal(colName));
            }
            else return DateTime.MinValue;
        }

        private double ReadDbl(string colName, SqlDataReader sqlDataReader)
        {
            if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(colName)))
            {
                return sqlDataReader.GetDouble(sqlDataReader.GetOrdinal(colName));
            }
            else return 0;
        }

        private string ReadString(string colName, SqlDataReader sqlDataReader)
        {
            return sqlDataReader.GetString(sqlDataReader.GetOrdinal(colName));
        }
    }
}
