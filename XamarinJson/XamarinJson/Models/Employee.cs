using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinJson.Models
{
    public class Employee
    {

        public Employee()
        {

        }

        public Employee(long empNo, string eName, long sal, string job)
        {
            this.Empno = empNo;
            this.Ename = eName;
            this.Sal = sal;
            this.Job = job;
        }

        public long Empno { get; set; }

        public string Ename{ get; set; }

        public long Sal { get; set; }

        public string Job { get; set; }
    }
}
