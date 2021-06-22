using System;
using System.Diagnostics;
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
    public static class MyExtensions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public class Game
    {
        private const string resourcePath = "pack://application:,,,/Memory;component/resources/";
        private const string questionMark = "qmark.png";
        private const string memButtonPrefix = "mem";
        private const int memDim = 4;
        private readonly Color normalMemButtonColor = Color.FromRgb(0xEA, 0xEA, 0xEA);
        private readonly Color pressedMemButtonColor = Color.FromRgb(0xFF, 0xFF, 0xFF);
        private int[,] gameArray;
        private int pressedFields;
        MainWindow mWindow;

        public Game(MainWindow w)
        {
            mWindow = w;
            pressedFields = 0;
            gameArray = new int[memDim, memDim];
            fillGameArray();
        }
        private void succesMatch(Tuple<int, int> f, Tuple<int, int> s)
        {

        }
        private List<int> getRandom()
        {
            var result = new List<int>(memDim * memDim);
            for(int i = 0; i < memDim * memDim; i++){
                result.Add(i % (memDim * memDim) / 2);
            }
            result.Shuffle();
            return result;
        }
        private void fillGameArray()
        {
            List<int> randomList = getRandom();
            for(int i = 0; i < memDim; i++){
                for(int j = 0; j < memDim; j++){
                    gameArray[i,j] = randomList[i * memDim + j];
                }
            }
        }
        private void fieldChangeToPressed(Tuple<int, int> field, bool pressed)
        {
            string fieldName = memButtonPrefix + field.Item1.ToString() + field.Item2.ToString();
            object wantedNode = mWindow.mainStackPanel.FindName(fieldName);
            if(wantedNode is Button)
            {
                // Border Background
                var parentBorder = (Border)VisualTreeHelper.GetParent((DependencyObject)wantedNode);
                parentBorder.Background = new SolidColorBrush(pressed ? pressedMemButtonColor : normalMemButtonColor);
                // Button image
                Button update = (Button)wantedNode;
                string imgName = pressed ? gameArray[field.Item1, field.Item2].ToString() + ".png" : questionMark;
                string imgPath = resourcePath + imgName;
                var imgBrush = new ImageBrush(new BitmapImage(new Uri(imgPath)));
                imgBrush.Stretch = Stretch.None;
                string styleName = pressed ? "MemPressedTheme" : "ButtonTheme";
                update.Background = imgBrush;
                update.Style = (Style)App.Current.Resources[styleName];
            }
        }
        public void displayTable()
        {
            for(int i = 0; i < memDim; i++){
                for(int j = 0; j < memDim; j++){
                    System.Console.Write(gameArray[i, j].ToString() + " ");
                }
                System.Console.WriteLine();
            }
        }
        public void updateButton(int i, int j)
        {
            pressedFields++;
            if (pressedFields <= 16)
            {
                fieldChangeToPressed(Tuple.Create(i, j), true);
            }
            else
            {
                fieldChangeToPressed(Tuple.Create(i, j), false);
            }
        }
    }
}
