using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private const int memDim = 4;
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
        public void displayTable()
        {
            for(int i = 0; i < memDim; i++){
                for(int j = 0; j < memDim; j++){
                    System.Console.Write(gameArray[i, j].ToString() + " ");
                }
                System.Console.WriteLine();
            }
        }
    }
}
