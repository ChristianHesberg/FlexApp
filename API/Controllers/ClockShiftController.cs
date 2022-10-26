using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClockShiftController : ControllerBase
{
    
    private IClockShiftService _clockService;
    private IFlexBalanceService _flexBalanceService;
    private IScheduleService _scheduleService;
    
    public ClockShiftController(IClockShiftService clockService, IFlexBalanceService flexService, IScheduleService scheduleService)
    {
        _clockService = clockService;
        _flexBalanceService = flexService;
        _scheduleService = scheduleService;
    }

    [HttpPost]
    [Route("clock_in")]
    public ActionResult<Session> ClockIn(ClockInDTO dto)
    {
        try
        {
            Session session = _clockService.ClockIn(dto);
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

    [HttpPost]
    [Route("clock_out/")]
    public ActionResult<Session> ClockOut(ClockOutDTO dto)
    {
        try
        {
            Session session = _clockService.ClockOut(dto);
            Schedule schedule =
                _scheduleService.GetScheduleForEmployeeAtDate(session.EmployeeId, session.StartTime.Date);
            _flexBalanceService.AddInitialFlexBalance(session, schedule);
            _scheduleService.LogSchedule(schedule.Id);
            return Ok(session);
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
}