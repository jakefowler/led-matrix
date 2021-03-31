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
        // class for passing in parameters through POST
        public class PostText
        {
            public string Text { get; set; }
            public int Iterations { get; set; }
            public int R { get; set; }
            public int G { get; set; }
            public int B { get; set; }
        }
        private readonly IScrollingText _scrollingText;
        public ScrollTextController(IScrollingText scrollingText)
        {
            _scrollingText = scrollingText;
        }

        [HttpPost]
        public ActionResult Post(PostText postText)
        {
            TextToScroll text = new TextToScroll(postText.Text, postText.R, postText.G, postText.B, postText.Iterations);
            if (_scrollingText.IsScrolling)
            {
                _scrollingText.AddText(text);
            }
            else
            {
                _scrollingText.ScrollText(text);
            }
            return new JsonResult(text.Text);
        }
    }
}