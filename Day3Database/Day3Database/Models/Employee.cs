using System;
using System.Collections.Generic;
using System.Text;

namespace Day3Database.Models
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }
        public string FirstName{ get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

//        public Guid DepartmentID { get; set; }
        public Department Department { get; set; }

        public DateTime? HireDate { get; set; }
		//////public string



    }
}
