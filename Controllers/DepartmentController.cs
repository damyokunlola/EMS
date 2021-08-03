using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPICore.Model;

namespace WebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult  GetDepartment()
        {
            string query = @"select * from dbo.Department";
            DataTable table = new DataTable();
            string sql = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection con = new SqlConnection(sql))
            {
                con.Open();
                using(SqlCommand sqlCommand = new SqlCommand(query, con))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public IActionResult AddDepartmet(Department department)
        {
            string query = @"Insert  into dbo.Department values('"+department.DepartmentName+@"')";
            DataTable table = new DataTable();
            string sql = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sql))
            {
                con.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, con))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Department Added");
        }

        [HttpPut]
        public IActionResult UpdateDepartment(int departmentId,string departmentName)
        {
            string query = @"update dbo.Department set DepartmentName = '"+ departmentName + @"' where DepartmentId ="+departmentId+@"";
            DataTable table = new DataTable();
            string sql = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sql))
            {
                con.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, con))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Department updated");
        }

        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(int departmentId)
        {
            string query = @"delete from dbo.Department where DepartmentId =" + departmentId + @"";
            DataTable table = new DataTable();
            string sql = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sql))
            {
                con.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, con))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Department deleted");
        }
    }
}
