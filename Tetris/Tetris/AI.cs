using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class AI
    {
        static private void tetrisDFS(ref GameBoard gb, Shape shp, Stack st, string navigace, char fromWhere)
        {
            if (shp.MoveDownNotPossible(ref gb))
            {
                st.Insert(shp.Pozice, navigace);
            }
            else
            {
                if (fromWhere != 'L' && shp.MoveLeft(ref gb))
                {                   
                    tetrisDFS(ref gb, shp, st, navigace + 'L', 'R');//prichazim zprava
                    shp.markVisited(ref gb);
                    shp.MoveRight(ref gb);
                }
                if (shp.MoveDown(ref gb))
                {
                    tetrisDFS(ref gb, shp, st, navigace + 'D', 'H');//prichazim sHora
                    shp.markVisited(ref gb);
                    shp.MoveUp();
                }
                if (fromWhere != 'R' && shp.MoveRight(ref gb))
                {
                    tetrisDFS(ref gb, shp, st, navigace + 'R', 'L');
                    shp.markVisited(ref gb);
                    shp.MoveLeft(ref gb);
                }               
            }
        }
        static private void cleanBoard(ref GameBoard gb)
        {
            for (int i = 0; i < gb.Board.GetLength(0); i++)
            {
                for (int j = 0; j < gb.Board.GetLength(1); j++)
                {
                    if (gb.Board[i,j]=='F')
                    {
                        gb.Board[i, j] = '\0';
                    }
                }
            }
        }
        static public string findBestPosition(ref GameBoard gb, Shape active, Shape next)
        {
            Stack st = new Stack();
            tetrisDFS(ref gb, active, st, "", 'N');//'N' z Nikama
            cleanBoard(ref gb);
            string bestPosition = "";
            int score = 5000;

            int pocet = st.Count();
            for (int i = 0; i < pocet; i++)
            {
                InfoBlock tempDrop = st.Pop();

                //data for decision
                int hardBlocked = checkBlockedHoles(ref gb, tempDrop.ArrayValue);
                int softBlocked = HardDropAI.checkSoftBlockedHoles(ref gb, tempDrop.ArrayValue, hardBlocked);
                int diff = HardDropAI.checkHeightDiff(ref gb, tempDrop.ArrayValue);
                int numLines = HardDropAI.checkFullLines(ref gb, tempDrop.ArrayValue);
                int hrbolatost = HardDropAI.deltaHrbolatosti(ref gb, tempDrop.ArrayValue);
                //data

                int tempScore = hardBlocked * 9 + softBlocked * 7 + diff * 5 - numLines * 3 + hrbolatost / 4;
                if (numLines > 2)
                {
                    //test data
                    Form1.test1 = hardBlocked;
                    Form1.test2 = softBlocked;
                    Form1.test3 = hrbolatost;
                    Form1.test4 = diff;
                    Form1.test5 = numLines;

                    bestPosition = tempDrop.StringValue;
                    break;
                }

                if (score > tempScore)
                {
                    //test data
                    Form1.test1 = hardBlocked;
                    Form1.test2 = softBlocked;
                    Form1.test3 = hrbolatost;
                    Form1.test4 = diff;
                    Form1.test5 = numLines;

                    bestPosition = tempDrop.StringValue;
                    score = tempScore;
                }
            }
            return bestPosition;
        }
        static private int checkBlockedHoles(ref GameBoard gb, int[,] Pozice)
        {
            Queue q;
            int numOfHoles = 0;//pocet zablokovanych der
            char[,] deska = (char[,])gb.Board.Clone();
            for (int i = 0; i < 4; i++)//dosadime do hraci desky umistenou figurku
            {
                deska[Pozice[i, 0], Pozice[i, 1]] = 'P';//Pozice
            }

            int[] clearLines = new int[5];
            for (int i = 0; i < 4; i++)
            {
                if (HardDropAI.checkLineFull(deska, Pozice[i, 0]))
                {
                    for (int j = 0; j < 10; j++)
                    {
                        deska[Pozice[i, 0], j] = '\0';
                    }
                    clearLines[clearLines[4]] = Pozice[i, 0];
                    clearLines[4]++;
                }
            }
            GameBoard.MoveMap(ref deska, clearLines);//budeme kontrolovat blokovane diry po odstranenych radach

            for (int i = 0; i < 4; i++)
            {
                if (GameBoard.contains(clearLines, Pozice[i, 0]))
                {
                    continue;
                }
                q = new Queue();
                int tempHoles = 0;
                int posunuti = 0;
                for (int j = 0; j < clearLines[4]; j++)
                {
                    if (clearLines[j] > Pozice[i, 0])
                    {
                        posunuti++;
                    }
                }
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
                            HardDropAI.BFS(q, deska, '\0', 'C', coord, ref tempHoles);
                        }
                    }
                    if (tempHoles == 0)
                    {
                        HardDropAI.clearAfterBFS(deska, new int[2] { Pozice[i, 0] + 1 + posunuti, Pozice[i, 1] });
                    }
                    else
                    {
                        numOfHoles += tempHoles;
                    }
                }

                tempHoles = 0;
                q = new Queue();
                if (Pozice[i, 1] > 0 && deska[Pozice[i, 0] + posunuti, Pozice[i, 1] - 1] == '\0')
                {
                    deska[Pozice[i, 0] + posunuti, Pozice[i, 1] - 1] = 'C'; //oCCupied
                    q.Insert(new int[2] { Pozice[i, 0] + posunuti, Pozice[i, 1] - 1 });
                    ++tempHoles;//
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
                            HardDropAI.BFS(q, deska, '\0', 'C', coord, ref tempHoles);
                        }
                    }
                    if (tempHoles == 0)
                    {
                        HardDropAI.clearAfterBFS(deska, new int[2] { Pozice[i, 0] + posunuti, Pozice[i, 1] - 1 });
                    }
                    else
                    {
                        numOfHoles += tempHoles;
                    }
                }

                tempHoles = 0;
                q = new Queue();
                if (Pozice[i, 1] < 10 && deska[Pozice[i, 0] + posunuti, Pozice[i, 1] + 1] == '\0')
                {
                    deska[Pozice[i, 0] + posunuti, Pozice[i, 1] + 1] = 'C'; //oCCupied
                    q.Insert(new int[2] { Pozice[i, 0] + posunuti, Pozice[i, 1] + 1 });
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
                            HardDropAI.BFS(q, deska, '\0', 'C', coord, ref tempHoles);
                        }
                    }
                    if (tempHoles == 0)
                    {
                        HardDropAI.clearAfterBFS(deska, new int[2] { Pozice[i, 0] + posunuti, Pozice[i, 1] + 1 });
                    }
                    else
                    {
                        numOfHoles += tempHoles;
                    }
                }
            }
            return numOfHoles;
        }
    }
}
