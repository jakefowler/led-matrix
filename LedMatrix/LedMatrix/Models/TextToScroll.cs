using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public class TextToScroll
    {
        public IEnumerable<char> Letters { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int ScrollIterations { get; set; }
        public TextToScroll(string text, Color color, int scrollIterations)
        {
            Color = color;
            Text = text;
            Letters = text;
            ScrollIterations = scrollIterations;
        }
        public TextToScroll(string text, int r, int g, int b, int scrollIterations)
        {
            Color = Color.FromArgb(r, g, b);
            R = r;
            G = g;
            B = b;
            Text = text;
            Letters = text;
            ScrollIterations = scrollIterations;
        }
    }
}
