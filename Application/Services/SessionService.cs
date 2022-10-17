using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Models;
using FluentValidation;

namespace Application.Services;

public class SessionService : ISessionService
{

    private ISessionRepository _repo;
    private IMapper _mapper;
    private IValidator<AddSessionDTO> _postValidator;
    private IValidator<EditSessionDTO> _putValidator;

    public SessionService(ISessionRepository repository, IMapper mapper, IValidator<AddSessionDTO> addValidator,
        IValidator<EditSessionDTO> editValidator)
    {
        _repo = repository;
        _mapper = mapper;
        _postValidator = addValidator;
        _putValidator = editValidator;
    }
    
    
    public List<Session> GetAllSessions()
    {
        return _repo.GetAllSessions();
    }

    public List<Session> GetAllSessionsFromUser(int userId)
    {
        return _repo.GetAllSessionsFromUser(userId);
    }

    public List<Session> GetAllSessionsFromDate(DateTime date)
    {
        return _repo.GetAllSessionsFromDate(date);
    }

    public List<Session> GetSessionsOfUserFromDate(int userId, DateTime date)
    {
        return _repo.GetSessionsOfUserFromDate(userId, date);
    }

    public Session GetSessionFromSessionId(int id)
    {
        return _repo.GetSessionFromSessionId(id);
    }

    public Session AddSession(AddSessionDTO sessionDto)
    {
        var validation = _postValidator.Validate(sessionDto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Session session = _mapper.Map<Session>(sessionDto);
        return _repo.AddSession(session);
    }

    public Session EditSession(EditSessionDTO sessionDto, out Session oldSession)
    {
        var validation = _putValidator.Validate(sessionDto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Session session = _mapper.Map<Session>(sessionDto);
        return _repo.EditSession(session, out oldSession);
    }

    public Session DeleteSession(int id)
    {
        return _repo.DeleteSession(id);
    }

    public void ResetDb()
    {
        _repo.ResetDb();
    }
}