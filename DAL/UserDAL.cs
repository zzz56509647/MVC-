using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UserDAL
    {
        string Connstr = "Data Source=.;Initial Catalog=Day227;Integrated Security=True";
        public PageInfo Show(string name, int currentpage, int pagesize)
        {
            if (currentpage < 1)
            {
                currentpage = 1;
            }
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Stu_Page";
                cmd.CommandType = CommandType.StoredProcedure;

                var Name = new SqlParameter("@name", SqlDbType.VarChar, 50);
                Name.Value = name;

                var Currentpage = new SqlParameter("@currentpage", SqlDbType.Int);
                Currentpage.Value = currentpage;

                var Pagesize = new SqlParameter("@pagesize", SqlDbType.Int);
                Pagesize.Value = pagesize;

                var Totalcount = new SqlParameter("@totalcount", SqlDbType.Int);
                Totalcount.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(Name);
                cmd.Parameters.Add(Currentpage);
                cmd.Parameters.Add(Pagesize);
                cmd.Parameters.Add(Totalcount);

                DataTable dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                var Stu = new List<Student>();
                foreach (DataRow i in dt.Rows)
                {
                    var s = new Student();
                    s.Sid = Convert.ToInt32(i["Sid"].ToString());
                    s.UserName = i["UserName"].ToString();
                    s.Sname = i["Sname"].ToString();
                    s.UserJs = i["UserJs"].ToString();
                    s.Img = i["Img"].ToString();
                    Stu.Add(s);
                }

                var p = new PageInfo
                {
                    currentpage = currentpage,
                    pagesize = pagesize,
                    totalcount = (int)Totalcount.Value,
                    Students = Stu

                };

                if (p.totalcount == 0)
                {
                    p.totalpage = 1;
                }
                else if (p.totalcount % p.pagesize == 0)
                {
                    p.totalpage = p.totalcount / p.pagesize;
                }
                else
                {
                    p.totalpage = p.totalcount / p.pagesize + 1;
                }

                if (p.currentpage > p.totalpage)
                {
                    Show(name, p.totalpage, p.pagesize);
                }

                return p;
            }
        }
    }
}
