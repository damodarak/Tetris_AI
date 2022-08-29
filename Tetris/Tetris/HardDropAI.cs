using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class HardDropAI
    {
        static private int[,,] findAllHardDrops(ref GameBoard gb, Shape shp)
        {
            int[,,] konec;
            dynamic tvar = shp;
            switch (tvar.Color)
            {
                case 'O':
                    konec = new int[17, 4, 3];// konec[i,0,2] je pocet rotaci, zbytek nic neznamena
                    break;
                case 'R':
                    konec = new int[9, 4, 3];
                    break;
                case 'D':
                    konec = new int[34, 4, 3];
                    break;
                case 'V':
                    konec = new int[34, 4, 3];
                    break;
                case 'Y':
                    konec = new int[34, 4, 3];
                    break;
                case 'L':
                    konec = new int[17, 4, 3];
                    break;
                case 'G':
                    konec = new int[17, 4, 3];
                    break;
                default:
                    konec = new int[1, 1, 1];
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                tvar.MoveLeft(ref gb);
            }
            switch (konec.GetLength(0))
            {
                case 9:
                    leftToRightHardDrop(ref gb, tvar, 0, 9, ref konec);
                    for (int i = 0; i < 4; i++)
                    {
                        tvar.MoveRight(ref gb);
                    }
                    break;
                case 17:
                    if (tvar.Color == 'O')
                    {
                        for (int i = 7; i < 17; i++)
                        {
                            konec[i, 0, 2] = 1;
                        }
                        leftToRightHardDrop(ref gb, tvar, 0, 7, ref konec);
                        tvar.RotRight(ref gb);
                        tvar.MoveLeft(ref gb);
                        leftToRightHardDrop(ref gb, tvar, 7, 17, ref konec);
                        tvar.MoveRight(ref gb);
                        tvar.RotRight(ref gb);
                    }
                    else
                    {
                        for (int i = 8; i < 17; i++)
                        {
                            konec[i, 0, 2] = 1;
                        }
                        leftToRightHardDrop(ref gb, tvar, 0, 8, ref konec);
                        tvar.RotRight(ref gb);
                        tvar.MoveLeft(ref gb);
                        leftToRightHardDrop(ref gb, tvar, 8, 17, ref konec);
                        tvar.MoveRight(ref gb);
                        tvar.RotRight(ref gb);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        tvar.MoveRight(ref gb);
                    }
                    break;
                case 34:
                    for (int i = 8; i < 17; i++)
                    {
                        konec[i, 0, 2] = 1;
                    }
                    for (int i = 17; i < 25; i++)
                    {
                        konec[i, 0, 2] = 2;
                    }
                    for (int i = 25; i < 34; i++)
                    {
                        konec[i, 0, 2] = 3;
                    }
                    leftToRightHardDrop(ref gb, tvar, 0, 8, ref konec);
                    tvar.RotRight(ref gb);
                    tvar.MoveLeft(ref gb);
                    leftToRightHardDrop(ref gb, tvar, 8, 17, ref konec);
                    tvar.RotRight(ref gb);
                    leftToRightHardDrop(ref gb, tvar, 17, 25, ref konec);
                    tvar.RotRight(ref gb);
                    tvar.MoveLeft(ref gb);
                    leftToRightHardDrop(ref gb, tvar, 25, 34, ref konec);
                    tvar.MoveRight(ref gb);
                    tvar.RotRight(ref gb);
                    for (int i = 0; i < 3; i++)
                    {
                        tvar.MoveRight(ref gb);
                    }
                    break;
                default:
                    break;
            }
            return konec;
        }
        static private void leftToRightHardDrop(ref GameBoard gb, Shape tvar, int od, int kam, ref int[,,] konec)
        {
            dynamic tvr = tvar;
            for (int i = od; i < kam; i++)
            {
                int[,] hardDrop = tvar.FakeHardDrop(ref gb, tvr.Pozice);
                for (int j = 0; j < 4; j++)
                {
                    konec[i, j, 0] = hardDrop[j, 0];
                    konec[i, j, 1] = hardDrop[j, 1];
                }
                tvar.MoveRight(ref gb);
            }
            for (int i = od; i < kam; i++)
            {
                tvar.MoveLeft(ref gb);
            }
        }
        static private int checkBlockedHoles(ref GameBoard gb, int[,] Pozice)
        {
            Queue q;
            int numOfHoles = 0;
            char[,] deska = (char[,])gb.Board.Clone();
            for (int i = 0; i < 4; i++)
            {
                deska[Pozice[i, 0], Pozice[i, 1]] = 'P';//Pozice
            }
            for (int i = 0; i < 4; i++)
            {
                q = new Queue();
                int tempHoles = 0;
                if (Pozice[i, 0] != 19 && deska[Pozice[i, 0] + 1, Pozice[i, 1]] == '\0')
                {
                    deska[Pozice[i, 0] + 1, Pozice[i, 1]] = 'C'; //oCCupied
                    q.Insert(new int[2] { Pozice[i, 0] + 1, Pozice[i, 1] });
                    ++tempHoles;
                    while (q.Count())
                    {
                        int[] coord = q.Pop();
                        if (coord[0] == 2)
                        {
                            tempHoles = 0;
                            break;
                        }
                        else
                        {
                            if (coord[0] - 1 >= 2 && deska[coord[0] - 1, coord[1]] == '\0')
                            {
                                ++tempHoles;
                                deska[coord[0] - 1, coord[1]] = 'C';
                                q.Insert(new int[2] { coord[0] - 1, coord[1] });
                            }
                            if (coord[0] + 1 <= 19 && deska[coord[0] + 1, coord[1]] == '\0')
                            {
                                ++tempHoles;
                                deska[coord[0] + 1, coord[1]] = 'C';
                                q.Insert(new int[2] { coord[0] + 1, coord[1] });
                            }
                            if (coord[1] - 1 >= 0 && deska[coord[0], coord[1] - 1] == '\0')
                            {
                                ++tempHoles;
                                deska[coord[0], coord[1] - 1] = 'C';
                                q.Insert(new int[2] { coord[0], coord[1] - 1 });
                            }
                            if (coord[1] + 1 <= 9 && deska[coord[0], coord[1] + 1] == '\0')
                            {
                                ++tempHoles;
                                deska[coord[0], coord[1] + 1] = 'C';
                                q.Insert(new int[2] { coord[0], coord[1] + 1 });
                            }
                        }
                    }
                    if (tempHoles == 0)
                    {
                        clearAfterBFS(deska, new int[2] { Pozice[i, 0] + 1, Pozice[i, 1] });
                    }
                    else
                    {
                        numOfHoles += tempHoles;
                    }
                }
            }
            return numOfHoles;
        }
        static private void clearAfterBFS(char[,] deska, int[] coord)
        {
            Queue q = new Queue();
            deska[coord[0], coord[1]] = '\0';
            q.Insert(coord);
            while (q.Count())
            {
                int[] souradnice = q.Pop();
                if (souradnice[0] - 1 >= 2 && deska[souradnice[0] - 1, souradnice[1]] == 'C')
                {
                    deska[souradnice[0] - 1, souradnice[1]] = '\0';
                    q.Insert(new int[2] { souradnice[0] - 1, souradnice[1] });
                }
                if (souradnice[0] + 1 <= 19 && deska[souradnice[0] + 1, souradnice[1]] == 'C')
                {
                    deska[souradnice[0] + 1, souradnice[1]] = '\0';
                    q.Insert(new int[2] { souradnice[0] + 1, souradnice[1] });
                }
                if (souradnice[1] - 1 >= 0 && deska[souradnice[0], souradnice[1] - 1] == 'C')
                {
                    deska[souradnice[0], souradnice[1] - 1] = '\0';
                    q.Insert(new int[2] { souradnice[0], souradnice[1] - 1 });
                }
                if (souradnice[1] + 1 <= 9 && deska[souradnice[0], souradnice[1] + 1] == 'C')
                {
                    deska[souradnice[0], souradnice[1] + 1] = '\0';
                    q.Insert(new int[2] { souradnice[0], souradnice[1] + 1 });
                }
            }
        }
        static public int[,] FindBestPlaceForDrop(ref GameBoard gb,  Shape shp)
        {
            int[,,] drops = findAllHardDrops(ref gb, shp);
            int[,] bestDrop = new int[5,2];//bestDrop[4,0] je pocet rotaci
            int bestDropHoles = 200;
            for (int i = 0; i < drops.GetLength(0); i++)
            {
                int[,] tempDrop = new int[5, 2];
                for (int j = 0; j < 4; j++)
                {
                    tempDrop[j, 0] = drops[i, j, 0];
                    tempDrop[j, 1] = drops[i, j, 1];
                }
                tempDrop[4, 0] = drops[i, 0, 2];
                int cis = checkBlockedHoles(ref gb, tempDrop);
                if (bestDropHoles > cis)
                {
                    bestDropHoles = cis;
                    bestDrop = tempDrop;
                }
            }
            while (bestDrop[0,0] != 2 && bestDrop[1,0] != 2 && bestDrop[2,0] != 2 && bestDrop[3,0] != 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    bestDrop[i, 0] -= 1;
                }
            }
            return bestDrop;
        }
        static public bool PlayNextMove(ref GameBoard gb, Shape shp, int[,] finishPoz)
        {
            dynamic tvar = shp;
            if (finishPoz[4, 0] > 0 && finishPoz[4,0] <=3)
            {
                tvar.RotRight(ref gb);
                finishPoz[4, 0]--;
                return true;
            }
            else if (finishPoz[4, 0] == 0)
            {
                if (samePozice(tvar.Pozice, finishPoz))
                {
                    finishPoz[4, 0] = 200;
                }
                else if (findDirect(tvar.Pozice, finishPoz))
                {
                    finishPoz[4, 0] = -100;//doleva
                }
                else
                {
                    finishPoz[4, 0] = 100;//doprava
                }
            }


            if (samePozice(tvar.Pozice, finishPoz))
            {
                finishPoz[4, 0] = 200;
            }

            if (finishPoz[4,0] == -100)
            {
                if (!tvar.MoveLeft(ref gb))
                {
                    finishPoz[4, 0] = 200;
                    return tvar.MoveDown(ref gb);
                }
            }
            else if (finishPoz[4, 0] == 200)//if signal for moving down is set
            {
                return tvar.MoveDown(ref gb);
            }
            else if (finishPoz[4,0] == 100)
            {
                tvar.MoveRight(ref gb);
                return true;
            }
            return true;//just to calm down visual studio
        }
        static private bool samePozice(int[,] poz1, int[,] poz2)
        {
            bool found;
            for (int i = 0; i < 4; i++)
            {
                found = false;
                for (int j = 0; j < 4; j++)
                {
                    if (poz1[i,0] == poz2[j,0] && poz1[i,1] == poz2[j,1])
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    return false;
                }
            }
            return true;
        }
        static private bool findDirect(int[,] pozShape, int[,] finish)
        {
            int[,] poz = (int[,])pozShape.Clone();
            while (!samePozice(poz, finish) && poz[0,1] != 0 && poz[1,1] != 0 && poz[2,1] != 0 && poz[3,1] != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    poz[i, 1] -= 1;
                }
            }
            if (samePozice(poz,finish))//pokud jsme vyleteli z hraci desky
            {
                return true;//doleva
            }
            else
            {
                return false;//doprava
            }
        }
    }
}
