using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LedMatrix.Pages.Draw
{
    public class IndexModel : PageModel
    {
        public int Height { get; } = 7;
        public int Width { get; } = 42;
        public void OnGet()
        {
        }
    }
}