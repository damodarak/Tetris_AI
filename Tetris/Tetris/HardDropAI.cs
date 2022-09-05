﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class HardDropAI
    {
        //nalezeni vsech moznych pozic pro pouziti funkce HardDrop ve vsech rotaci
        static private int[,,] findAllHardDrops(ref GameBoard gb, Shape shp)
        {
            int[,,] konec;
            switch (shp.Color)
            {
                //pocet moznych rotaci a HardDropu 
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
            for (int i = 0; i < 4; i++)//presuneme se na nejlevejsi stranu a budeme postupne 'HardDropovat' TetroBlock a nasledne rotovat
            {
                shp.MoveLeft(ref gb);
            }
            switch (konec.GetLength(0))//rozhodneme podle velikosti nulte dimenze, coz je pocet moznych dropu ve vsech rotacich
            {
                case 9:
                    leftToRightHardDrop(ref gb, shp, 0, 9, ref konec);//nalezeni vsech dropu
                    for (int i = 0; i < 4; i++)//vraceni zpet na startovni pozici
                    {
                        shp.MoveRight(ref gb);
                    }
                    break;
                case 17:
                    if (shp.Color == 'O')
                    {
                        for (int i = 7; i < 17; i++)
                        {
                            konec[i, 0, 2] = 1;
                        }
                        leftToRightHardDrop(ref gb, shp, 0, 7, ref konec);
                        shp.RotRight(ref gb);
                        shp.MoveLeft(ref gb);
                        leftToRightHardDrop(ref gb, shp, 7, 17, ref konec);
                        shp.MoveRight(ref gb);
                        shp.RotRight(ref gb);
                    }
                    else
                    {
                        for (int i = 8; i < 17; i++)
                        {
                            konec[i, 0, 2] = 1;
                        }
                        leftToRightHardDrop(ref gb, shp, 0, 8, ref konec);
                        shp.RotRight(ref gb);
                        shp.MoveLeft(ref gb);
                        leftToRightHardDrop(ref gb, shp, 8, 17, ref konec);
                        shp.MoveRight(ref gb);
                        shp.RotRight(ref gb);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        shp.MoveRight(ref gb);
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
                    leftToRightHardDrop(ref gb, shp, 0, 8, ref konec);
                    shp.RotRight(ref gb);
                    shp.MoveLeft(ref gb);
                    leftToRightHardDrop(ref gb, shp, 8, 17, ref konec);
                    shp.RotRight(ref gb);
                    leftToRightHardDrop(ref gb, shp, 17, 25, ref konec);
                    shp.RotRight(ref gb);
                    shp.MoveLeft(ref gb);
                    leftToRightHardDrop(ref gb, shp, 25, 34, ref konec);
                    shp.MoveRight(ref gb);
                    shp.RotRight(ref gb);
                    for (int i = 0; i < 3; i++)
                    {
                        shp.MoveRight(ref gb);
                    }
                    break;
                default:
                    break;
            }
            return konec;
        }
        //pomoci funkce FakeHardDrop nalezneme vsechny mozne HardDropy v dane rotaci a zapiseme je do pole konec
        static private void leftToRightHardDrop(ref GameBoard gb, Shape shp, int od, int kam, ref int[,,] konec)
        {
            for (int i = od; i < kam; i++)
            {
                int[,] hardDrop = shp.FakeHardDrop(ref gb);
                for (int j = 0; j < 4; j++)//prepsani hodnot do spravnych mist v poli konec, to zajistuji hodnoty 'int od' a 'int kam'
                {
                    konec[i, j, 0] = hardDrop[j, 0];
                    konec[i, j, 1] = hardDrop[j, 1];
                }
                shp.MoveRight(ref gb);
            }
            for (int i = od; i < kam; i++)//navraceni zpet na nejlevejsi kraj hraci desky
            {
                shp.MoveLeft(ref gb);
            }
        }
        static private int checkBlockedHoles(ref GameBoard gb, int[,] Pozice)
        {
            Queue q;
            int numOfHoles = 0;//pocet zablokovanych der
            char[,] deska = (char[,])gb.Board.Clone();

            GameBoard.markPozice(ref deska, Pozice);//dosadime do hraci desky umistenou figurku
            int[] clearLines = GameBoard.FindFullLines(ref deska);
            GameBoard.MoveMap(ref deska, clearLines);//budeme kontrolovat blokovane diry po odstranenych radach

            for (int i = 0; i < 4; i++)
            {
                if (GameBoard.contains(clearLines, Pozice[i,0]))//pouze v pripade, ze dana pozice nezpusobi vymazani rady
                {
                    continue;
                }
                q = new Queue();
                int tempHoles = 0;
                int posunuti = countPosunuti(clearLines, Pozice[i,0]);

                if (Pozice[i, 0] + posunuti < 19 && deska[Pozice[i, 0] + 1 + posunuti, Pozice[i, 1]] == '\0')
                {
                    deska[Pozice[i, 0] + 1 + posunuti, Pozice[i, 1]] = 'C'; //oCCupied
                    q.Insert(new int[2] { Pozice[i, 0] + 1 + posunuti, Pozice[i, 1] });
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
                            BFS(q, deska, '\0', 'C', coord, ref tempHoles);
                        }
                    }
                    if (tempHoles == 0)
                    {
                        clearAfterBFS(deska, new int[2] { Pozice[i, 0] + 1 + posunuti, Pozice[i, 1] });
                    }
                    else
                    {
                        numOfHoles += tempHoles;
                    }
                }
            }
            return numOfHoles;
        }
        static public void clearAfterBFS(char[,] deska, int[] coord)
        {
            //vycistime desku stejnym prohledavanim BFS jako kdyz jsme prohledavali pocet blokovanych der akorat menime naopak chary
            Queue q = new Queue();
            deska[coord[0], coord[1]] = '\0';
            q.Insert(coord);
            int uselessNum = 0;
            while(q.Count())//BFS - premenujeme 'C' char na '\0'
            {
                int[] block = q.Pop();
                BFS(q, deska, 'C', '\0', block, ref uselessNum);
            }          
        }
        static public void BFS(Queue q, char[,] deska, char searchChar, char changeChar, int[] souradnice, ref int holes)
        {
            //jeden krok BFS - zkusime vsechny smery a pokud splnuje souradnice nase podminku (searchChar), tak to prepiseme na changeChar a
            //pridame do fronty na pozdejsi prohledavani
            if (souradnice[0] - 1 >= 2 && deska[souradnice[0] - 1, souradnice[1]] == searchChar)
            {
                ++holes;
                deska[souradnice[0] - 1, souradnice[1]] = changeChar;
                q.Insert(new int[2] { souradnice[0] - 1, souradnice[1] });
            }
            if (souradnice[0] + 1 <= 19 && deska[souradnice[0] + 1, souradnice[1]] == searchChar)
            {
                ++holes;
                deska[souradnice[0] + 1, souradnice[1]] = changeChar;
                q.Insert(new int[2] { souradnice[0] + 1, souradnice[1] });
            }
            if (souradnice[1] - 1 >= 0 && deska[souradnice[0], souradnice[1] - 1] == searchChar)
            {
                ++holes;
                deska[souradnice[0], souradnice[1] - 1] = changeChar;
                q.Insert(new int[2] { souradnice[0], souradnice[1] - 1 });
            }
            if (souradnice[1] + 1 <= 9 && deska[souradnice[0], souradnice[1] + 1] == searchChar)
            {
                ++holes;
                deska[souradnice[0], souradnice[1] + 1] = changeChar;
                q.Insert(new int[2] { souradnice[0], souradnice[1] + 1 });
            }
        }
        static public int checkSoftBlockedHoles(ref GameBoard gb, int[,] Pozice, int hardBlocked)
        {
            if (hardBlocked>0)//zajimame se o pripad kdy, nemame hardBlocked diry, protoze kazda hardBlocked je zaroven i softBlocked
            {
                return 0;
            }
            int numOfSoftHoles = 0;
            char[,] deska = (char[,])gb.Board.Clone();
            int[,] clonePozice = (int[,])Pozice.Clone();

            GameBoard.markPozice(ref deska, clonePozice);
            int[] clearLines = GameBoard.FindFullLines(ref deska);
            GameBoard.MoveMap(ref deska, clearLines);//budeme kontrolovat blokovane diry po odstranenych radach

            for (int i = 0; i < 4; i++)
            {
                if (GameBoard.contains(clearLines, Pozice[i, 0]))//pouze v pripade, ze dana pozice nezpusobi vymazani rady
                {
                    continue;
                }
                int posunuti = countPosunuti(clearLines, clonePozice[i,0]);

                //hledame softBlocked diry svisle dolu od souradnic bloku (i v pripade posunuti)
                while (clonePozice[i, 0] + 1 + posunuti <= 19 && deska[clonePozice[i, 0] + 1 + posunuti, clonePozice[i, 1]] == '\0')
                {
                    numOfSoftHoles++;
                    clonePozice[i, 0] += 1;
                }
            }
            return numOfSoftHoles;
        }
        static public int checkHeightDiff(ref GameBoard gb, int[,] Pozice)
        {
            int height = boardHeight(ref gb);
            int heighestBlock = 0;
            GameBoard gbnew = gb.Copy();
            GameBoard.markPozice(ref gbnew.Board, Pozice);
            int[] clearLines = GameBoard.FindFullLines(ref gbnew.Board);

            for (int i = 0; i < 4; i++)
            {
                if (GameBoard.contains(clearLines, Pozice[i, 0]))
                {
                    continue;
                }

                int posunuti = countPosunuti(clearLines, Pozice[i,0]);

                if ((20 - Pozice[i, 0] - posunuti) > heighestBlock)
                {
                    heighestBlock = 20 - Pozice[i, 0] - posunuti;
                }
            }
            int diff = heighestBlock - height;
            return diff;
        }
        static private int boardHeight(ref GameBoard gb)
        {
            int lineNum = 2;
            while (lineNum <= 19 && checkLineClear(ref gb, lineNum))//nalezeni prvniho radku, ktery neni prazdny (radek plny '\0')
            {
                ++lineNum;
            }
            int boardHeight = 20 - lineNum;//hraci deska je reprezentovana opacne...nejvyssi pole maji nejnizsi indexy
            return boardHeight;
        }
        static private bool checkLineClear(ref GameBoard gb, int lineNum)
        {
            for (int i = 0; i < 10; i++)
            {
                if (gb.Board[lineNum, i] != '\0')
                {
                    return false;
                }
            }
            return true;
        }
        static public bool checkLineFull(char[,] deska, int lineNum)
        {
            for (int i = 0; i < 10; i++)
            {
                if (deska[lineNum, i] == '\0')
                {
                    return false;
                }
            }
            return true;
        }
        static public int checkFullLines(ref GameBoard gb, int[,] Pozice)
        {
            //funkce vrati pocet plnych rad po zapsani int[] Pozice
            int fullLines = 0;
            int[] checkedLines = new int[4];
            char[,] deska = (char[,])gb.Board.Clone();
            GameBoard.markPozice(ref deska, Pozice);
            for (int i = 0; i < 4; i++)
            {
                if (!GameBoard.contains(checkedLines, Pozice[i,0]))
                {
                    checkedLines[i] = Pozice[i, 0];
                    if (checkLineFull(deska, Pozice[i,0]))
                    {
                        fullLines++;
                    }
                }
            }
            return fullLines;
        }
        static public int deltaHrbolatosti(ref GameBoard gb, int[,] Pozice)
        {
            int[] vyskaSloupu = new int[10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 2; j < 20; j++)//vypocita vysku kazdeho sloupce v hracim poli
                {
                    if (gb.Board[j, i] != '\0')
                    {
                        vyskaSloupu[i] = 20 - j;
                        break;
                    }
                }
            }

            int hrbolatostPred = 0;
            for (int i = 0; i < 9; i++)//vypocet "hrbolatosti"
            {
                hrbolatostPred += Math.Abs(vyskaSloupu[i] - vyskaSloupu[i + 1]);
            }

            char[,] deska = (char[,])gb.Board.Clone();
            GameBoard.markPozice(ref deska, Pozice);

            int[] clearLines = new int[5];
            GameBoard.FindFullLines(ref deska);
            GameBoard.MoveMap(ref deska, clearLines);//budeme kontrolovat blokovane diry po odstranenych radach

            vyskaSloupu = new int[10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 2; j < 20; j++)
                {
                    if (deska[j, i] != '\0')
                    {
                        vyskaSloupu[i] = 20 - j;
                        break;
                    }
                }
            }
            int hrbolatostPo = 0;
            for (int i = 0; i < 9; i++)
            {
                hrbolatostPo += Math.Abs(vyskaSloupu[i] - vyskaSloupu[i + 1]);
            }
            return hrbolatostPo - hrbolatostPred;
        }
        static public int[,] FindBestPlaceForDrop(ref GameBoard gb,  Shape shp)
        {
            int[,,] drops = findAllHardDrops(ref gb, shp);
            int[,] bestDrop = new int[5,2];//bestDrop[4,0] je pocet rotaci
            int score = 5000;
            for (int i = 0; i < drops.GetLength(0); i++)
            {
                int[,] tempDrop = new int[5, 2];
                for (int j = 0; j < 4; j++)
                {
                    tempDrop[j, 0] = drops[i, j, 0];
                    tempDrop[j, 1] = drops[i, j, 1];
                }
                tempDrop[4, 0] = drops[i, 0, 2];
                //data for decision
                int hardBlocked = checkBlockedHoles(ref gb, tempDrop);
                int softBlocked = checkSoftBlockedHoles(ref gb, tempDrop, hardBlocked);
                int diff = checkHeightDiff(ref gb, tempDrop);
                int numLines = checkFullLines(ref gb, tempDrop);
                int hrbolatost = deltaHrbolatosti(ref gb, tempDrop);
                //data

                int tempScore = hardBlocked * 9 + softBlocked * 7 + diff * 5 - numLines * 3 + hrbolatost / 4;
                if (numLines>2)
                {
                    bestDrop = tempDrop;
                    break;
                }

                if (score > tempScore)
                {
                    score = tempScore;
                    bestDrop = tempDrop;
                }
            }

            Form1.Ghost = (int[,])bestDrop.Clone();
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
            //posunuti TetroBlocku, aby byl cely viditelny
            if (shp.Pozice[0,0] < 2 || shp.Pozice[1,0] < 2 || shp.Pozice[2,0] < 2 || shp.Pozice[3,0] < 2)
            {               
                return shp.MoveDown(ref gb);
            }
            //hodnota finishPoz[4,0] je pocetRotaci
            else if (finishPoz[4, 0] > 0 && finishPoz[4,0] <=3)
            {               
                finishPoz[4, 0]--;
                return shp.RotRight(ref gb);
            }
            //zde jsme jiz ve spravne rotaci
            else if (finishPoz[4, 0] == 0)
            {
                if (samePozice(shp.Pozice, finishPoz))//jsme ve spravne pozici a muzeme se zacit pohybovat dolu
                {
                    finishPoz[4, 0] = 200;//200 znamnena pohyb dolu
                }
                else if (findDirect(shp.Pozice, finishPoz))// true znamena doleva a false doprava
                {
                    finishPoz[4, 0] = -100;//doleva
                }
                else
                {
                    finishPoz[4, 0] = 100;//doprava
                }
            }


            if (samePozice(shp.Pozice, finishPoz))
            {
                finishPoz[4, 0] = 200;
            }

            if (finishPoz[4,0] == -100)
            {
                if (!shp.MoveLeft(ref gb))
                {
                    finishPoz[4, 0] = 200;
                    return shp.MoveDown(ref gb);
                }
            }
            else if (finishPoz[4, 0] == 200)//if signal for moving down is set
            {
                return shp.MoveDown(ref gb);
            }
            else if (finishPoz[4,0] == 100)
            {               
                return shp.MoveRight(ref gb);
            }
            return true;
        }
        static private bool samePozice(int[,] poz1, int[,] poz2)
        {
            //kontrola zda 2 dvojice int[4,2] jsou totozne
            //hledame bijektivni zobrazeni
            bool found;
            for (int i = 0; i < 4; i++)
            {
                found = false;
                for (int j = 0; j < 4; j++)
                {
                    if (poz1[i,0] == poz2[j,0] && poz1[i,1] == poz2[j,1])
                    {
                        found = true;//nalezeni prvku
                    }
                }
                if (!found)//nenasli jsme shodu
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
            if (samePozice(poz,finish))//dosli jsme maximalne doleva a pokud to stale neni totozna pozice, tak je spravny smer doprava
            {
                return true;//doleva
            }
            else
            {
                return false;//doprava
            }
        }
        static public int countPosunuti(int[] clearLines, int line)
        {
            int posunuti = 0;
            for (int j = 0; j < clearLines[4]; j++)
            {
                if (clearLines[j] > line)
                {
                    posunuti++;
                }
            }
            return posunuti;
        }
    }
}
