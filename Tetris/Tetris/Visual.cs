using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    static class Visual
    {
        static public void DrawRect(Graphics grafika, Pen tuzka, string color, int height, int width)
        {
            width *= 35;
            height *= 35;
            grafika.DrawRectangle(tuzka, 1+width, 1+height, 35, 35);
            Color colorBrush;
            switch (color)
            {
                case "Red":
                    colorBrush = Color.Red;
                    break;
                case "LBlue":
                    colorBrush = Color.Turquoise;
                    break;
                case "Orange":
                    colorBrush = Color.DarkOrange;
                    break;
                case "Green":
                    colorBrush = Color.Lime;
                    break;
                case "Yellow":
                    colorBrush = Color.Yellow;
                    break;
                case "DBlue":
                    colorBrush = Color.MediumBlue;
                    break;
                case "Violet":
                    colorBrush = Color.BlueViolet;
                    break;
                default:
                    colorBrush = Color.Black;
                    break;
            }
            SolidBrush sb = new SolidBrush(colorBrush);
            grafika.FillRectangle(sb, 2+width, 2+height, 33, 33);
            colorBrush = Color.FromArgb(colorBrush.A, (int)(colorBrush.R * 0.8), 
                (int)(colorBrush.G * 0.8), (int)(colorBrush.B * 0.8));
            sb.Color = colorBrush;
            grafika.FillRectangle(sb, 11 + width, 11 + height, 15, 15);
        }
    }
}
