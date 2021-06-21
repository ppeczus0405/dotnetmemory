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

namespace Memory
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this);
        }
        private void exitClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void playClick(object sender, RoutedEventArgs e)
        {
            game.displayTable();
        }
        private void mem00Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem01Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem02Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem03Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem10Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem11Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem12Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem13Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem20Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem21Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem22Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem23Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem30Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem31Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem32Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
        private void mem33Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            System.Console.WriteLine(s.Name);
        }
    }
}