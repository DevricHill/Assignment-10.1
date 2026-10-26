using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_10._1
{
    public enum EmployeeDepartment { Business, Finance, IT}
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public EmployeeDepartment Department { get; set; }
    }
}
