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

namespace kalkulator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<string> historia = new List<string>();
        private List<Double> wartosci = new List<double>();
        private string dzialanie = "";

        private string dlugosc_ekranu(string s, string liczba)
        {
            if (s.Length == 1 && s == "0")
                return liczba;
            else if (dzialanie == "=" && s[0] == '0' && s[1] == ',')
            {
                return s + liczba;
            }
            else if (dzialanie == "=" && s.Length > 0)
            {
                Screen_tb.Text = "";
                dzialanie = "";
                return liczba;
            }
            else
                return s + liczba;
        }

        private void dodanieOperatora(string d, string s)
        {
            try
            {
                try
                {
                    wartosci.Add(Convert.ToDouble(s));
                }
                catch
                {
                    s.Replace(",", ".");
                    wartosci.Add(Convert.ToDouble(s));
                }
                if(wartosci.Count > 0 && dzialanie != "" && s.Length > 0)
                {
                    double w2;
                    try
                    {
                        w2 = Convert.ToDouble(s);
                    }
                    catch
                    {
                        s.Replace(",", ".");
                        w2 = Convert.ToDouble(s);
                    }
                    switch (dzialanie)
                    {
                        case "+":
                            wartosci[0] += Math.Round(w2);
                            break;
                        case "-":
                            wartosci[0] -= Math.Round(w2);
                            break;
                        case "*":
                            wartosci[0] *= Math.Round(w2);
                            break;
                        case "/":
                            if (w2 == 0)
                            {
                                MessageBox.Show("Nie dzieli się przez zero");
                            }
                            else
                                wartosci[0] /= Math.Round(w2);
                            break;
                    }

                    dzialanie = d;
                    miniEkran_tb.Text = " " + wartosci[0].ToString().Replace(".",",") + " " + d;
                    Screen_tb.Text = "";
                    wartosci.Remove(wartosci[1]);

                }
                else
                {
                    dzialanie = d;
                    miniEkran_tb.Text = " " + wartosci[0].ToString().Replace(".", ",") + " " + d;
                    Screen_tb.Text = "";
                }
               
            }
            catch
            {
                if (wartosci.Count > 0)
                {
                    string stare_dzialanie = dzialanie;
                    dzialanie = d;
                    string mini_ekran = miniEkran_tb.Text;
                    mini_ekran = mini_ekran.Replace(stare_dzialanie, dzialanie);
                    miniEkran_tb.Text = mini_ekran;
                }
            }
        }

        private void wynikDzialania()
        {
            double wynik;
            switch (dzialanie)
            {
                case "+":
                    wynik = wartosci[0] + wartosci[1];
                    Screen_tb.Text = (Math.Round(wynik,2)).ToString().Replace(".", ",");
                    if(wartosci[1] < 0)
                    {
                        miniEkran_tb.Text = wartosci[0].ToString() + " " + wartosci[1].ToString() + " = ";
                        historia.Add(wartosci[0] + " - " + wartosci[1] + " = " + Math.Round(wynik, 2));
                    } 
                    else
                    {
                        miniEkran_tb.Text += " " + wartosci[1].ToString() + " = ";
                        historia.Add(wartosci[0] + " + " + wartosci[1] + " = " + Math.Round(wynik, 2));
                    }
                        
                    break;
                case "-":
                    wynik = wartosci[0] - wartosci[1];
                    Screen_tb.Text = (Math.Round(wynik, 2)).ToString();
                    if (wartosci[1] < 0)
                    {
                        miniEkran_tb.Text = wartosci[0].ToString() + " " + wartosci[1].ToString() + " = ";
                        historia.Add(wartosci[0] + " + " + wartosci[1] + " = " + Math.Round(wynik, 2));
                    }
                    else
                    {
                        miniEkran_tb.Text += " " + wartosci[1].ToString() + " = ";
                        historia.Add(wartosci[0] + " - " + wartosci[1] + " = " + Math.Round(wynik, 2));
                    }
                        
                    break;
                case "*":
                    wynik = wartosci[0] * wartosci[1];
                    Screen_tb.Text = (Math.Round(wynik, 2)).ToString().Replace(".", ",");
                    if (wartosci[1] < 0)
                    {
                        miniEkran_tb.Text = wartosci[0].ToString() + " * (" + wartosci[1].ToString() + ") = ";
                        historia.Add(wartosci[0] + " * (" + wartosci[1] + ") = " + Math.Round(wynik, 2));
                    }                        
                    else
                    {
                        miniEkran_tb.Text += " " + wartosci[1].ToString() + " = ";
                        historia.Add(wartosci[0] + " * " + wartosci[1] + " = " + Math.Round(wynik, 2));
                    }
                        
                    break;
                case "/":
                    if (wartosci[1] == 0)
                    {
                        MessageBox.Show("Nie dzieli się przez zero");
                    }
                    else
                    {
                        wynik = wartosci[0] / wartosci[1];
                        Screen_tb.Text = Math.Round(wynik, 2).ToString().Replace(".", ",");
                        if (wartosci[1] < 0)
                        {
                            miniEkran_tb.Text = wartosci[0].ToString() + " / (" + wartosci[1].ToString() + ") = ";
                            historia.Add(wartosci[0] + " / (" + wartosci[1] + ") = " + Math.Round(wynik, 2));
                        }

                        else
                        {
                            miniEkran_tb.Text += " " + wartosci[1].ToString() + " = ";
                            historia.Add(wartosci[0] + " / " + wartosci[1] + " = " + Math.Round(wynik, 2));
                        }
                    }
                        
                    break;
            }
            
            wartosci.Clear();
            dzialanie = "=";
            HistoriaDzialan_lb.Items.Refresh();
        }
        public MainWindow()
        {
            InitializeComponent();
            HistoriaDzialan_lb.ItemsSource = historia;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            string screen = Screen_tb.Text.ToString();
            string maly_ekran = miniEkran_tb.Text.ToString();
            switch (e.Key)
            {
                case Key.D0:
                    Screen_tb.Text = dlugosc_ekranu(screen, "0");
                    break;
                case Key.D1:
                    Screen_tb.Text = dlugosc_ekranu(screen, "1");
                    break;
                case Key.D2:
                    Screen_tb.Text = dlugosc_ekranu(screen, "2");
                    break;
                case Key.D3:
                    Screen_tb.Text = dlugosc_ekranu(screen, "3");
                    break;
                case Key.D4:
                    Screen_tb.Text = dlugosc_ekranu(screen, "4");
                    break;
                case Key.D5:
                    Screen_tb.Text = dlugosc_ekranu(screen, "5");
                    break;
                case Key.D6:
                    Screen_tb.Text = dlugosc_ekranu(screen, "6");
                    break;
                case Key.D7:
                    Screen_tb.Text = dlugosc_ekranu(screen, "7");
                    break;
                case Key.D8:
                    Screen_tb.Text = dlugosc_ekranu(screen, "8");
                    break;
                case Key.D9:
                    Screen_tb.Text = dlugosc_ekranu(screen, "9");
                    break;
                case Key.Back:
                    if (screen.Length > 0)
                    {
                        screen = screen.Remove(screen.Length - 1);
                        Screen_tb.Text = screen;
                    }
                    else
                    {
                        miniEkran_tb.Text = "";
                        dzialanie = "";
                        wartosci.Clear();
                    }
                    break;
                case Key.NumPad0:
                    Screen_tb.Text = dlugosc_ekranu(screen, "0");
                    break;
                case Key.NumPad1:
                    Screen_tb.Text = dlugosc_ekranu(screen, "1");
                    break;
                case Key.NumPad2:
                    Screen_tb.Text = dlugosc_ekranu(screen, "2");
                    break;
                case Key.NumPad3:
                    Screen_tb.Text = dlugosc_ekranu(screen, "3");
                    break;
                case Key.NumPad4:
                    Screen_tb.Text = dlugosc_ekranu(screen, "4");
                    break;
                case Key.NumPad5:
                    Screen_tb.Text = dlugosc_ekranu(screen, "5");
                    break;
                case Key.NumPad6:
                    Screen_tb.Text = dlugosc_ekranu(screen, "6");
                    break;
                case Key.NumPad7:
                    Screen_tb.Text = dlugosc_ekranu(screen, "7");
                    break;
                case Key.NumPad8:
                    Screen_tb.Text = dlugosc_ekranu(screen, "8");
                    break;
                case Key.NumPad9:
                    Screen_tb.Text = dlugosc_ekranu(screen, "9");
                    break;
                case Key.OemPlus:
                    dodanieOperatora("+", screen);
                    break;
                case Key.Subtract:
                    dodanieOperatora("-", screen);
                    break;
                case Key.OemMinus:
                    dodanieOperatora("-", screen);
                    break;
                case Key.OemQuestion:
                    dodanieOperatora("/", screen);
                    break;
                case Key.Multiply:
                    dodanieOperatora("*", screen);
                    break;
                case Key.Divide:
                    dodanieOperatora("/", screen);
                    break;
                case Key.Return:
                    if(screen.Length > 0)
                    {
                        try
                        {
                            wartosci.Add(Convert.ToDouble(screen));
                        }
                        catch
                        {
                            screen.Replace(",", ".");
                            wartosci.Add(Convert.ToDouble(screen));
                        }
                        wynikDzialania();
                    }
                 
                    break;
                case Key.OemComma:
                    if (screen.Length == 0)
                        Screen_tb.Text = "0,";
                    else if(screen[screen.Length-1] == ',')
                    { }
                    else if(screen.Contains(","))
                    { }
                    else if (dzialanie == "=")
                    {
                        Screen_tb.Text = "0,";
                    }
                    else
                        Screen_tb.Text = screen + ",";
                    break;
                case Key.OemPeriod:
                    if (screen.Length == 0)
                        Screen_tb.Text = "0,";
                    else if (screen[screen.Length - 1] == ',')
                    { }
                    else if (screen.Contains(","))
                    { }
                    else if(dzialanie == "=")
                    {
                        Screen_tb.Text = "0,";
                    }
                    else
                        Screen_tb.Text = screen + ",";
                    break;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string screen = Screen_tb.Text.ToString();
            string maly_ekran = miniEkran_tb.Text.ToString();
            string nazwa_przycisku = ((Button)sender).Name;

            switch (nazwa_przycisku)
            {
                case "l0":
                    Screen_tb.Text = dlugosc_ekranu(screen, "0");
                    break;
                case "l1":
                    Screen_tb.Text = dlugosc_ekranu(screen, "1");
                    break;
                case "l2":
                    Screen_tb.Text = dlugosc_ekranu(screen, "2");
                    break;
                case "l3":
                    Screen_tb.Text = dlugosc_ekranu(screen, "3");
                    break;
                case "l4":
                    Screen_tb.Text = dlugosc_ekranu(screen, "4");
                    break;
                case "l5":
                    Screen_tb.Text = dlugosc_ekranu(screen, "5");
                    break;
                case "l6":
                    Screen_tb.Text = dlugosc_ekranu(screen, "6");
                    break;
                case "l7":
                    Screen_tb.Text = dlugosc_ekranu(screen, "7");
                    break;
                case "l8":
                    Screen_tb.Text = dlugosc_ekranu(screen, "8");
                    break;
                case "l9":
                    Screen_tb.Text = dlugosc_ekranu(screen, "9");
                    break;
                case "ce":
                    Screen_tb.Text = "";
                    break;
                case "c":
                    Screen_tb.Text = "";
                    miniEkran_tb.Text = "";
                    wartosci.Clear();
                    dzialanie = "";
                    break;
                case "pm":
                    if (screen.Length > 0)
                    {
                        double liczba;
                        try
                        {
                            liczba = (Convert.ToDouble(screen)) * (-1);
                        }
                        catch
                        {
                            screen.Replace(",", ".");
                            liczba = (Convert.ToDouble(screen)) * (-1);
                        }
                        
                        Screen_tb.Text = liczba.ToString();
                    }
                    break;
                case "przecinek":
                    if (screen.Length == 0)
                        Screen_tb.Text = "0,";
                    else if (screen[screen.Length - 1] == ',')
                    { }
                    else if (screen.Contains(","))
                    { }
                    else if (dzialanie == "=")
                    {
                        Screen_tb.Text = "0,";
                    }
                    else
                        Screen_tb.Text = screen + ",";
                    break;
                case "usun":
                    if(screen.Length > 0)
                    {
                        screen = screen.Remove(screen.Length - 1);
                        Screen_tb.Text = screen;
                    }
                    break;
                case "dodawanie":
                    dodanieOperatora("+", screen);
                    break;
                case "odejmowanie":
                    dodanieOperatora("-", screen);
                    break;
                case "mnozenie":
                    dodanieOperatora("*", screen);
                    break;
                case "dzielenie":
                    dodanieOperatora("/", screen);
                    break;
                case "wynik":
                    if(screen.Length > 0)
                    {
                        try
                        {
                            wartosci.Add(Convert.ToDouble(screen));
                        }
                        catch
                        {
                            screen.Replace(",", ".");
                            wartosci.Add(Convert.ToDouble(screen));
                        }
                        wynikDzialania();
                    }
                    
                    break;
            }
            
        }

        private void WczytanieHistorii_lb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string wybrany_el = HistoriaDzialan_lb.SelectedItem.ToString();
            string[] tablica_elementow = wybrany_el.Split(' ');
            if(tablica_elementow.Length == 4)
            {
                wartosci.Clear();
                dzialanie = "=";
                Screen_tb.Text = tablica_elementow[3];
            }
            else
            {
                wartosci.Clear();
                dzialanie = "=";
                Screen_tb.Text = tablica_elementow[4];
            }
            miniEkran_tb.Text = wybrany_el.Substring(0, (wybrany_el.Length - (wybrany_el.Length-wybrany_el.IndexOf("=")-2)));
        }
    }
}
