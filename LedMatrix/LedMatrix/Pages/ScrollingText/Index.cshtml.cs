using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using LedMatrix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LedMatrix.Pages.ScrollingText
{
    public class IndexModel : PageModel
    {
        private readonly ILedStripTranslation _ledStripTranslation;
        public IndexModel(ILedStripTranslation ledStripTranslation)
        {
            _ledStripTranslation = ledStripTranslation;
        }

        private Models.ScrollingText _scrollingText;
        public int Height { get; } = 7;
        public int Width { get; } = 42;
        [BindProperty]
        public string Text { get; set; }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            _scrollingText = new Models.ScrollingText(_ledStripTranslation);
            _scrollingText.ScrollText(Text, Color.Green, 1);
        }
    }
}