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
        [BindProperty]
        public string Text { get; set; }
        public void OnGet()
        {
            int height = 7;
            int width = 42;
            ledStripTranslation = new Models.LedStripTranslation(height, width);
            scrollingText = new Models.ScrollingText("hello from the web", Color.Red, ledStripTranslation, 1);
        }

        public void OnPost()
        {
            int height = 7;
            int width = 42;
            ledStripTranslation = new Models.LedStripTranslation(height, width);
            scrollingText = new Models.ScrollingText(Text, Color.FromArgb(67, 94, 82), ledStripTranslation, 1);
        }
    }
}