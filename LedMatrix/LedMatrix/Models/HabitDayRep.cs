using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public class HabitDayRep
    {
        public DateTime Date { get; set; }
        public int Repititions { get; set; }

        public HabitDayRep(DateTime date, int repititions)
        {
            Date = date;
            Repititions = repititions;
        }
    }
}
