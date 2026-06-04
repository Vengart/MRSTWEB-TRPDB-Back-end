using Microsoft.EntityFrameworkCore;
using Orichalcum.DataAccess;
using Orichalcum.DataAccess.Context;
using Orichalcum.Domains;
using Orichalcum.Domains.Entities.Application;
using Orichalcum.Domains.Entities.GameCard;
using Orichalcum.Domains.Entities.GameSession;
using Orichalcum.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orichalcum.BusinessLogic.Core.GameSession
{
    public class GameSessionActions
    {
        public List<GameSessionData> ExecuteGetAllSessionsAction()
        {
            using (var db = new DatabaseContext())
            {
                return db.GameSessions.ToList();
            }
        }

        public GameSessionData? ExecuteGetSessionByIdAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                return db.GameSessions
                    .Include(s => s.Applications)
                        .ThenInclude(a => a.Player)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public GameSessionData? ExecuteCreateSessionAction(GameSessionData session)
        {
            using (var db = new DatabaseContext())
            {
                var _newSession = new GameSessionData()
                {
                    Title = session.Title,
                    Description = session.Description,
                    System = session.System,
                    Setting = session.Setting,
                    MaxPlayers = session.MaxPlayers,
                    CoverImageUrl = session.CoverImageUrl,
                    Duration = session.Duration,
                    Price = session.Price,
                    Status = SessionStatus.Open,
                    ScheduledAt = session.ScheduledAt,
                    GameMasterId = session.GameMasterId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.GameSessions.Add(_newSession);
                db.SaveChanges();

                // Автоматически создаём GameCard для заметок сессии
                var _card = new GameCardData()
                {
                    Title = session.Title,
                    Content = session.Description,
                    Type = Orichalcum.Domains.Enums.CardType.Other,
                    IsPublic = false,
                    OwnerId = session.GameMasterId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.GameCards.Add(_card);
                db.SaveChanges();

                // Сохраняем id карточки в сессию
                _newSession.GameCardId = _card.Id;
                db.SaveChanges();

                return _newSession;
            }
        }

        public bool ExecuteDeleteSessionAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                var session = db.GameSessions.FirstOrDefault(x => x.Id == id);
                if (session == null) return false;
                db.GameSessions.Remove(session);
                db.SaveChanges();
                return true;
            }
        }

        public GameSessionData? ExecuteUpdateSessionAction(int id, GameSessionData session)
        {
            using (var db = new DatabaseContext())
            {
                var _session = db.GameSessions.FirstOrDefault(x => x.Id == id);
                if (_session == null) return null;
                _session.Title = session.Title;
                _session.Description = session.Description;
                _session.Duration = session.Duration;
                _session.Price = session.Price;
                _session.System = session.System;
                _session.Setting = session.Setting;
                _session.MaxPlayers = session.MaxPlayers;
                _session.CoverImageUrl = session.CoverImageUrl;
                _session.Status = session.Status;
                _session.ScheduledAt = session.ScheduledAt;
                _session.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                return _session;
            }
        }

        protected List<GameSessionData> ExecuteGetSessionsByUserIdAction(int userId)
        {
            using (var db = new DatabaseContext()) // Поменяй на твой способ вызова контекста, если он другой
            {
                return db.GameSessions
                    .Include(s => s.Applications) // Подтягиваем связанные заявки
                    .Where(session =>
                        session.GameMasterId == userId ||
                        (session.Applications != null && session.Applications.Any(a => a.PlayerId == userId && a.Status == ApplicationStatus.Approved))
                    )
                    .ToList();
            }
        }

        public class TimeSlotDto
        {
            public TimeSpan Start { get; set; }
            public TimeSpan End { get; set; }
        }

        public class AdvancedScheduleDto
        {
            public DateTime Date { get; set; }
            public bool IsGmAvailable { get; set; }
            public int PlayerCount { get; set; }
            public List<TimeSlotDto> OverlappingSlots { get; set; } = new();
            public List<string> AvailableUserNames { get; set; } = new();
        }

        // Исправленный метод расчета:
        public List<AdvancedScheduleDto> GetDetailedHeatmap(int sessionId)
        {
            List<Availability> availabilities;

            // 1. Получаем данные из базы с помощью корректного DatabaseContext
            using (var db = new Orichalcum.DataAccess.Context.DatabaseContext())
            {
                availabilities = db.Availabilities
                    .Where(a => a.GameSessionId == sessionId)
                    .Include(a => a.User)
                    .ToList();
            }

            // 2. Группируем по дням уже в оперативной памяти
            var groupedByDate = availabilities.GroupBy(a => a.Date.Date);
            var result = new List<AdvancedScheduleDto>();

            foreach (var group in groupedByDate)
            {
                var gmRecords = group.Where(a => a.User?.Role == UserRole.GameMaster).ToList();
                var playerRecords = group.Where(a => a.User?.Role == UserRole.Player).ToList();

                var dto = new AdvancedScheduleDto
                {
                    Date = group.Key,
                    IsGmAvailable = gmRecords.Any(),
                    PlayerCount = playerRecords.Count,
                    AvailableUserNames = group.Select(a => a.User?.UserName ?? "Unknown").Distinct().ToList(),
                    OverlappingSlots = new List<TimeSlotDto>() // Сразу железно инициализируем список, чтобы не было NullReferenceException
                };

                // 3. Если в этот день есть и ГМ, и игроки — ищем пересечения времени
                if (gmRecords.Any() && playerRecords.Any())
                {
                    foreach (var gmTime in gmRecords)
                    {
                        foreach (var playerTime in playerRecords)
                        {
                            // Находим пересечение (поздний старт и ранний конец)
                            TimeSpan overlapStart = gmTime.StartTime > playerTime.StartTime ? gmTime.StartTime : playerTime.StartTime;
                            TimeSpan overlapEnd = gmTime.EndTime < playerTime.EndTime ? gmTime.EndTime : playerTime.EndTime;

                            // Если пересечение валидно (старт раньше конца)
                            if (overlapStart < overlapEnd)
                            {
                                // Проверяем, нет ли уже точно такого же слота в этот день (чтобы не дублировать)
                                if (!dto.OverlappingSlots.Any(s => s.Start == overlapStart && s.End == overlapEnd))
                                {
                                    dto.OverlappingSlots.Add(new TimeSlotDto
                                    {
                                        Start = overlapStart,
                                        End = overlapEnd
                                    });
                                }
                            }
                        }
                    }

                    // Красиво сортируем получившиеся слоты по времени начала
                    dto.OverlappingSlots = dto.OverlappingSlots.OrderBy(s => s.Start).ToList();
                }

                result.Add(dto);
            }

            return result.OrderBy(r => r.Date).ToList();
        }
    }
}