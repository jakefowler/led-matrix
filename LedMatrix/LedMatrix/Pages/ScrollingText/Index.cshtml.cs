using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LedMatrix.Pages.ScrollingText
{
    public class IndexModel : PageModel
    {
        private Models.ScrollingText scrollingText;
        private Models.LedStripTranslation ledStripTranslation;
        public int Height { get; } = 7;
        public int Width { get; } = 42;
        [BindProperty]
        public string Text { get; set; }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            ledStripTranslation = new Models.LedStripTranslation(Height, Width);
            scrollingText = new Models.ScrollingText(Text, Color.FromArgb(67, 94, 82), ledStripTranslation, 1);
        }
    }
}