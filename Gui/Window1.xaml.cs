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
    class mentettJatszmak
    {
        public int sorszam;
        public List<char[,]> tabla;
        public mentettJatszmak(int s, List<char[,]> t)
        {
            sorszam = s;
            tabla = t;
        }

        public int getSorszam()
        {
            return sorszam;
        }
        public List<char[,]> getTabla()
        {
            return tabla;
        }

    }


    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        
        public Window1()
        {
            InitializeComponent();
            kovetkezik.Content = Table.p1Kovetkezik ? Table.p1 + " következik" : Table.p2 + " következik";
            feltoltes();

        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button a = (Button)sender;
            int sor = Grid.GetRow(a);
            int oszl = Grid.GetColumn(a);

            char[,] mostaniTabla = (char[,])Table.aktualisJatszma.Last().Clone();
            if (mostaniTabla[sor-1,oszl] == '_')
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
                Table.aktualisJatszma.Add(mostaniTabla);
                history.Items.Add(Table.aktualisJatszma.Last());
                kovetkezik.Content = Table.p1Kovetkezik ? Table.p2 + " következik" : Table.p1 + " következik";
                Table.p1Kovetkezik = !Table.p1Kovetkezik;
            }
            else
            {
                MessageBox.Show("Ez a cella már foglalt!", "Foglalt cella", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            mentettJatszmak aktualis = new mentettJatszmak(Table.mentesek.Count + 1, new List<char[,]>(Table.aktualisJatszma));
            Table.mentesek.Add(aktualis);
            
            Table.aktualisJatszma.Clear();
            
            

            if (Table.mentesek.Count != 0)
            {
                foreach (mentettJatszmak a in Table.mentesek)
                {
                    if (!((MainWindow)Owner).mentesek.Items.Contains(a.getSorszam() + " . játszma"))
                        ((MainWindow)Owner).mentesek.Items.Add(a.getSorszam() +" . játszma");
                }
            }
            Table.defaultTabla = true;
            
            Owner.Show();
        }

        private void Feltolt(List<List<Button>> gombok)
        {
            
            Dispatcher.Invoke(
                () =>
                {
                    for(int i = 0; i < 3; i++)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            gombok[i][j].Content = Table.aktualisJatszma.Last()[i, j];
                        }
                    } 
                }
                );

        }

        private void feltoltes()
        {
            
            Thread thread1 = new Thread(callback);
            thread1.IsBackground = true;
            thread1.Start();
        }

        private void callback()
        {
            List<List<Button>> gombok = new List<List<Button>>() { new List<Button>() { EE, EK, EH }, new List<Button>() { KE, KK, KH }, new List<Button>() { HE, HK, HH } };
            try
            {
                Feltolt(gombok);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
                
        }

        private void historyBetolt(object sender, RoutedEventArgs e)
        {

        }

        private void historyBetolt(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
