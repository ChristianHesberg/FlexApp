﻿using System.Data;
using Application.Interfaces;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{

    private AppDbContext _dbContext;

    public SessionRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    
    public List<Session> GetAllSessions()
    {
        return _dbContext.Sessions.ToList();
    }

    public List<Session> GetAllSessionsFromUser(int userId)
    {
        List<Session> sessions = _dbContext.Sessions.Where(s => s.EmployeeId == userId).Select(s => s).ToList();
        if (!sessions.IsNullOrEmpty())
            return sessions;
        throw new KeyNotFoundException();
    }

    public List<Session> GetAllSessionsFromDate(DateTime date)
    {
        List<Session> sessions = _dbContext.Sessions.Where(s => s.StartTime.Date == date.Date).Select(s => s).ToList();
        if (!sessions.IsNullOrEmpty())
            return sessions;
        throw new DataException();
    }

    public List<Session> GetSessionsOfUserFromDate(int userId, DateTime date)
    {
        List<Session> sessions = _dbContext.Sessions
            .Where(s => s.EmployeeId == userId)
            .Where(s => s.StartTime.Date == date.Date)
            .Select(s => s)
            .ToList();
        if (!sessions.IsNullOrEmpty())
            return sessions;
        throw new DataException();
    }

    public Session GetSessionFromSessionId(int id)
    {
        Session session = _dbContext.Sessions.Find(id);
        if (session != null)
            return session;
        throw new KeyNotFoundException();
    }

    public Session AddSession(Session session)
    {
        Session addedSession = _dbContext.Sessions.Add(session).Entity;
        _dbContext.SaveChanges();
        return new Session() 
            {Id = addedSession.Id, 
            EndTime = addedSession.EndTime, 
            StartTime = addedSession.StartTime, 
            EmployeeId = addedSession.EmployeeId};
    }

    public Session EditSessionClockOut(Session session)
    {
        Session edit = _dbContext.Sessions.Find(session.Id);
        if (edit != null)
        {
            edit.EndTime = session.EndTime;
            _dbContext.SaveChanges();
            return session;
        }
        throw new KeyNotFoundException();
    }

    public Session EditSession(Session session, out Session oldSession)
    {
        Session edit = _dbContext.Sessions.Find(session.Id);
        if (edit != null)
        {
            oldSession = new Session()
                { Id = edit.Id, StartTime = edit.StartTime, EndTime = edit.EndTime, EmployeeId = edit.EmployeeId };
            edit.StartTime = session.StartTime;
            edit.EndTime = session.EndTime;
            _dbContext.SaveChanges();
            return session;
        }
        throw new KeyNotFoundException();
    }

    public Session DeleteSession(int id)
    {
        Session session = _dbContext.Sessions.Find(id);
        if (session != null)
        {
            _dbContext.Sessions.Remove(session);
            _dbContext.SaveChanges();
            return session;
        }
        throw new KeyNotFoundException();
    }

    public Session GetSessionNoClockout(int id)
    {
        Session session = _dbContext.Sessions
            .Where(s => s.EndTime == DateTime.MinValue)
            .Where(s => s.EmployeeId == id)
            .Select(s => s)
            .FirstOrDefault();
        if (session != null)
            return new Session()
            {
                Id = session.Id,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                EmployeeId = session.EmployeeId
            };
        return new Session() { Id = -1 };
    }
    
    
    public void ResetDb()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }
}