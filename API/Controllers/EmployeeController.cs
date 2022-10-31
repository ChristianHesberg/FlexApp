using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public ActionResult<List<Employee>> GetAllEmployees()
    {
        return Ok(_employeeService.GetAllEmployees());
    }

    [HttpGet]
    [Route("{userId}")]
    public ActionResult<Employee> GetEmployeeById(int id)
    {
        try
        {
            return Ok(_employeeService.GetEmployeeById(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Could not find employee with given ID");
        }
    }

    [HttpPost]
    public ActionResult<Employee> PostEmployee(AddEmployeeDTO dto)
    {
        try
        {
            Employee employee = _employeeService.AddEmployee(dto);
            return Created("employee/" + employee.Id, employee);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public ActionResult<Employee> PutEmployee(EditEmployeeDTO dto)
    {
        try
        {
            Employee employee = _employeeService.EditEmployee(dto);
            return Ok(employee);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public ActionResult<Employee> DeleteEmployee(int id)
    {
        try
        {
            Employee employee = _employeeService.DeleteEmployee(id);
            return Ok(employee);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

}