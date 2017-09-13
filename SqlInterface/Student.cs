using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlInterface
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public int MajorId { get; set; }
        public int SatScore { get; set; }
        public double GPA { get; set; }


        public bool Read(SqlDataReader sqlDataReader)
        {
            return true;
        }
    }
}
