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
    public class PixelController : ControllerBase
    {
        public class Pixel
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int R { get; set; }
            public int G { get; set; }
            public int B { get; set; }
        }
        private readonly ILedStripTranslation _ledStripTranslation;
        public PixelController(ILedStripTranslation ledStripTranslation)
        {
            _ledStripTranslation = ledStripTranslation;
        }
        [HttpPost]
        public JsonResult Post(Pixel pixel)
        {
            LedNode ledNode = new LedNode(pixel.X, pixel.Y, pixel.R, pixel.G, pixel.B);
            _ledStripTranslation.ToImage(ledNode);
            _ledStripTranslation.Device.Update();
            return new JsonResult(new { pixel.X, pixel.Y, pixel.R, pixel.G, pixel.B });
        }
    }
}