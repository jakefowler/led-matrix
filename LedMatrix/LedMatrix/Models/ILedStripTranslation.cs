﻿using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public interface ILedStripTranslation
    {

        int Height { get; set; }
        int Width { get; set; }
        BitmapImage Image { get; set; }
        Ws2812b Device { get; set; }
        bool ToImage(Color[,] colorGrid);
        bool ToImage(IEnumerable<LedNode> ledNodes);
        bool ToImage(LedNode ledNode);
        bool EraseLedNode(LedNode ledNode);
    }
}
