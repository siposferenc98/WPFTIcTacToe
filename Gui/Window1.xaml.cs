using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        static List<List<Button>> gombok = new List<List<Button>>();
        public Window1()
        {
            InitializeComponent();
            kovetkezik.Content = Table.p1Kovetkezik ? Table.p1 + " következik" : Table.p2 + " következik";
            var itemek = mygrid.Children;
            
            List<Button> temp = new List<Button>();
            foreach(var a in itemek)
            {
                if(a.GetType() == typeof(Button))
                {
                    temp.Add((Button)a);
                }
                if (temp.Count == 3)
                {
                    gombok.Add(new List<Button>(temp));
                    temp.Clear();
                }
            }
            
            gombok[2][2].SetValue(ContentProperty, 3);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button a = (Button)sender;
            int sor = Grid.GetRow(a);
            int oszl = Grid.GetColumn(a);
            
            char[,] mostaniTabla = Table.aktualisJatszma.Last();
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
                MessageBox.Show(Table.aktualisJatszma.Count().ToString());
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
            MessageBox.Show("mentesekcount= "+Table.mentesek.Last().tabla.Count().ToString());
            

            if (Table.mentesek.Count != 0)
            {
                foreach (mentettJatszmak a in Table.mentesek)
                {
                    if (!((MainWindow)Owner).mentesek.Items.Contains(a.getSorszam() + " . játszma"))
                        ((MainWindow)Owner).mentesek.Items.Add(a.getSorszam() +" . játszma");
                }
            }
            Owner.Show();
        }

        
    }
}
