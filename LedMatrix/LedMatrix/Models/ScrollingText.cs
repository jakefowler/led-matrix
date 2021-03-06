﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public class ScrollingText : IScrollingText
    {
        public Queue<TextToScroll> Texts { get; set; }
        public TextToScroll CurrentText { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool IsScrolling { get; set; }
        public Color[,] ColorGrid { get; set; }
        private readonly ILedStripTranslation _ledStripTranslation;
        public static Dictionary<char, string[]> Font = new Dictionary<char, string[]>() {
            {' ', new string[5] {"0000000", "0000000", "0000000", "0000000", "0000000" } },
            {'!', new string[5] {"0000000", "0000000", "1011111", "0000000", "0000000" } },
            {'"', new string[5] {"0000000", "0000111", "0000000", "0000111", "0000000" } },
            {'#', new string[5] {"0010100", "1111111", "0010100", "1111111", "0010100" } },
            {'$', new string[5] {"0100100", "0101010", "1111111", "0101010", "0010010" } },
            {'%', new string[5] {"0100011", "0010011", "0001000", "1100100", "1100010" } },
            {'&', new string[5] {"0110110", "1001001", "1010101", "0100010", "1010000" } },
            {'\'', new string[5] {"0000000", "0000101", "0000011", "0000000", "0000000" } },
            {'(', new string[5] {"0000000", "0011100", "0100010", "1000001", "0000000" } },
            {')', new string[5] {"0000000", "1000001", "0100010", "0011100", "0000000" } },
            {'*', new string[5] {"0001000", "0101010", "0011100", "0101010", "0001000" } },
            {'+', new string[5] {"0001000", "0001000", "0111110", "0001000", "0001000" } },
            {',', new string[5] {"0000000", "1010000", "0110000", "0000000", "0000000" } },
            {'-', new string[5] {"0001000", "0001000", "0001000", "0001000", "0001000" } },
            {'.', new string[5] {"0000000", "1100000", "1100000", "0000000", "0000000" } },
            {'/', new string[5] {"0100000", "0010000", "0001000", "0000100", "0000010" } },
            {'0', new string[5] {"0111110", "1010001", "1001001", "1000101", "0111110" } },
            {'1', new string[5] {"0000000", "1000010", "1111111", "1000000", "0000000" } },
            {'2', new string[5] {"1000010", "1100001", "1010001", "1001001", "1000110" } },
            {'3', new string[5] {"0100001", "1000001", "1000101", "1001011", "0110001" } },
            {'4', new string[5] {"0011000", "0010100", "0010010", "1111111", "0010000" } },
            {'5', new string[5] {"0100111", "1000101", "1000101", "1000101", "0111001" } },
            {'6', new string[5] {"0111100", "1001010", "1001001", "1001001", "0110000" } },
            {'7', new string[5] {"0000001", "1110001", "0001001", "0000101", "0000011" } },
            {'8', new string[5] {"0110110", "1001001", "1001001", "1001001", "0110110" } },
            {'9', new string[5] {"0000110", "1001001", "1001001", "0101001", "0011110" } },
            {':', new string[5] {"0000000", "0110110", "0110110", "0000000", "0000000" } },
            {';', new string[5] {"0000000", "1010110", "0110110", "0000000", "0000000" } },
            {'<', new string[5] {"0000000", "0001000", "0010100", "0100010", "1000001" } },
            {'=', new string[5] {"0010100", "0010100", "0010100", "0010100", "0010100" } },
            {'>', new string[5] {"1000001", "0100010", "0010100", "0001000", "0000000" } },
            {'?', new string[5] {"0000010", "0000001", "1010001", "0001001", "0000110" } },
            {'@', new string[5] {"0110010", "1001001", "1111001", "1000001", "0111110" } },
            {'A', new string[5] {"1111110", "0010001", "0010001", "0010001", "1111110" } },
            {'B', new string[5] {"1111111", "1001001", "1001001", "1001001", "0110110" } },
            {'C', new string[5] {"0111110", "1000001", "1000001", "1000001", "0100010" } },
            {'D', new string[5] {"1111111", "1000001", "1000001", "0100010", "0011100" } },
            {'E', new string[5] {"1111111", "1001001", "1001001", "1001001", "1000001" } },
            {'F', new string[5] {"1111111", "0001001", "0001001", "0000001", "0000001" } },
            {'G', new string[5] {"0111110", "1000001", "1000001", "1010001", "0110010" } },
            {'H', new string[5] {"1111111", "0001000", "0001000", "0001000", "1111111" } },
            {'I', new string[5] {"0000000", "1000001", "1111111", "1000001", "0000000" } },
            {'J', new string[5] {"0100000", "1000000", "1000001", "0111111", "0000001" } },
            {'K', new string[5] {"1111111", "0001000", "0010100", "0100010", "1000001" } },
            {'L', new string[5] {"1111111", "1000000", "1000000", "1000000", "1000000" } },
            {'M', new string[5] {"1111111", "0000010", "0000100", "0000010", "1111111" } },
            {'N', new string[5] {"1111111", "0000100", "0001000", "0010000", "1111111" } },
            {'O', new string[5] {"0111110", "1000001", "1000001", "1000001", "0111110" } },
            {'P', new string[5] {"1111111", "0001001", "0001001", "0001001", "0000110" } },
            {'Q', new string[5] {"0111110", "1000001", "1010001", "0100001", "1011110" } },
            {'R', new string[5] {"1111111", "0001001", "0011001", "0101001", "1000110" } },
            {'S', new string[5] {"1000110", "1001001", "1001001", "1001001", "0110001" } },
            {'T', new string[5] {"0000001", "0000001", "1111111", "0000001", "0000001" } },
            {'U', new string[5] {"0111111", "1000000", "1000000", "1000000", "0111111" } },
            {'V', new string[5] {"0011111", "0100000", "1000000", "0100000", "0011111" } },
            {'W', new string[5] {"1111111", "0100000", "0011000", "0100000", "1111111" } },
            {'X', new string[5] {"1100011", "0010100", "0001000", "0010100", "1100011" } },
            {'Y', new string[5] {"0000011", "0000100", "1111000", "0000100", "0000011" } },
            {'Z', new string[5] {"1100001", "1010001", "1001001", "1000101", "1000011" } },
            {'[', new string[5] {"0000000", "0000000", "1111111", "1000001", "1000001" } },
            {'\\', new string[5] {"0000010", "0000100", "0001000", "0010000", "0100000" } },
            {']', new string[5] {"1000001", "1000001", "1111111", "0000000", "0000000" } },
            {'^', new string[5] {"0000100", "0000010", "0000001", "0000010", "0000100" } },
            {'_', new string[5] {"1000000", "1000000", "1000000", "1000000", "1000000" } },
            {'`', new string[5] {"0000000", "0000001", "0000010", "0000100", "0000000" } },
            {'a', new string[5] {"0100000", "1010100", "1010100", "1010100", "1111000" } },
            {'b', new string[5] {"1111111", "1001000", "1000100", "1000100", "0111000" } },
            {'c', new string[5] {"0111000", "1000100", "1000100", "1000100", "0100000" } },
            {'d', new string[5] {"0111000", "1000100", "1000100", "1001000", "1111111" } },
            {'e', new string[5] {"0111000", "1010100", "1010100", "1010100", "0011000" } },
            {'f', new string[5] {"0001000", "1111110", "0001001", "0000001", "0000010" } },
            {'g', new string[5] {"0001000", "0010100", "1010100", "1010100", "0111100" } },
            {'h', new string[5] {"1111111", "0001000", "0000100", "0000100", "1111000" } },
            {'i', new string[5] {"0000000", "1000100", "1111101", "1000000", "0000000" } },
            {'j', new string[5] {"0100000", "1000000", "1000100", "0111101", "0000000" } },
            {'k', new string[5] {"0000000", "1111111", "0010000", "0101000", "1000100" } },
            {'l', new string[5] {"0000000", "1000001", "1111111", "1000000", "0000000" } },
            {'m', new string[5] {"1111100", "0000100", "0011000", "0000100", "1111000" } },
            {'n', new string[5] {"1111100", "0001000", "0000100", "0000100", "1111000" } },
            {'o', new string[5] {"0111000", "1000100", "1000100", "1000100", "0111000" } },
            {'p', new string[5] {"1111100", "0010100", "0010100", "0010100", "0001000" } },
            {'q', new string[5] {"0001000", "0010100", "0010100", "0011000", "1111100" } },
            {'r', new string[5] {"1111100", "0001000", "0000100", "0000100", "0001000" } },
            {'s', new string[5] {"1001000", "1010100", "1010100", "1010100", "0100000" } },
            {'t', new string[5] {"0000100", "0111111", "1000100", "1000000", "0100000" } },
            {'u', new string[5] {"0111100", "1000000", "1000000", "0100000", "1111100" } },
            {'v', new string[5] {"0011100", "0100000", "1000000", "0100000", "0011100" } },
            {'w', new string[5] {"0111100", "1000000", "0110000", "1000000", "0111100" } },
            {'x', new string[5] {"1000100", "0101000", "0010000", "0101000", "1000100" } },
            {'y', new string[5] {"0001100", "1010000", "1010000", "1010000", "0111100" } },
            {'z', new string[5] {"1000100", "1100100", "1010100", "1001100", "1000100" } },
            {'{', new string[5] {"0000000", "0001000", "0110110", "1000001", "0000000" } },
            {'|', new string[5] {"0000000", "0000000", "1111111", "0000000", "0000000" } },
            {'}', new string[5] {"0000000", "1000001", "0110110", "0001000", "0000000" } },
            {'°', new string[5] {"0000000", "0000000", "0000111", "0000101", "0000111" } },
            {'Ä', new string[5] {"1111101", "0010010", "0010010", "1111101", "0000000" } },
            {'Ö', new string[5] {"0111101", "1000010", "1000010", "1000010", "0111101" } },
            {'Ü', new string[5] {"0111101", "1000000", "1000000", "1000000", "0111101" } },
            {'ä', new string[5] {"0100000", "1010101", "1010100", "1010101", "1111000" } },
            {'ö', new string[5] {"0111010", "1000100", "1000100", "0111010", "0000000" } },
            {'ü', new string[5] {"0111010", "1000000", "1000000", "0111010", "0000000" } },
            {'€', new string[5] {"0010100", "0111110", "1010101", "1000001", "0100010" } }
        };

        public ScrollingText(ILedStripTranslation ledStripTranslation)
        {
            _ledStripTranslation = ledStripTranslation;
            Texts = new Queue<TextToScroll>();
        }

        public bool AddText(TextToScroll text)
        {
            Texts.Enqueue(text);
            return true;
        }
        public bool ScrollText(TextToScroll text)
        {
            if (Texts.Count > 0)
            {
                Texts.Enqueue(text);
                CurrentText = Texts.Dequeue();
                if (IsScrolling)
                {
                    return true;
                }
            }
            else
            {
                CurrentText = text;
            }
            IsScrolling = true;
            _ledStripTranslation.Image.Clear();
            _ledStripTranslation.Device.Update();
            ColorGrid = new Color[_ledStripTranslation.Height, _ledStripTranslation.Width];

            // adding letters one column at a time
            while (CurrentText.ScrollIterations > 0)
            {
                foreach (char c in CurrentText.Letters)
                {
                    IEnumerable<string> currentLetter = Font[c];
                    foreach (string column in currentLetter)
                    {
                        ShiftColorGrid();
                        // add on new column
                        for (int i = 0; i < column.Length; i++)
                        {
                            if (column[i].Equals('1'))
                            {
                                ColorGrid[i, ColorGrid.GetLength(1) - 1] = CurrentText.Color;
                            }
                            else
                            {
                                ColorGrid[i, ColorGrid.GetLength(1) - 1] = Color.Empty;
                            }
                        }
                        _ledStripTranslation.Image.Clear();
                        _ledStripTranslation.ToImage(ColorGrid);
                        _ledStripTranslation.Device.Update();
                        System.Threading.Thread.Sleep(100);
                    }
                }
                // shift to clear scrolling text
                for (int i = 0; i < _ledStripTranslation.Width; i++)
                {
                    ShiftColorGrid();
                    for (int j = 0; j < _ledStripTranslation.Height; j++)
                    {
                        ColorGrid[j, ColorGrid.GetLength(1) - 1] = Color.Empty;
                    }
                    _ledStripTranslation.Image.Clear();
                    _ledStripTranslation.ToImage(ColorGrid);
                    _ledStripTranslation.Device.Update();
                    System.Threading.Thread.Sleep(100);
                }
                CurrentText.ScrollIterations--;
                if (Texts.Count > 0)
                {
                    CurrentText = Texts.Dequeue();
                }
            }
            IsScrolling = false;
            return true;
        }

        public void ShiftColorGrid()
        {
            for (int i = 0; i < ColorGrid.GetLength(1) - 1; i++)
            {
                for (int j = 0; j < ColorGrid.GetLength(0); j++)
                {
                    ColorGrid[j, i] = ColorGrid[j, i + 1];
                }
            }
        }
    }
}
