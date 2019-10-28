using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual IEnumerable<Test> Tests { get; set; }
    }
}
