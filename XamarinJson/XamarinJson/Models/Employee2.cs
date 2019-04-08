using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinJson.Models
{
    public class Employee2
    {

        public Employee2()
        {

        }

        public Employee2(long empNo, string eName, long sal, string job, string job2, string job3, string job4)
        {
            this.Empno = empNo;
            this.Ename = eName;
            this.Sal = sal;
            this.Job = job;
            this.Job2 = job2;
            this.Job3 = job3;
            this.Job4 = job4;

        }

        public long Empno { get; set; }

        public string Ename{ get; set; }

        public long Sal { get; set; }
        public string Job { get; set; }
        public string Job2 { get; set; }
        public string Job3 { get; set; }
        public string Job4 { get; set; }

    }
}
