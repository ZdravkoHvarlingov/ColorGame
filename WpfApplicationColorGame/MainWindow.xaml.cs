using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Brush> gameColors;
        private int turnCounter=0;
        private DispatcherTimer timer;
        private Random rnd;
        private TimeSpan gameTime;
        private List<Brush> gameBorderColors;  

        public MainWindow()
        {
            InitializeComponent();

            rnd = new Random();

            TextBlockTimer.Text = "Time remaining: 0:20";
            TextBlockTurns.Text = "Turns: 0"; 

            gameColors = new List<Brush>() {Brushes.Red,Brushes.Blue, Brushes.Yellow,Brushes.Green};           

            GridColorGame.Background = gameColors.OrderBy(c=>rnd.Next()).Take(1).First();
            borderGame.BorderBrush = PickBrush();
            gameTime = new TimeSpan(0, 0, 20);

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,1);
            timer.Tick += new EventHandler(timer_Tick);

            MenuItemFirstLevel.IsChecked = true;
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            gameTime -= new TimeSpan(0,0,1);
            TextBlockTimer.Text = string.Format("Time remaining: {0}:{1}",gameTime.Minutes,gameTime.Seconds);          

            if (gameTime==new TimeSpan(0,0,0))
            {
                timer.Stop();
                MessageBox.Show(string.Format("The game finished. your score is {0}.",turnCounter));
                TextBlockTimer.Text = "Time remaining: 0:20";
                turnCounter = 0;
                TextBlockTurns.Text = "Turns: 0";
                gameTime = new TimeSpan(0,0,20);
            }
        }

        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            timer.Start();

            if (Keyboard.IsKeyDown(Key.Up) && GridColorGame.Background==Brushes.Blue)
            {
                borderGame.BorderBrush = PickBrush();
                 GridColorGame.Background = gameColors.OrderBy(c=>rnd.Next()).Take(1).First();
                 turnCounter++;
                 TextBlockTurns.Text = "Turns: " + turnCounter;
            }
            else if (Keyboard.IsKeyDown(Key.Down) && GridColorGame.Background == Brushes.Red)
            {
                borderGame.BorderBrush = PickBrush();
                GridColorGame.Background = gameColors.OrderBy(c => rnd.Next()).Take(1).First();
                turnCounter++;
                TextBlockTurns.Text = "Turns: " + turnCounter;
            }
            else if (Keyboard.IsKeyDown(Key.Left) && GridColorGame.Background == Brushes.Yellow)
            {
                borderGame.BorderBrush = PickBrush();
                GridColorGame.Background = gameColors.OrderBy(c => rnd.Next()).Take(1).First();
                turnCounter++;
                TextBlockTurns.Text = "Turns: " + turnCounter;
            }
            else if (Keyboard.IsKeyDown(Key.Right) && GridColorGame.Background == Brushes.Green)
            {
                borderGame.BorderBrush = PickBrush();
                GridColorGame.Background = gameColors.OrderBy(c => rnd.Next()).Take(1).First();
                turnCounter++;
                TextBlockTurns.Text = "Turns: " + turnCounter;
            }
            else
            {
                timer.Stop();
                MessageBox.Show("You did a wrong turn. The game finished. Your score is: "+turnCounter);                
                TextBlockTimer.Text = "Time remaining: 0:20";
                turnCounter = 0;
                TextBlockTurns.Text = "Turns: 0";
                gameTime = new TimeSpan(0,0,20);
            }
        }

        private void OnMenuItemFirstChecked(object sender, RoutedEventArgs e)
        {
            MenuItemSecondLevel.IsChecked = false;

            ImageArrow.Visibility = Visibility.Visible;
        }

        private void OnMenuItemSecondChecked(object sender, RoutedEventArgs e)
        {
            MenuItemFirstLevel.IsChecked = false;

            ImageArrow.Visibility = Visibility.Hidden;
        }

        private void OnMenuItemExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private Brush PickBrush()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }
    }
}
