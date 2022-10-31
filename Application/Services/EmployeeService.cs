using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;

namespace Application.Services;

public class EmployeeService : IEmployeeService
{
    
    private IEmployeeRepository _repo;
    private IMapper _mapper;
    private IValidator<AddEmployeeDTO> _postValidator;
    private IValidator<EditEmployeeDTO> _putValidator;

    public EmployeeService(IEmployeeRepository repository, IMapper mapper, IValidator<AddEmployeeDTO> addValidator,
        IValidator<EditEmployeeDTO> editValidator)
    {
        _repo = repository;
        _mapper = mapper;
        _postValidator = addValidator;
        _putValidator = editValidator;
    }
    
    public List<Employee> GetAllEmployees()
    {
        return _repo.GetAllEmployees();
    }

    public Employee GetEmployeeById(int id)
    {
        return _repo.GetEmployeeById(id);
    }

    public Employee AddEmployee(AddEmployeeDTO dto)
    {
        var validation = _postValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Employee employee = _mapper.Map<Employee>(dto);
        employee.FlexBalance = 0;
        return _repo.AddEmployee(employee);
    }

    public Employee EditEmployee(EditEmployeeDTO dto)
    {
        var validation = _putValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Employee employee = _mapper.Map<Employee>(dto);
        return _repo.EditEmployee(employee);
    }

    public Employee DeleteEmployee(int id)
    {
        return _repo.DeleteEmployee(id);
    }
}