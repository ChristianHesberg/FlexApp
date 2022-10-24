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
        return _repo.AddSession(session);
    }

    public Session ClockOut(ClockOutDTO dto)
    {
        var validation = _outValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Session session = _mapper.Map<Session>(dto);
        return _repo.AddSession(session);
    }
}