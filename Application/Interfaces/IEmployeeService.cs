using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IEmployeeService
{
    List<Employee> GetAllEmployees();
    Employee GetEmployeeById(int id);
    Employee AddEmployee(AddEmployeeDTO dto);
    Employee EditEmployee(EditEmployeeDTO dto);
    Employee DeleteEmployee(int id);
}