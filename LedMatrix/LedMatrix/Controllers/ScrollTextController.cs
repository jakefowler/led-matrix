using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using LedMatrix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LedMatrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrollTextController : ControllerBase
    {
        public class ScrollText
        {
            public string Text { get; set; }
        }
        private readonly IScrollingText _scrollingText;
        public ScrollTextController(IScrollingText scrollingText)
        {
            _scrollingText = scrollingText;
        }

        [HttpPost]
        public ActionResult Post(ScrollText scrollText)
        {
            _scrollingText.IsScrolling = false;
            System.Threading.Thread.Sleep(100);
            _scrollingText.ScrollText(scrollText.Text, Color.BlueViolet, 1);
            return new JsonResult(scrollText.Text);
        }
    }
}