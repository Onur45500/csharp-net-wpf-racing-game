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

namespace csharp_net_wpf_racing_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();
        List<Rectangle> itemRemover = new List<Rectangle>();

        Random rand = new Random();

        ImageBrush playerImage = new ImageBrush();
        ImageBrush starImage = new ImageBrush();

        Rect playerHitBox;

        int speed = 15;
        int playerScore = 10;
        int carNum = 0;
        int starCounter = 30;
        int powerModeCounter = 200;

        double score;
        double i;

        bool moveLeft, moveRight, gameOver, powerMode;

        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            StartGame();
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnkeyDown(object sender, KeyEventArgs e)
        {

        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void StartGame()
        {
            speed = 8;
            gameTimer.Start();

            moveLeft = false;
            moveRight = false;
            gameOver = false;
            powerMode = false;

            score = 0;

            scoreText.Content = "Survived: 0 seconds";

            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/playerImage.png"));
            starImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/star.png"));

            player.Fill = playerImage;

            MyCanvas.Background = Brushes.Gray;

            foreach(var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if((string)x.Tag == "Car")
                {
                    Canvas.SetTop(x, (rand.Next(100, 400) * -1));
                    Canvas.SetLeft(x, (rand.Next(0, 430)));
                    ChangeCars(x);
                }

                if((string)x.Tag == "star")
                {
                    itemRemover.Add(x);
                }
            }
        }

        private void ChangeCars(Rectangle car)
        {
            carNum = rand.Next(1, 6);

            ImageBrush carImage = new ImageBrush();

            switch(carNum)
            {
                case 1:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car1.png"));
                    break;
                case 2:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car2.png"));
                    break;
                case 3:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car3.png"));
                    break;
                case 4:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car4.png"));
                    break;
                case 5:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car5.png"));
                    break;
                case 6:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car6.png"));
                    break;
            }

            car.Fill = carImage;
        }

        private void PowerUp()
        {

        }

        private void MakeStar()
        {

        }
    }
}