using EmployeeDataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeCRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        SqlConnection con;

       

        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            try
            {
               
                var responseData = EmployeeDA.EmployeeDA.GetAllEmployees();
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, responseData);
                return new OkObjectResult(responseData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("GetEmployee")]
        public IActionResult GetEmployee(int id)
        {
            try
            {
                var responseData = EmployeeDA.EmployeeDA.GetEmployee(id);
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, responseData);
                return new OkObjectResult(responseData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            try
            {
                var responseData = EmployeeDA.EmployeeDA.AddEmployee(employee);
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, responseData);
                return new OkObjectResult(responseData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromForm] Employee employee)
        {
            try
            {
                var responseData = EmployeeDA.EmployeeDA.UpdateEmployee(employee);
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, responseData);
                return new OkObjectResult(responseData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var responseData = EmployeeDA.EmployeeDA.DeleteEmployee(id);
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, responseData);
                return new OkObjectResult(responseData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
