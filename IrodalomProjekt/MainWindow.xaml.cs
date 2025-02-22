using System.IO;
using System.Text;
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
using System.Linq;
using System.Linq.Expressions;

namespace IrodalomProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Kerdes> kerdesek = new List<Kerdes>();
        private static int aktualisIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private static void KerdeseketFeltolt(string fileName)
        {
            kerdesek.Clear();

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(';');

                        if (parts.Length == 5) 
                        {
                            string kerdesSzoveg = parts[0];
                            string valaszA = parts[1];
                            string valaszB = parts[2];
                            string valaszC = parts[3];
                            string helyesValasz = parts[4];

                           
                            kerdesek.Add(new Kerdes(kerdesSzoveg, valaszA, valaszB, valaszC, helyesValasz));
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a fájl beolvasása közben: " + ex.Message);
            }
        }


        private void BetoltesClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT Fájlok (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    KerdeseketFeltolt(openFileDialog.FileName);

                    MessageBox.Show($"Sikeres betöltés!");

                    if (kerdesek.Count > 0)
                    {
                        aktualisIndex = 0;
                        MutatKerdes(aktualisIndex);
                    }
                    else
                    {
                        MessageBox.Show("Nincsenek betöltött kérdések!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void MutatKerdes(int index)
        {
            if (kerdesek.Count == 0 || index < 0 || index >= kerdesek.Count)
                return;

            Kerdes aktKerdes = kerdesek[index];

            tbkKerdesSzovege.Text = aktKerdes.KerdesSzoveg; 
            ValaszA.Content = "A) " + aktKerdes.ValaszA;
            ValaszB.Content = "B) " + aktKerdes.ValaszB;
            ValaszC.Content = "C) " + aktKerdes.ValaszC;

            ValaszA.IsChecked = false;
            ValaszB.IsChecked = false;
            ValaszC.IsChecked = false;

            if (aktKerdes.FelhasznaloValasza == aktKerdes.ValaszA) ValaszA.IsChecked = true;
            else if (aktKerdes.FelhasznaloValasza == aktKerdes.ValaszB) ValaszB.IsChecked = true;
            else if (aktKerdes.FelhasznaloValasza == aktKerdes.ValaszC) ValaszC.IsChecked = true;
        }
        private void KiertekelesClick(object sender, RoutedEventArgs e)
        {

        }

        private void KilepesClick(object sender, RoutedEventArgs e)
        {

        }

        private void ElozoClick(object sender, RoutedEventArgs e)
        {
            if (aktualisIndex > 0)
            {
                aktualisIndex--;
                MutatKerdes(aktualisIndex);
            }
        }

        private void KovetkezoClick(object sender, RoutedEventArgs e)
        {
            if (aktualisIndex < kerdesek.Count - 1)
            {
                aktualisIndex++;
                MutatKerdes(aktualisIndex);
            }
        }

        private void ValaszMenteseClick(object sender, RoutedEventArgs e)
        {

        }
    }
}