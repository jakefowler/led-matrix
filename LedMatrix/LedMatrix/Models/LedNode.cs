using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public class LedNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
        public LedNode(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }
        public LedNode(int x, int y, int r, int g, int b)
        {
            X = x;
            Y = y;
            Color = Color.FromArgb(r, g, b);
        }
    }
}
