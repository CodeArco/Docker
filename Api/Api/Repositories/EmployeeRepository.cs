using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class EmployeeRepository
    {
        private MyContext _context;
        public EmployeeRepository(MyContext context) { 
            _context = context;
        }
        
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> InsertEmployee(Employee Employee)
        {
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return await GetEmployee(Employee.Id);
        }

        public async Task UpdateEmployee(int id, Employee Employee)
        {
            _context.Entry(Employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return;
        }

        public async Task DeleteEmployee(int id)
        {
            var Employee = await _context.Employees.FindAsync(id);

            _context.Employees.Remove(Employee);
            await _context.SaveChangesAsync();

            return;
        }
    }
}
