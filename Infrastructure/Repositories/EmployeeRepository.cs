using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public List<Employee> GetAllEmployees()
    {
        return _dbContext.Employees.ToList();
    }

    public Employee GetEmployeeById(int id)
    {
        Employee employee = _dbContext.Employees.Find(id);
        if (employee != null)
            return employee;
        throw new KeyNotFoundException();
    }

    public Employee AddEmployee(Employee employee)
    {
        Employee added = _dbContext.Employees.Add(employee).Entity;
        _dbContext.SaveChanges();
        return new Employee()
        {
            Id = added.Id,
            Name = added.Name
        };
    }

    public Employee EditEmployee(Employee employee)
    {
        Employee edit = _dbContext.Employees.Find(employee.Id);
        if (edit != null)
        {
            edit.Name = employee.Name;
            _dbContext.SaveChanges();
            return employee;
        }
        throw new KeyNotFoundException();
    }

    public Employee DeleteEmployee(int id)
    {
        Employee employee = _dbContext.Employees.Find(id);
        if(employee!=null)
        {
            _dbContext.Remove(employee);
            _dbContext.SaveChanges();
            return employee;
        }
        throw new KeyNotFoundException();
    }
}