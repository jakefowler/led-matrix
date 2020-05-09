using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using System;
using System.Collections.Generic;
using System.Device.Spi;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public class LedStripTranslation : ILedStripTranslation
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public BitmapImage Image { get; set; }
        public Ws2812b Device { get; set; }
        public LedStripTranslation(int height = 7, int width = 42)
        {

            Height = height;
            Width = width;

            SpiConnectionSettings settings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 2_400_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            SpiDevice spi = SpiDevice.Create(settings);
            Device = new Ws2812b(spi, width, height);

            Image = Device.Image;
            Device.Update();
        }

        public bool TwoDArrayToImage(Color[,] colorGrid)
        {
            int adjusted_col;
            int height = colorGrid.GetLength(0);
            int width = colorGrid.GetLength(1);

            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    if (row % 2 == 0)
                    {
                        adjusted_col = Math.Abs(col - width + 1);
                    }
                    else
                    {
                        adjusted_col = col;
                    }
                    if (colorGrid[row, col] != null)
                    {
                        Image.SetPixel(adjusted_col, row, colorGrid[row, col]);
                    }
                }
            }
            return true;
        }

        public bool LinkedListToImage(LinkedList<LedNode> ledNodes)
        {
            int adjusted_X;
            foreach (LedNode node in ledNodes)
            {
                if (node.Y % 2 == 0)
                {
                    adjusted_X = Math.Abs(node.X - Width + 1);
                }
                else
                {
                    adjusted_X = node.X;
                }
                if (node.Y >= 0 && node.Y < Height && adjusted_X >= 0 && adjusted_X < Width)
                {
                    Image.SetPixel(adjusted_X, node.Y, node.Color);
                }
                else
                {
                    Console.WriteLine("X: " + adjusted_X + " Y: " + node.Y);
                }
            }
            return true;
        }

        public bool LedNodeToImage(LedNode node)
        {
            if (node.Y >= 0 && node.Y < Height && node.X >= 0 && node.X < Width)
            {
                int adjusted_X;
                if (node.Y % 2 == 0)
                {
                    adjusted_X = Math.Abs(node.X - Width + 1);
                }
                else
                {
                    adjusted_X = node.X;
                }
                Image.SetPixel(adjusted_X, node.Y, node.Color);
                return true;
            }
            return false;
        }

        public bool EraseLedNode(LedNode node)
        {
            if (node.Y >= 0 && node.Y < Height && node.X >= 0 && node.X < Width)
            {
                int adjusted_X;
                if (node.Y % 2 == 0)
                {
                    adjusted_X = Math.Abs(node.X - Width + 1);
                }
                else
                {
                    adjusted_X = node.X;
                }
                Image.SetPixel(adjusted_X, node.Y, Color.Empty);
                return true;
            }
            return false;
        }
    }
}
