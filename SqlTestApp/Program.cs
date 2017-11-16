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
            StudentCollection studentCollection = StudentCollection.Select();

            foreach (Student s in studentCollection)
            {
                Console.WriteLine($"{s.LastName}, {s.FirstName} has student id of {s.Id}");
            }

            Student bob = StudentCollection.Select(6);
            Console.WriteLine($"Student number 6 is {bob.FirstName} {bob.LastName}");
        }
    }
}
