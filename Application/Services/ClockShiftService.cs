using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;

namespace Application.Services;

public class ClockShiftService : IClockShiftService
{
    private ISessionRepository _repo;
    private IMapper _mapper;
    private IValidator<ClockInDTO> _inValidator;
    private IValidator<ClockOutDTO> _outValidator;

    public ClockShiftService(ISessionRepository repository, IMapper mapper, IValidator<ClockInDTO> inValidator,
        IValidator<ClockOutDTO> outValidator)
    {
        _repo = repository;
        _mapper = mapper;
        _inValidator = inValidator;
        _outValidator = outValidator;
    }

    public Session ClockIn(ClockInDTO dto)
    {
        var validation = _inValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Session session = _mapper.Map<Session>(dto);
        session.EndTime = DateTime.MinValue;

        Session checkClockIn = _repo.GetSessionNoClockout(dto.EmployeeId);
        if(checkClockIn.Id == -1)
            return _repo.AddSession(session);

        throw new ArgumentException("Already Clocked In");
    }

    public Session ClockOut(ClockOutDTO dto)
    {
        var validation = _outValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Session session = _mapper.Map<Session>(dto);

        Session clockedIn = _repo.GetSessionNoClockout(session.EmployeeId);
        clockedIn.EndTime = session.EndTime;
        if (clockedIn.Id != null)
            return _repo.EditSessionClockOut(clockedIn);
        throw new ArgumentException("Not Clocked In");
    }
}