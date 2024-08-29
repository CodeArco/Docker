using Api.Data;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _EmployeeRepository;

        public EmployeesController(EmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return new ActionResult<IEnumerable<Employee>>(await _EmployeeRepository.GetEmployees());
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            return new ActionResult<Employee>(await _EmployeeRepository.GetEmployee(id));
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee Employee)
        {
            return new ActionResult<Employee>(await _EmployeeRepository.InsertEmployee(Employee));
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee Employee)
        {
            await _EmployeeRepository.UpdateEmployee(id, Employee);
            return new OkResult();
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _EmployeeRepository.DeleteEmployee(id);
            return new OkResult();
        }

    }
}
