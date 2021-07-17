using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Gui
{

    class mentettLepesek
    {
        private int sorszam;
        private char[,] tabla;
        private bool p1Kov;
        public mentettLepesek(int s, char[,] t, bool p)
        {
            sorszam = s;
            tabla = t;
            p1Kov = p;
        }
        public int getSorszam()
        {
            return sorszam;
        }
        public char[,] getTabla()
        {
            return tabla;
        }
        public bool getP1()
        {
            return p1Kov;
        }

    }

    class mentettJatszmak
    {
        private int sorszam;
        private List<char[,]> tabla;
        private List<mentettLepesek> history;
        public mentettJatszmak(int s, List<char[,]> t, List<mentettLepesek> h)
        {
            sorszam = s;
            tabla = t;
            history = h;
        }

        public int getSorszam()
        {
            return sorszam;
        }
        public List<char[,]> getTabla()
        {
            return tabla;
        }
        public List<mentettLepesek> getHistory()
        {
            return history;
        }

    }

    //Window1
    public partial class Window1 : Window
    {

        private bool changed = false;
        private int aktualisTablaIndex = 0;
        
        public Window1()
        {
            InitializeComponent();
            kovetkezik.Content = Table.p1Kovetkezik ? Table.p1 + " következik" : Table.p2 + " következik";
            Table.historyList.Add(new mentettLepesek(0, Table.aktualisJatszma.First(), Table.p1Kovetkezik));
            feltoltes();
            historyAdd();

            if (Table.p1Kovetkezik == false && Table.gep)
                AI();
            
            

            
        }
        public void AI()
        {
            if(Table.ureshelyek(Table.aktualisJatszma.Last()) > 0)
            {
                Ellenfel_lepes geplepese = Minimax.legjobblepes(Table.aktualisJatszma.Last());
                char[,] temp = (char[,])Table.aktualisJatszma.Last().Clone();
                temp[geplepese.sor, geplepese.oszlop] = Table.p2;
                Table.aktualisJatszma.Add(temp);
                Table.historyList.Add(new mentettLepesek(Table.historyList.Count, temp, Table.p1Kovetkezik));
                feltoltes();
                Table.p1Kovetkezik = !Table.p1Kovetkezik;

                gameOver(temp);
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button a = (Button)sender;
            int sor = Grid.GetRow(a);
            int oszl = Grid.GetColumn(a);

            char[,] mostaniTabla = new char[3,3];
            if(changed == false)
                 mostaniTabla = (char[,])Table.aktualisJatszma.Last().Clone();
            else
                mostaniTabla = (char[,])Table.aktualisJatszma[aktualisTablaIndex].Clone();

            if(Table.ureshelyek(mostaniTabla) != 0 && Table.nyerolehetosegek(mostaniTabla) == 0)
            {
                if (mostaniTabla[sor - 1, oszl] == '_')
                {

                    if (Table.p1Kovetkezik)
                    {
                        a.Content = Table.p1;
                        mostaniTabla[sor - 1, oszl] = Table.p1;
                    }
                    else
                    {
                        a.Content = Table.p2;
                        mostaniTabla[sor - 1, oszl] = Table.p2;

                    }
                    if(changed)
                    {
                        Table.aktualisJatszma.RemoveRange(aktualisTablaIndex, Table.aktualisJatszma.Count() - aktualisTablaIndex);
                        Table.historyList.RemoveRange(aktualisTablaIndex, Table.historyList.Count() - aktualisTablaIndex);
                    }
                    Table.aktualisJatszma.Add(mostaniTabla);
                    Table.historyList.Add(new mentettLepesek(Table.aktualisJatszma.Count - 1, mostaniTabla, Table.p1Kovetkezik));
                    historyAdd();
                    changed = false;
                    kovetkezik.Content = Table.p1Kovetkezik ? Table.p2 + " következik" : Table.p1 + " következik";
                    Table.p1Kovetkezik = !Table.p1Kovetkezik;
                    if (Table.gep)
                    {
                        AI();
                        historyAdd();
                    }
                        
                        
                }
                else
                {
                    MessageBox.Show("Ez a cella már foglalt!", "Foglalt cella", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }
            gameOver(mostaniTabla);



        }

        private void Feltolt(List<List<Button>> gombok, int index = -1)
        {
            
            Dispatcher.Invoke(
                () =>
                {
                    for(int i = 0; i < 3; i++)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            if(index == -1)
                            {
                                gombok[i][j].Content = Table.aktualisJatszma.Last()[i, j];
                            }
                            else
                            {
                                gombok[i][j].Content = Table.aktualisJatszma[index][i, j];
                                if(index != 0)
                                    Table.p1Kovetkezik = !Table.historyList[index].getP1();
                            }
                            
                        }
                    } 
                }
                );

        }

        private void feltoltes(int index = -1)
        {
            
            Thread thread1 = new Thread(this.callback);
            thread1.IsBackground = true;
            thread1.Start(index);
        }

        private void callback(object index)
        {
            List<List<Button>> gombok = new List<List<Button>>() { new List<Button>() { EE, EK, EH }, new List<Button>() { KE, KK, KH }, new List<Button>() { HE, HK, HH } };
            try
            {
                Feltolt(gombok,(int)index);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
                
        }


        private void historyBetolt(object sender, SelectionChangedEventArgs e)
        {
            feltoltes(history.SelectedIndex);
            if (history.SelectedIndex != Table.aktualisJatszma.IndexOf(Table.aktualisJatszma.Last()))
            {
                aktualisTablaIndex = history.SelectedIndex;
                changed = true;
            }
            else
                changed = false;
        }

        private void historyAdd()
        {
            history.Items.Clear();
            if(Table.historyList.Count != 0)
            {
                foreach(mentettLepesek a in Table.historyList)
                {
                    if (!history.Items.Contains(a.getSorszam() + ". lépés"))
                        history.Items.Add(a.getSorszam() + ". lépés");
                }
            }
        }

        private void gameOver(char[,] t)
        {
            if (Table.nyerolehetosegek(t) == 1)
                MessageBox.Show(Table.p1 + " nyert!");
            else if (Table.nyerolehetosegek(t) == -1)
                MessageBox.Show(Table.p2 + " nyert!");
            else if (Table.ureshelyek(t) == 0)
                MessageBox.Show("Döntetlen!");
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            mentettJatszmak aktualis = new mentettJatszmak(Table.mentesek.Count + 1, new List<char[,]>(Table.aktualisJatszma), new List<mentettLepesek>(Table.historyList));
            Table.mentesek.Add(aktualis);

            Table.aktualisJatszma.Clear();
            Table.historyList.Clear();


            if (Table.mentesek.Count != 0)
            {
                foreach (mentettJatszmak a in Table.mentesek)
                {
                    if (!((MainWindow)Owner).mentesek.Items.Contains(a.getSorszam() + " . játszma"))
                        ((MainWindow)Owner).mentesek.Items.Add(a.getSorszam() + " . játszma");
                }
            }
            Table.defaultTabla = true;

            Owner.Show();
        }

    }
}
