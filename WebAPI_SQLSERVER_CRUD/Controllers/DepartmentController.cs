using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebAPI_SQLSERVER_CRUD.Models;

namespace WebAPI_SQLSERVER_CRUD.Controllers
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
        public JsonResult Get()
        {
            string query = @"SELECT * FROM Department";
            DataTable dt = new DataTable();
            string connection = _configuration.GetConnectionString("Conn");
            SqlConnection conn = new SqlConnection(connection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(Department department)
        {
            string query = $"INSERT INTO Department VALUES ('{department.DepartmentName}')";
            DataTable dt = new DataTable();
            string connection = _configuration.GetConnectionString("Conn");
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return new JsonResult("Added Successfully...");
        }

        [HttpPut]
        public JsonResult Put(Department department)
        {
            string query = $"UPDATE Department SET DepartmentName =  '{department.DepartmentName}' WHERE DepartmentId = '{department.DepartmentId}'";
            DataTable dt = new DataTable();
            string connection = _configuration.GetConnectionString("Conn");
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return new JsonResult("Updated Successfully...");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = $"DELETE FROM Department WHERE DepartmentId =  '{id}'";
            DataTable dt = new DataTable();
            string connection = _configuration.GetConnectionString("Conn");
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return new JsonResult("Deleted Successfully...");
        }
    }
}
