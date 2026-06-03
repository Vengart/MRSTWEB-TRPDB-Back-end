using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orichalcum.DataAccess.Context;
using Orichalcum.Domains;
using Orichalcum.Domains.Entities;
using System;
using System.Linq;

namespace Orichalcum.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        // 1. Принимаем данные через безопасный класс-запрос
        [HttpPost]
        public IActionResult SaveAvailability([FromBody] AvailabilityRequest req)
        {
            if (req == null) return BadRequest("Неверные данные");
            if (string.IsNullOrWhiteSpace(req.StartTime) || string.IsNullOrWhiteSpace(req.EndTime))
            {
                using (var db = new DatabaseContext())
                {
                    var existing = db.Availabilities
                        .FirstOrDefault(a => a.GameSessionId == req.GameSessionId
                                          && a.UserId == req.UserId
                                          && a.Date == req.Date.Date);

                    if (existing != null)
                    {
                        db.Availabilities.Remove(existing);
                        db.SaveChanges();
                    }
                    return Ok(); // Успешно удалили, день снова свободен
                }
            }

            // Ниже остаётся старый код парсинга и сохранения...
            if (!TimeSpan.TryParse(req.StartTime, out TimeSpan start) ||
                !TimeSpan.TryParse(req.EndTime, out TimeSpan end))
            {
                return BadRequest("Неверный формат времени. Используйте ЧЧ:ММ");
            }

            using (var db = new DatabaseContext())
            {
                var existing = db.Availabilities
                    .FirstOrDefault(a => a.GameSessionId == req.GameSessionId
                                      && a.UserId == req.UserId
                                      && a.Date == req.Date.Date);

                if (existing != null)
                {
                    existing.StartTime = start;
                    existing.EndTime = end;
                }
                else
                {
                    var newAvailability = new Availability
                    {
                        GameSessionId = req.GameSessionId,
                        UserId = req.UserId,
                        Date = req.Date.Date,
                        StartTime = start,
                        EndTime = end
                    };
                    db.Availabilities.Add(newAvailability);
                }

                db.SaveChanges();
                return Ok();
            }
        }

        // 2. Отдаем все заполненные даты для конкретной сессии (оставляем как было)
        [HttpGet("session/{sessionId}")]
        public IActionResult GetSessionAvailability(int sessionId)
        {
            using (var db = new DatabaseContext())
            {
                var list = db.Availabilities
                    .Where(a => a.GameSessionId == sessionId)
                    .ToList();

                return Ok(list);
            }
        }
    }

    // Небольшой вспомогательный класс прямо здесь, чтобы обойти жесткую валидацию .NET
    public class AvailabilityRequest
    {
        public int GameSessionId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; } // Принимаем строкой, чтобы не падать на "18:00"
        public string EndTime { get; set; }   // Принимаем строкой
    }
}