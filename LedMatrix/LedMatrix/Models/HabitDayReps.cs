using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public class HabitDayReps
    {
        public DateTime Date { get; set; }
        public int Repititions { get; set; }

        public HabitDayReps(DateTime date, int repititions)
        {
            Date = date;
            Repititions = repititions;
        }
    }
}
