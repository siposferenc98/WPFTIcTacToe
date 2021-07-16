using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui
{
    public class Ellenfel_lepes
    {
        public int sor, oszlop, melyseg;
    }
    class Minimax
    {
        
        public static (int, int) minimax(char[,] t, int melyseg, bool max)
        {

            int eredmeny = Table.nyerolehetosegek(t);
            if (eredmeny == 1)
                return (eredmeny, melyseg);
            if (eredmeny == -1)
                return (eredmeny, melyseg);
            if (Table.ureshelyek(t) == 0)
                return (0, melyseg);

            if (max)
            {
                int legjobb = -100;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (t[i, j] == '_')
                        {
                            t[i, j] = Table.p1;

                            legjobb = Math.Max(legjobb, minimax(t, melyseg + 1, !max).Item1);

                            t[i, j] = '_';
                        }
                    }
                }

                return (legjobb, melyseg);
            }
            else
            {
                int legjobb = 100;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (t[i, j] == '_')
                        {
                            t[i, j] = Table.p2;

                            legjobb = Math.Min(legjobb, minimax(t, melyseg + 1, !max).Item1);

                            t[i, j] = '_';
                        }
                    }
                }

                return (legjobb, melyseg);
            }
        }

        public static Ellenfel_lepes legjobblepes(char[,] t)
        {
            int legjobbertek = 10000;
            Ellenfel_lepes legjobb = new Ellenfel_lepes();
            legjobb.sor = -1;
            legjobb.oszlop = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (t[i, j] == '_')
                    {
                        t[i, j] = Table.p2;
                        (int, int) eredmeny = minimax(t, 0, true);
                        int lepesertek = eredmeny.Item1;
                        int melyseg = eredmeny.Item2;
                        //Console.WriteLine("lépésérték"+lepesertek);

                        t[i, j] = '_';


                        if (lepesertek < legjobbertek)
                        {
                            legjobb.melyseg = melyseg;
                            legjobb.sor = i;
                            legjobb.oszlop = j;
                            legjobbertek = lepesertek;

                        }
                        else if (lepesertek == legjobbertek)
                        {
                            if (melyseg < legjobb.melyseg)
                            {
                                legjobb.melyseg = melyseg;
                                legjobb.sor = i;
                                legjobb.oszlop = j;
                                legjobbertek = lepesertek;
                            }
                        }
                    }
                }
            }
            //Console.WriteLine("Legjobbertek = "+legjobbertek);

            return legjobb;
        }

        
    }
}
