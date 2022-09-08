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

namespace Tetris
{
    static class AI
    {
        //naprosto zbytecne a z nejakeho duvodu nefunkcni DFS, v projektu neni pouzita
        static private void tetrisDFS(ref GameBoard gb, Shape shp, Stack st, string navigace, char fromWhere)
        {
            //SHIT METHOD
            //AVOID!!!
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
        //nalezeni vsech moznych pozic, kam muze figurka dojit a ulozeni i s navigaci do fronty positions
        static private void tetrisBFS(ref GameBoard gb, Shape shp, QueuePozic positions, int numRots)
        {
            QueuePozic qp = new QueuePozic();
            string start = "";
            for (int i = 0; i < numRots; i++)
            {
                start += 'G';//rotace doprava
            }
            for (int i = 0; i < 4; i++)
            {
                if (gb.Board[shp.Pozice[i,0],shp.Pozice[i,1]] != '\0')//deska je preplena a nemuzeme do ni pridat TetroBlock
                {
                    return;
                }
            }
            qp.Insert(shp.Pozice, start);
            markVisited(ref gb, shp.Pozice);
            while (qp.Count())//klasicke BFS
            {
                InfoBlock ib = qp.Pop();
                if (Shape.MoveDownNotPossible(ref gb, ib.ArrayValue))//pod figurkou se nachazi dolni hranice nebo jine barevne bloky
                {
                    positions.Insert(ib);
                }

                if (Shape.checkLeftSide(ref gb, ib.ArrayValue))
                {
                    int[,] insert = new int[4, 2];
                    for (int i = 0; i < 4; i++)
                    {
                        insert[i, 0] = ib.ArrayValue[i, 0];
                        insert[i, 1] = ib.ArrayValue[i, 1] - 1;
                    }
                    qp.Insert(insert, ib.StringValue + 'L');//k navigaci pridame krok doleva
                    markVisited(ref gb, insert);//zaznamename, ze jsme jiz v teto pozici byli
                }
                if (Shape.checkRightSide(ref gb, ib.ArrayValue))
                {
                    int[,] insert = new int[4, 2];
                    for (int i = 0; i < 4; i++)
                    {
                        insert[i, 0] = ib.ArrayValue[i, 0];
                        insert[i, 1] = ib.ArrayValue[i, 1] + 1;
                    }
                    qp.Insert(insert, ib.StringValue + 'R');
                    markVisited(ref gb, insert);
                }
                if (Shape.checkDownSide(ref gb, ib.ArrayValue))
                {
                    int[,] insert = new int[4, 2];
                    for (int i = 0; i < 4; i++)
                    {
                        insert[i, 0] = ib.ArrayValue[i, 0] + 1;
                        insert[i, 1] = ib.ArrayValue[i, 1];
                    }
                    qp.Insert(insert, ib.StringValue + 'D');
                    markVisited(ref gb, insert);
                }
            }
        }
        //funkce pro oznackovani pro BFS a DFS
        static private void markVisited(ref GameBoard gb, int[,] visited)
        {
            for (int i = 0; i < 4; i++)
            {
                gb.Board[visited[i, 0], visited[i, 1]] = 'F';//tetris dFs, bFs
            }
        }
        //uklizeni po BFS nebo DFS hraci desky
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
            QueuePozic movesFirst = new QueuePozic();
            QueuePozic movesSecond = new QueuePozic();
            findAllMoves(ref gb, active, movesFirst);//ve fronte movesFirst mame vsechny mozne vysledne pozice

            string bestPosition = "";//navigace pro nejlepsi pozici
            int score = 5000;

            int pocet1 = movesFirst.NumCount();//pocet moznosti pro vyslendou pozici
            for (int i = 0; i < pocet1; i++)
            {
                InfoBlock tempDrop1 = movesFirst.Pop();

                int tempScore = calculateScore(ref gb, tempDrop1);

                GameBoard gbnew = gb.Copy();
                GameBoard.markPozice(ref gbnew.Board, tempDrop1.ArrayValue);
                int[] clearLines = gbnew.FindFullLines();
                GameBoard.ClearLines(ref gbnew, clearLines);
                GameBoard.MoveMap(ref gbnew.Board, clearLines);

                findAllMoves(ref gbnew, next, movesSecond);//nalezeni vsech pozic po dosazeni TetroBlocku z prvni fronty
                int pocet2 = movesSecond.NumCount();
                for (int j = 0; j < pocet2; j++)
                {
                    InfoBlock tempDrop2 = movesSecond.Pop();
                    int tempScore2 = calculateScore(ref gbnew, tempDrop2);
                    if (score > tempScore + tempScore2)
                    {
                        score = tempScore + tempScore2;
                        bestPosition = tempDrop1.StringValue;
                        Form1.Ghost = (int[,])tempDrop1.ArrayValue.Clone();//ulozeni nejlepsi pozice do Ducha, abychom videli, kam AI miri
                    }
                }
            }
            return bestPosition;
        }
        static private void findAllMoves(ref GameBoard gb, Shape shp, QueuePozic qp)
        {
            //BFS algoritmus provedeme ve vsech rotacich
            int rotCount = shp.NumOfRots();//poce rotaci
            tetrisBFS(ref gb, shp, qp, 0);
            cleanBoard(ref gb);
            for (int i = 0; i < rotCount; i++)
            {
                shp.RotRight(ref gb);
                tetrisBFS(ref gb, shp, qp, i + 1);
                cleanBoard(ref gb);
            }
            shp.RotRight(ref gb);//vraceni do puvodniho stavu rotace
        }
        static private int calculateScore(ref GameBoard gb, InfoBlock tempDrop)
        {
            //data for decision
            int hardBlocked = checkBlockedHoles(ref gb, tempDrop.ArrayValue);//tato funkce je prepsana, protoze nas zajima detailnejsi vysledek
            int softBlocked = HardDropAI.checkSoftBlockedHoles(ref gb, tempDrop.ArrayValue, hardBlocked);
            int diff = HardDropAI.checkHeightDiff(ref gb, tempDrop.ArrayValue);
            int numLines = HardDropAI.checkFullLines(ref gb, tempDrop.ArrayValue);
            int hrbolatost = HardDropAI.deltaHrbolatosti(ref gb, tempDrop.ArrayValue);

            int tempScore = hardBlocked * 9 + softBlocked * 7 + diff * 5 - numLines * 3 + hrbolatost / 4;
            return tempScore;
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
                        HardDropAI.clearAfterBFS(deska, new int[2] { Pozice[i, 0] + posunuti, Pozice[i, 1] - 1 });
                    }
                    else
                    {
                        numOfHoles += tempHoles;
                    }
                }

                tempHoles = 0;
                q = new Queue();
                if (Pozice[i, 1] < 9 && deska[Pozice[i, 0] + posunuti, Pozice[i, 1] + 1] == '\0')
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
        //AI zahraje nasledujici tah a vrati true pokud je jeste co hrat
        static public bool PlayNextMove(ref GameBoard gb, Shape shp, string navigation, int stepNum)
        {
            if (stepNum == navigation.Length)
            {
                return false;//dosli jsme na konec navigace
            }
            else
            {
                switch (navigation[stepNum])
                {
                    case 'L':
                        return shp.MoveLeft(ref gb);
                    case 'R':
                        return shp.MoveRight(ref gb);
                    case 'D':
                        return shp.MoveDown(ref gb);
                    case 'G'://rotateriGht
                        return shp.RotRight(ref gb);
                    default:
                        return false;
                }
            }
        }
    }
}
