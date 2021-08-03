using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPICore.Model;

namespace WebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public IActionResult GetEmployee()
        {
            string query = @"select EmployeeId,EmployeeName,Department,convert(varchar(10),DateOfJoining,120) 
                                 as DateOfJoining, PhotoFileName from dbo.Employee";
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
            return new JsonResult(table);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            string query = @"Insert  into dbo.Employee 
                         values(
                              '" + employee.EmployeeName + @"'
                              ,'" + employee.Department + @"'
                              ,'" + employee.DateOfJoining + @"'
                              ,'" + employee.PhotoFileName + @"'
                          )
                               ";
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
            return new JsonResult("Employee Added");
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee emp)
        {
            string query = @"update dbo.Employee set
                             EmployeeName = '" + emp.EmployeeName + @"'
                             , Department = '" + emp.Department + @"'
                             , DateOfJoining = '" + emp.DateOfJoining + @"'

                          where EmployeeId =" + emp.EmployeeId + @"";
            
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
            return new JsonResult("Employee updated");
        }

        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            string query = @"delete from dbo.Employee where EmployeeId =" + employeeId + @"";
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
            return new JsonResult("Employee deleted");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photo/" + fileName;


                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch(Exception)
            {
                return new JsonResult("annonymous.png");          
            }
        }
    }
}
