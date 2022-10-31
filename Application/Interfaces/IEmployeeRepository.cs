using Domain.Models;

namespace Application.Interfaces;

public interface IEmployeeRepository
{
    List<Employee> GetAllEmployees();
    Employee GetEmployeeById(int id);
    Employee AddEmployee(Employee employee);
    Employee EditEmployee(Employee employee);
    Employee DeleteEmployee(int id);
}