using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public class Heatmap
    {
        public DateTime StartOfYear;
        public IEnumerable<LedNode> LedNodes { get; set; }
        public int Height;
        public int Width;
        public int MaxRep = 1;
        public int MinRep = 1;
        // rgb with a max of 255 for the green and red and blue with the remainder to make the green lighter in color
        public int MaxColorValue = 450;
        public int MinColorValue = 50;
        private readonly ILedStripTranslation _ledStripTranslation; 
        public Heatmap(ILedStripTranslation ledStripTranslation, IEnumerable<HabitDayRep> habitDayReps)
        {
            _ledStripTranslation = ledStripTranslation;
            int year = DateTime.Now.Year;
            StartOfYear = new DateTime(year, 1, 1);
            MaxRep = habitDayReps.Max(habit => habit.Repititions);
            MinRep = habitDayReps.Min(habit => habit.Repititions);

            int dayOfWeekOffset = (int)StartOfYear.DayOfWeek;
            LedNodes = from habitDayRep in habitDayReps
                       where habitDayRep.Date.Year == StartOfYear.Year
                       select new LedNode
                       (
                           (habitDayRep.Date.DayOfYear + dayOfWeekOffset) / 7,
                           (habitDayRep.Date.DayOfYear % 7) + dayOfWeekOffset,
                           CalculateColor(habitDayRep.Repititions)
                       );
            _ledStripTranslation.Image.Clear();
            _ledStripTranslation.ToImage(LedNodes);
        }

        public Color CalculateColor(int reps)
        {
            return Color.Red;
        }

        public int NormalizeToMinMaxColor(int reps)
        {
            return (MaxColorValue - MinColorValue) * ((reps - MinRep) / (MaxRep - MinRep)) + MinColorValue;
        }
    }
}
