using System;
using System.Collections.Generic;

namespace Orichalcum.BusinessLogic.Dto // Проверь свой namespace (можно заменить на нужный)
{
    public class AdvancedScheduleDto
    {
        public DateTime Date { get; set; }
        public bool IsGmAvailable { get; set; }
        public int PlayerCount { get; set; }
        public List<string> AvailableUserNames { get; set; } = new List<string>();
        public List<TimeSlotDto> OverlappingSlots { get; set; } = new List<TimeSlotDto>();
    }

    public class TimeSlotDto
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}