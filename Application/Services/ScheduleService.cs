using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;

namespace Application.Services;

public class ScheduleService : IScheduleService
{

    private IScheduleRepository _repository;
    private IMapper _mapper;
    private IValidator<AddScheduleDTO> _postValidator;
    private IValidator<EditScheduleDTO> _putValidator;
    
    public ScheduleService(IScheduleRepository repository, IMapper mapper, IValidator<AddScheduleDTO> addValidator,
        IValidator<EditScheduleDTO> editValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _postValidator = addValidator;
        _putValidator = editValidator;
    }
    
    public List<Schedule> GetScheduleForEmployee(int employeeId)
    {
        return _repository.GetScheduleForEmployee(employeeId);
    }

    public List<Schedule> GetScheduleForEmployeeInRange(int employeeId, DateTime start, DateTime end)
    {
        return _repository.GetScheduleForEmployeeInRange(employeeId, start, end);
    }

    public Schedule AddSchedule(AddScheduleDTO scheduleDTO)
    {
        var validation = _postValidator.Validate(scheduleDTO);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Schedule s = _mapper.Map<Schedule>(scheduleDTO);
        s.Logged = false;
        s.ShiftLength
        return _repository.AddSchedule(s);
    }

    public Schedule EditSchedule(EditScheduleDTO schedule)
    {
        throw new NotImplementedException();
    }

    public void DeleteSchedule(int id)
    {
        throw new NotImplementedException();
    }

    private double CalculateShiftLength(DateTime start, DateTime end)
    {
        double hours = start.Hour
    }
}