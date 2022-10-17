using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface ISessionService
{
    public List<Session> GetAllSessions();
    public List<Session> GetAllSessionsFromUser(int userId);
    public List<Session> GetAllSessionsFromDate(DateTime date);
    public List<Session> GetSessionsOfUserFromDate(int userId, DateTime date);
    public Session GetSessionFromSessionId(int id);
    public Session AddSession(AddSessionDTO session);
    public Session EditSession(EditSessionDTO session, out Session oldSession);
    public Session DeleteSession(int id);
    public void ResetDb();
}