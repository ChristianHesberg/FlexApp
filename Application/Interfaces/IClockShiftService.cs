using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IClockShiftService
{
    public Session ClockIn(ClockInDTO dto);
    public Session ClockOut(ClockOutDTO dto);
}