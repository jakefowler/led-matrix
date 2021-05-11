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
        public int MaxColorValue = 350;
        public int MinColorValue = 10;
        private readonly ILedStripTranslation _ledStripTranslation; 
        public Heatmap(ILedStripTranslation ledStripTranslation)
        {
            _ledStripTranslation = ledStripTranslation;
        }
        public bool UpdateHeatmap(IEnumerable<HabitDayRep> habitDayReps)
        {
            int year = DateTime.Now.Year;
            StartOfYear = new DateTime(year, 1, 1);
            MaxRep = habitDayReps.Max(habit => habit.Repititions);
            MinRep = habitDayReps.Min(habit => habit.Repititions);

            int dayOfWeekOffset = (int)StartOfYear.DayOfWeek;
            LedNodes = from habitDayRep in habitDayReps
                       where habitDayRep.Date.Year == StartOfYear.Year
                       select new LedNode
                       (
                           ((habitDayRep.Date.DayOfYear + dayOfWeekOffset - 1) / 7),
                           Math.Abs(((habitDayRep.Date.DayOfYear + dayOfWeekOffset - 1) % 7) - 6),
                           CalculateColor(habitDayRep.Repititions)
                       );
            _ledStripTranslation.Image.Clear();
            _ledStripTranslation.ToImage(LedNodes);
            _ledStripTranslation.Device.Update();
            return true;
        }
        public Color CalculateColor(int reps)
        {
            int colorRepValue = MaxColorValue + MinColorValue - NormalizeToMinMaxColor(reps);
            int r = 0;
            int g = 0;
            int b = 0;
            if (colorRepValue > 255)
            {
                r = colorRepValue - 255;
                g = 255;
                b = colorRepValue - 255;
                return Color.FromArgb(r, g, b);
            }
            return Color.FromArgb(r, colorRepValue, b);
        }
        public int NormalizeToMinMaxColor(int reps)
        {
            return (int)((MaxColorValue - MinColorValue) * (((double)reps - MinRep) / (MaxRep - MinRep)) + MinColorValue);
        }
    }
}
