using System.Data;
using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("[controller]")]
public class SessionController : ControllerBase
{
    private ISessionService _sessionService;
    private IFlexBalanceService _flexBalanceService;
    private IScheduleService _scheduleService;

    public SessionController(ISessionService sessionService, IFlexBalanceService flexService, IScheduleService scheduleService)
    {
        _sessionService = sessionService;
        _flexBalanceService = flexService;
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public ActionResult<List<Session>> GetAllSessions()
    {
        return Ok(_sessionService.GetAllSessions());
    }

    [HttpGet]
    [Route("user/{userId}")]
    public ActionResult<List<Session>> GetAllSessionsFromUser(int userId)
    {
        try
        {
            return Ok(_sessionService.GetAllSessionsFromUser(userId));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User with Id could not be found");
        }
    }

    [HttpGet]
    [Route("date/{date}")]
    public ActionResult<List<Session>> GetAllSessionsFromDate(DateTime date)
    {
        try
        {
            return Ok(_sessionService.GetAllSessionsFromDate(date));
        }
        catch(DataException)
        {
            return BadRequest("Please check date input");
        }
    }

    [HttpGet]
    [Route("user/{userId}/date/{date}")]
    public ActionResult<List<Session>> GetAllSessionsOfUserFromDate(int userId, DateTime date)
    {
        try
        {
            return Ok(_sessionService.GetSessionsOfUserFromDate(userId, date));
        }
        catch (DataException)
        {
            return BadRequest("Please check input data");
        }
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Session> GetSessionFromSessionId(int id)
    {
        try
        {
            return Ok(_sessionService.GetSessionFromSessionId(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Could not find session with given Id");
        }
    }

    [HttpPost]
    public ActionResult<Session> PostSession(AddSessionDTO dto)
    {
        try
        {
            Session session = _sessionService.AddSession(dto);
            Schedule schedule =
                _scheduleService.GetScheduleForEmployeeAtDate(session.EmployeeId, session.StartTime.Date);
            _flexBalanceService.AddInitialFlexBalance(session, schedule);
            _scheduleService.LogSchedule(schedule.Id);
            return Created("session/" + session.Id, session);
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
    public ActionResult<Session> PutSession(EditSessionDTO dto)
    {
        try
        {
            Session oldSession;
            Session session = _sessionService.EditSession(dto, out oldSession);
            _flexBalanceService.EditFlexBalance(session, oldSession);
            return Ok(session);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteSession(int id)
    {
        Session session = _sessionService.DeleteSession(id);
        _flexBalanceService.RemoveFlexBalance(session);
        return NoContent();
    }

    [HttpGet]
    [Route("db")]
    public void ResetDb()
    {
        _sessionService.ResetDb();
    }

}