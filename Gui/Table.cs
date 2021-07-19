using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui
{
    class Table
    {
        public static char p1,p2;
        public static bool p1Kovetkezik;
        public static List<mentettJatszmak> mentesek = new List<mentettJatszmak>();
        public static List<char[,]> aktualisJatszma = new List<char[,]>();
        public static List<mentettLepesek> historyList = new List<mentettLepesek>();
        public static bool defaultTabla = true;
        public static bool gep = false;
        public static bool fileBetolt = false;

        public static int nyerolehetosegek(char[,] tabla)
        {
            //sorwin
            for (int sorba = 0; sorba < 3; sorba++)
            {
                if (tabla[sorba, 0] == tabla[sorba, 1] && tabla[sorba, 1] == tabla[sorba, 2])
                {
                    if (tabla[sorba, 0] == p1)
                        return 1;
                    else if (tabla[sorba, 0] == p2)
                        return -1;
                }
            }
            //oszlopwin
            for (int oszlopba = 0; oszlopba < 3; oszlopba++)
            {
                if (tabla[0, oszlopba] == tabla[1, oszlopba] && tabla[1, oszlopba] == tabla[2, oszlopba])
                {
                    if (tabla[0, oszlopba] == p1)
                        return 1;
                    else if (tabla[0, oszlopba] == p2)
                        return -1;
                }
            }
            //átló
            if (tabla[0, 0] == tabla[1, 1] && tabla[1, 1] == tabla[2, 2])
            {
                if (tabla[0, 0] == p1)
                    return 1;
                else if (tabla[0, 0] == p2)
                    return -1;
            }

            if (tabla[0, 2] == tabla[1, 1] && tabla[1, 1] == tabla[2, 0])
            {
                if (tabla[0, 2] == p1)
                    return 1;
                else if (tabla[0, 2] == p2)
                    return -1;
            }

            return 0;
        }

        public static int ureshelyek(char[,] t)
        {
            int darab = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (t[i, j] == '_')
                    {
                        darab++;
                    }
                }
            }
            return darab;
        }

        

    }



}
    



