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
        private readonly ILedStripTranslation _ledStripTranslation;
        public PixelController(ILedStripTranslation ledStripTranslation)
        {
            _ledStripTranslation = ledStripTranslation;
        }
        [HttpPost]
        public ActionResult PostPixel(int x, int y)
        {
            _ledStripTranslation.LedNodeToImage(new LedNode(x, y, Color.Green));
            return new JsonResult(true);
        }

    }
}