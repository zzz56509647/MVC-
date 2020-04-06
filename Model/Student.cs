using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Model
{
    public class Student
    {
        public int Sid { get; set; }
        public string UserName { get; set; }
        public string Sname { get; set; }
        public string UserJs { get; set; }
        public string Img { get; set; }
    }
    public class PageInfo
    {
        public List<Student> Students { get; set; }
        public int currentpage { get; set; }
        public int pagesize { get; set; }
        public int totalcount { get; set; }
        public int totalpage { get; set; }
    }
}
