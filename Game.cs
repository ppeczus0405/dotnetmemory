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
        public static List<int> getRandomList(int dim)
        {
            int dimSquare = dim * dim;
            var result = new List<int>(dimSquare);
            for (int i = 0; i < dimSquare; i++){
                result.Add(i % (dimSquare / 2));
            }
            result.Shuffle();
            return result;
        }
    }

    public class Game
    {    
        // Readonly, const values
        private const string resourcePath = "pack://application:,,,/Memory;component/resources/";
        private const string mistakesText = "Mistakes: ";
        private const string questionMark = "qmark.png";
        private const string memButtonPrefix = "mem";
        private const int memDim = 4;
        private const int waitTime = 2000;
        private readonly Color normalMemButtonColor = Color.FromRgb(0xEA, 0xEA, 0xEA);
        private readonly Color pressedMemButtonColor = Color.FromRgb(0xFF, 0xFF, 0xFF);
        
        // Additional types declaration
        enum GameState {StartGame, RightMatch, WrongMatch, EndGame}

        // Game state variables

        private bool restartButtonActivated;
        private int[,] gameArray;
        private int mistakes;
        private int matchedPairs;
        private List <Tuple<int, int> > pressedFields;
        private GameState actualState;
        private MainWindow mWindow;
        private Dictionary<GameState, Tuple<string, Color>> stateDict;

        public Game(MainWindow w)
        {
            mWindow = w;
            gameArray = new int[memDim, memDim];
            pressedFields = new List<Tuple<int, int>>();
            actualState = GameState.StartGame;
            mistakes = 0;
            matchedPairs = 0;
            restartButtonActivated = false;
            fillGameArray();
            initializeStateDict();
        }
        private void fillGameArray()
        {
            List<int> randomList = MyExtensions.getRandomList(memDim);
            for (int i = 0; i < memDim; i++) { 
                for (int j = 0; j < memDim; j++) {
                    gameArray[i, j] = randomList[i * memDim + j];
                }
            }
        }
        private void initializeStateDict()
        {
            stateDict = new Dictionary<GameState, Tuple<string, Color>>();
            stateDict.Add(GameState.StartGame, Tuple.Create("Game starts! Stay focused.", Color.FromRgb(0xFF, 0xFF, 0xCC)));
            stateDict.Add(GameState.RightMatch, Tuple.Create("Succesfully matched!", Color.FromRgb(0x99, 0xFF, 0x66)));
            stateDict.Add(GameState.WrongMatch, Tuple.Create("Wrong matched. Don't give up!", Color.FromRgb(0xFF, 0x66, 0x00)));
            stateDict.Add(GameState.EndGame, Tuple.Create("Nice! You guessed all cards.", Color.FromRgb(0xCC, 0xFF, 0xFF)));
        }
        private Button getButton(Tuple<int, int> field)
        {
            string fieldName = memButtonPrefix + field.Item1.ToString() + field.Item2.ToString();
            return (Button)mWindow.mainStackPanel.FindName(fieldName);
        }
        private bool fieldChangeToPressed(Tuple<int, int> field, bool pressed)
        {
            if (!restartButtonActivated && pressed && (pressedFields.Count > 1 || pressedFields.Contains(field)))
                return false;
            
            Button wantedButton = getButton(field);
            // Border Background
            var parentBorder = (Border)VisualTreeHelper.GetParent((DependencyObject)wantedButton);
            parentBorder.Background = new SolidColorBrush(pressed ? pressedMemButtonColor : normalMemButtonColor);
                
            // Button image
            string imgName = pressed ? gameArray[field.Item1, field.Item2].ToString() + ".png" : questionMark;
            string imgPath = resourcePath + imgName;
            var imgBrush = new ImageBrush(new BitmapImage(new Uri(imgPath)));
            imgBrush.Stretch = Stretch.None;
            string styleName = pressed ? "MemPressedTheme" : "ButtonTheme";
            wantedButton.Background = imgBrush;
            wantedButton.Style = (Style)App.Current.Resources[styleName];
            return true;
        }
        private void changeState(GameState toChange)
        {
            // Nothing to change
            if (actualState == toChange)
                return;

            var statePair = stateDict[toChange];
            string stateDescription = statePair.Item1;
            Color descriptionColor = statePair.Item2;
            mWindow.gameInfo.Text = stateDescription;
            mWindow.gameInfo.Foreground = new SolidColorBrush(descriptionColor);
            actualState = toChange;
        }
        private void incrementMistakes()
        {
            mistakes++;
            mWindow.mistakes.Text = mistakesText + mistakes.ToString();
        }
        private async void rightMatch()
        {
            changeState(GameState.RightMatch);
            await Task.Delay(waitTime);
            unpressRight();
            matchedPairs++;
            if (isGameEnd())
                changeState(GameState.EndGame);
        }
        private void unpressRight()
        {
            Button firstField = getButton(pressedFields[0]);
            Button secondField = getButton(pressedFields[1]);
            var borderFirst = (Border)VisualTreeHelper.GetParent((DependencyObject)firstField);
            var borderSecond = (Border)VisualTreeHelper.GetParent((DependencyObject)secondField);
            borderFirst.Background = Brushes.Transparent;
            borderSecond.Background = Brushes.Transparent;
            firstField.Background = Brushes.Transparent;
            firstField.IsEnabled = false;
            secondField.Background = Brushes.Transparent;
            secondField.IsEnabled = false;
            pressedFields.Clear();
        }
        private async void wrongMatch()
        {
            incrementMistakes();
            changeState(GameState.WrongMatch);
            await Task.Delay(waitTime);
            unpressWrong();
        }
        private void unpressWrong()
        {
            fieldChangeToPressed(pressedFields[0], false);
            fieldChangeToPressed(pressedFields[1], false);
            pressedFields.Clear();
        }
        private bool isMatched()
        {
            return pressedFields.Count == 2 &&
                   gameArray[pressedFields[0].Item1, pressedFields[0].Item2] == 
                   gameArray[pressedFields[1].Item1, pressedFields[1].Item2];
        }
        private void handlePressedField(Tuple <int, int> field)
        {
            pressedFields.Add(field);
            if (pressedFields.Count == 1) 
                return;
            if (isMatched()) rightMatch();
            else wrongMatch();

        }
        private bool isGameEnd()
        {
            return matchedPairs == memDim * memDim / 2;
        }
        public void endGame()
        {
            App.Current.Shutdown();
        }
        public async void restartGame()
        {
            if (restartButtonActivated)
                return;
            // Wait for end of other functions
            restartButtonActivated = true;
            await Task.Delay(waitTime);

            mistakes = -1;
            matchedPairs = 0;
            pressedFields.Clear();

            fillGameArray();
            incrementMistakes();
            changeState(GameState.StartGame);

            for (int i = 0; i < memDim; i++)
            {
                for (int j = 0; j < memDim; j++)
                {
                    Button actBut = getButton(Tuple.Create(i, j));
                    actBut.IsEnabled = true;
                    actBut.Style = (Style)App.Current.Resources["ButtonTheme"];
                    var imgBrush = new ImageBrush(new BitmapImage(new Uri(resourcePath + questionMark)));
                    imgBrush.Stretch = Stretch.None;
                    actBut.Background = imgBrush;

                    var border = (Border)VisualTreeHelper.GetParent((DependencyObject)actBut);
                    border.Background = new SolidColorBrush(normalMemButtonColor);
                }
            }
            restartButtonActivated = false;
        }
        public void updateButton(int i, int j)
        {
            Tuple<int, int> fieldToUpdate = Tuple.Create(i, j);
            bool succesPressed = fieldChangeToPressed(fieldToUpdate, true);
            if (succesPressed)
                handlePressedField(fieldToUpdate);
        }
    }
}
