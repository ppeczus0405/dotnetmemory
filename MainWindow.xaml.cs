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
    public partial class MainWindow : Window
    {
        Game game;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this);
        }
        private void exitClick(object sender, RoutedEventArgs e) => game.endGame();
        private void playClick(object sender, RoutedEventArgs e) => game.restartGame();
        private void mem00Click(object sender, RoutedEventArgs e) => game.updateButton(0, 0);
        private void mem01Click(object sender, RoutedEventArgs e) => game.updateButton(0, 1);
        private void mem02Click(object sender, RoutedEventArgs e) => game.updateButton(0, 2);
        private void mem03Click(object sender, RoutedEventArgs e) => game.updateButton(0, 3);
        private void mem10Click(object sender, RoutedEventArgs e) => game.updateButton(1, 0);
        private void mem11Click(object sender, RoutedEventArgs e) => game.updateButton(1, 1);
        private void mem12Click(object sender, RoutedEventArgs e) => game.updateButton(1, 2);
        private void mem13Click(object sender, RoutedEventArgs e) => game.updateButton(1, 3);
        private void mem20Click(object sender, RoutedEventArgs e) => game.updateButton(2, 0);
        private void mem21Click(object sender, RoutedEventArgs e) => game.updateButton(2, 1);
        private void mem22Click(object sender, RoutedEventArgs e) => game.updateButton(2, 2);
        private void mem23Click(object sender, RoutedEventArgs e) => game.updateButton(2, 3);
        private void mem30Click(object sender, RoutedEventArgs e) => game.updateButton(3, 0);
        private void mem31Click(object sender, RoutedEventArgs e) => game.updateButton(3, 1);
        private void mem32Click(object sender, RoutedEventArgs e) => game.updateButton(3, 2);
        private void mem33Click(object sender, RoutedEventArgs e) => game.updateButton(3, 3);
    }
}