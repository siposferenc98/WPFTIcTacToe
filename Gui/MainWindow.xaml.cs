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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            InitializeComponent();
            xButton.IsChecked = true;
            elso.IsChecked = true;
            jatekos.IsChecked = true;
            
        }

        private void mentesBetolt(object sender, RoutedEventArgs e)
        {

            try
            {
                int index = mentesek.SelectedIndex;
                Table.aktualisJatszma = Table.mentesek[index].getTabla();
                foreach (var a in Table.aktualisJatszma)
                {
                    Table.historyList.Add(new mentettLepesek(Table.aktualisJatszma.IndexOf(a), a));
                }
                Table.defaultTabla = false;
                MessageBox.Show(Table.aktualisJatszma.Count().ToString());

            }
            catch (Exception)
            {
                MessageBox.Show("Nincs kiválasztva mentés!");
            }
        }


        private void NextWindow(object sender, RoutedEventArgs e)
        {
            if ((bool)xButton.IsChecked)
            {   
                Table.p1 = 'X';
                Table.p2 = 'O';
            }
            else
            {
               Table.p1 = 'O';
               Table.p2 = 'X';
            }
            if ((bool)gep.IsChecked)
                Table.gep = true;
            else
                Table.gep = false;

            if ((bool)elso.IsChecked)
                Table.p1Kovetkezik = true;
            else
                Table.p1Kovetkezik = false;

            if (Table.defaultTabla)
            {
                char[,] tabla =
                {
                {'_','_','_'},
                {'_','_','_'},
                {'_','_','_'},
                };
                Table.aktualisJatszma.Add(tabla);
            }
            
               
            Window window = new Window1();
            window.Owner = this;
            window.Show();
            Hide();
        }

        
    }
}
