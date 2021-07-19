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
using Microsoft.Win32;
using System.IO;

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
                Table.historyList = Table.mentesek[index].getHistory();
                /*foreach (var a in Table.aktualisJatszma)
                {
                    Table.historyList.Add(new mentettLepesek(Table.aktualisJatszma.IndexOf(a), a, Table.p1Kovetkezik));
                }*/
                Table.defaultTabla = false;
                MessageBox.Show("Mentés betöltve!","Betöltve!",MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("Nincs kiválasztva mentés!");
            }
        }

        private void torol(object sender, RoutedEventArgs e)
        {
            try
            {
                Table.mentesek.RemoveAt(mentesek.SelectedIndex);
                mentesek.Items.RemoveAt(mentesek.SelectedIndex);
            }
            catch(Exception)
            {
                MessageBox.Show("Nincs kiválasztva mentés!");
            }
            
        }
        private void torolall(object sender, RoutedEventArgs e)
        {
            Table.mentesek.Clear();
            mentesek.Items.Clear();
        }


        private void NextWindow(object sender, RoutedEventArgs e)
        {
            char[,] tabla =
                {
                {'_','_','_'},
                {'_','_','_'},
                {'_','_','_'},
                };

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
                Table.aktualisJatszma.Add(tabla);
            else
            {
                if(Table.fileBetolt == false)
                {
                    try
                    {
                        Table.mentesek.RemoveAt(mentesek.SelectedIndex);
                    }
                    catch (Exception)
                    {
                        Table.aktualisJatszma.Clear();
                        Table.aktualisJatszma.Add(tabla);
                        Table.historyList.Clear();
                    }
                }    
            }
               
            
            
               
            Window window = new Window1();
            window.Owner = this;
            window.Show();
            Hide();
        }

        private void betolt(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A beolvasáshoz szükséges egy .txt fájl, space-el elválasztva 3x3-as táblákkal( X , O , _ ). Minden tábla után egy üres sort kihagyva lehet több lépést is csinálni.", "Formátum",MessageBoxButton.OK,MessageBoxImage.Exclamation);

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "txt fájlok (*.txt)|*.txt";
            if(open.ShowDialog() == true)
            {
                StreamReader sr = new StreamReader(open.FileName);
                while(!sr.EndOfStream)
                {
                    char[,] temp = new char[3, 3];
                    for(int i = 0; i < 3; i++)
                    {
                        List<string> sor = sr.ReadLine().Split(' ').ToList();
                        temp[i, 0] = Convert.ToChar(sor[0].ToUpper());
                        temp[i, 1] = Convert.ToChar(sor[1].ToUpper());
                        temp[i, 2] = Convert.ToChar(sor[2].ToUpper());

                    }
                    Table.aktualisJatszma.Add(temp);
                    sr.ReadLine();
                }
                foreach (var a in Table.aktualisJatszma)
                {
                    Table.historyList.Add(new mentettLepesek(Table.aktualisJatszma.IndexOf(a), a, Table.p1Kovetkezik));
                }
                Table.defaultTabla = false;
                Table.fileBetolt = true;
                MessageBox.Show("Fájl beolvasás sikeres!","",MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void help(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("-A lenti dobozba mentett játékok fognak kerülni, egy játék bezárásánál automatikusan hozzá fog adódni.\n\n"+
                "-A főablakban a fenti file menüben betölteni tudsz .txt formátumú játékokat, bővebb leírást előtte kapni fogsz, ezután az Indít gomb ezt a játékot fogja elindítani.\n\n" + 
                "-Egy új játék indítása után a fenti file menüből le tudod menteni az aktuális játékodat, minden lépésével együtt(amit később be tudsz velük együtt tölteni).\n\n" + "-Játék közben az ablak jobb oldalán tárolva lesznek az előző lépések, ha rákattintasz egy-egy lépésre meg tudod nézni, és ha esetleg módosítod , akkor az azt követő lépések törlésre kerülnek.\n\n" + "-Ha a gép ellen játszol akkor az ő lépéseit nem tudod módosítani!");
        }
    }
}
