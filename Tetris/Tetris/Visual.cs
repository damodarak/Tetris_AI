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
        static public void DrawRect(Graphics grafika, Pen tuzka, char color, int height, int width)
        {
            if (color == '\0')
            {
                return;
            }
            width *= 35;
            height *= 35;
            grafika.DrawRectangle(tuzka, 1+width, 1+height, 35, 35);
            Color colorBrush;
            switch (color)
            {
                case 'R':
                    colorBrush = Color.Red;
                    break;
                case 'L':
                    colorBrush = Color.Turquoise;
                    break;
                case 'O':
                    colorBrush = Color.DarkOrange;
                    break;
                case 'G':
                    colorBrush = Color.Lime;
                    break;
                case 'Y':
                    colorBrush = Color.Yellow;
                    break;
                case 'D':
                    colorBrush = Color.MediumBlue;
                    break;
                case 'V':
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
        static public void DrawShape(Shape shp, Graphics grafika, Pen tuzka)
        {
            dynamic tvar = shp;
            for (int i = 0; i < 4; i++)
            {
                DrawRect(grafika, tuzka, tvar.Color, tvar.Pozice[i, 0] - 2, tvar.Pozice[i, 1]);
            }
        }
        static public void DrawMap(ref GameBoard gb, Graphics grafika, Pen tuzka)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DrawRect(grafika, tuzka, gb.Board[i, j], i-2, j);
                }
            }
        }
        static public void DrawGame(ref GameBoard gb, Shape shp, Graphics grafika, Pen tuzka)
        {
            DrawMap(ref gb, grafika, tuzka);
            DrawShape(shp, grafika, tuzka);
        }
    }
}
