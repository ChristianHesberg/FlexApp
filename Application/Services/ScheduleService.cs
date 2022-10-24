using Application.DTOs;
using Application.Interfaces;
using Application.Util;
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

    public Schedule GetScheduleForEmployeeAtDate(int employeeId, DateTime date)
    {
        return _repository.GetScheduleForEmployeeAtDate(employeeId, date);
    }

    public List<Schedule> GetScheduleForEmployeeInRange(int employeeId, DateTime start, DateTime end)
    {
        return _repository.GetScheduleForEmployeeInRange(employeeId, start, end);
    }

    public void LogSchedule(int id)
    {
        _repository.LogSchedule(id);
    }

    public Schedule AddSchedule(AddScheduleDTO scheduleDTO)
    {
        var validation = _postValidator.Validate(scheduleDTO);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Schedule s = _mapper.Map<Schedule>(scheduleDTO);
        s.Logged = false;
        s.ShiftLength = CalculateShiftLength.CalculateLength(scheduleDTO.StartTime, scheduleDTO.EndTime);
        return _repository.AddSchedule(s);
    }

    public Schedule EditSchedule(EditScheduleDTO scheduleDTO, out Schedule oldSchedule)
    {
        var validation = _putValidator.Validate(scheduleDTO);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Schedule s = _mapper.Map<Schedule>(scheduleDTO);
        return _repository.EditSchedule(s, out oldSchedule);
    }

    public void DeleteSchedule(int id)
    {
        _repository.DeleteSchedule(id);
    }
}