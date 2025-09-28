using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.DTO;
using WebApiDemo.Models;
using WebApiDemo.Services.SEmployee;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost(Name = "Post")]
        public ActionResult PostEmployee([FromForm] Employee body)
        {
            APIresponse response = new APIresponse();

            try
            {
                response.Data = _employeeService.Insert(body.position, body.fullName, body.managerId);
            }
            catch (Exception ex)
            {
                // ex.Message se envia al log
                response.error.code = 1;
                response.error.message = "Error en la ejecución revisar los Logs";
            }

            return Ok(response);
        }


        [HttpGet("{employeeId}", Name = "GetEmployeeById")]
        public ActionResult GetEmployeeById(int employeeId)
        {
            APIresponse response = new APIresponse();

            try
            {
                response.Data = _employeeService.GetById(employeeId);
            }
            catch (Exception ex)
            {
                // ex.Message se envia al log
                response.error.code = 1;
                response.error.message = "Error en la ejecución revisar los Logs";
            }

            return Ok(response);
        }



        [HttpPut(Name = "UpdateEmployee")]
        public ActionResult UpdateEmployee([FromForm] UpdateEmployeeDto dto)
        {
            APIresponse response = new APIresponse();

            try
            {
                var updatedEmployee = _employeeService.UpdateEmployee(dto);
                response.Data = updatedEmployee;
            }
            catch (Exception ex)
            {
                response.error.code = 1;
                response.error.message = "Error en la ejecución revisar los Logs";
            }

            return Ok(response);
        }


        [HttpGet(Name = "GetEmployeeHierarchy")]
        public ActionResult GetEmployeeHierarchy([FromQuery] int? rootEmployeeId = null)
        {
            APIresponse response = new APIresponse();

            try
            {
                List<EmployeeNode> employees = _employeeService.EmployeeHierarchy(rootEmployeeId);

                response.Data = _employeeService.BuildHierarchy(employees);

            }
            catch (Exception ex)
            {
                response.error.code = 1;
                response.error.message = "Error en la ejecución revisar los Logs";
            }

            return Ok(response);
        }
    }
}
