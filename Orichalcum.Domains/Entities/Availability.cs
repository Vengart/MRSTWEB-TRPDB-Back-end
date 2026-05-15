using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orichalcum.Domains
{
    public class Availability
    {
        public int Id { get; set; }
        public int GameSessionId { get; set; }

        // ID игрока или мастера (тип string, так как в localStorage у тебя userId)
        public string PlayerId { get; set; } = string.Empty;

        public DateTime Date { get; set; } // Дата (время будет 00:00:00)

        public TimeSpan StartTime { get; set; } // Например, 18:00
        public TimeSpan EndTime { get; set; }   // Например, 22:00
    }
}