using System.Drawing;

namespace LedMatrix.Models
{
    public interface IScrollingText
    {
        Color[,] ColorGrid { get; set; }
        string DisplayText { get; set; }
        int Height { get; set; }
        Color PixelColor { get; set; }
        int Width { get; set; }

        bool ScrollText(string displayText, Color color, int loopIterations = 10);
        void ShiftColorGrid();
    }
}