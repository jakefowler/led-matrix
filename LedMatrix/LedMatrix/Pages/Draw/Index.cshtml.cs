﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LedMatrix.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LedMatrix.Pages.Draw
{
    public class IndexModel : PageModel
    {
        public int Height { get; } = Constants.LedHeight;
        public int Width { get; } = Constants.LedWidth;
        public void OnGet()
        {
        }
    }
}