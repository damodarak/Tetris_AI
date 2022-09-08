/*
Tetris
David Kroupa, I. ročník, 31 st. skupina
letní semestr 2021/22
Programování 2 NPRG031
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    //staticka trida pro zobrazeni a grafickou ilustraci prubehu hru
    static class Visual
    {
        //nejzakladnejsi metoda, protoze se cela hra soustredi okolo ctvercu
        static public void DrawRect(Graphics grafika, Pen tuzka, char color, int height, int width)
        {
            Color colorBrush;
            switch (color)
            {
                case '\0':
                    return;
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
            width *= 35;
            height *= 35;
            grafika.DrawRectangle(tuzka, 1 + width, 1 + height, 35, 35);//ctverec

            SolidBrush sb = new SolidBrush(colorBrush);
            grafika.FillRectangle(sb, 2+width, 2+height, 33, 33);//vyvarveni ctverce

            colorBrush = Color.FromArgb(colorBrush.A, (int)(colorBrush.R * 0.8), 
                (int)(colorBrush.G * 0.8), (int)(colorBrush.B * 0.8));
            sb.Color = colorBrush;
            grafika.FillRectangle(sb, 11 + width, 11 + height, 15, 15);// mensi ctverec pro hezci design
        }
        //zobrazeni hraci figurky
        static public void DrawShape(Shape shp, Graphics grafika, Pen tuzka)
        {
            for (int i = 0; i < 4; i++)
            {
                DrawRect(grafika, tuzka, shp.Color, shp.Pozice[i, 0] - 2, shp.Pozice[i, 1]);
            }
        }
        //zobrazeni hraciho pole bez figurky
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
        static public void DrawNextPiece(Shape shp, Graphics grafika, Pen tuzka)
        {
            for (int i = 0; i < 4; i++)
            {
                DrawRect(grafika, tuzka, shp.Color, shp.Pozice[i, 0] - 1, shp.Pozice[i, 1]-2);
            }
        }
        //tato funkce se vola pri spusteni modu HardDropAI nebo ImpovedAI
        static public void DrawGhost(ref GameBoard gb, int[,] ghost, Graphics grafika, Pen tuzka)
        {
            SolidBrush sb = new SolidBrush(Color.FromArgb(255, 240, 245));
            for (int i = 0; i < 4; i++)
            {
                grafika.DrawRectangle(tuzka, (35 * ghost[i, 1]) + 1, (35 * (ghost[i, 0] - 2)) + 1, 35, 35);
                grafika.FillRectangle(sb, (35 * ghost[i, 1]) + 2, (35 * (ghost[i, 0] - 2)) + 2, 33, 33);
            }
        }
        static public void DrawWB(ref WallBreaker wbgame, Graphics grafika, Pen tuzka)
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DrawRect(grafika, tuzka, wbgame.Board[i,j], i, j);
                }
            }
            DrawRect(grafika, tuzka, 'B', 17, wbgame.Hrac);//B - black
            if (wbgame.Strela[0] != -1)
            {
                DrawRect(grafika, tuzka, 'B', wbgame.Strela[0], wbgame.Strela[1]);
            }
        }
    }
}
