using System.Collections.Generic;
using System.Drawing;

namespace LedMatrix.Models
{
    public interface IScrollingText
    {
        Queue<TextToScroll> Texts { get; set; }
        TextToScroll CurrentText { get; set; }
        Color[,] ColorGrid { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        bool IsScrolling { get; set; }
        bool AddText(TextToScroll text);
        bool ScrollText(TextToScroll text);

        void ShiftColorGrid();
    }
}