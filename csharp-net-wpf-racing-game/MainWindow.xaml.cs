using System.Security.Policy;
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

        int playerSpeed = 15;
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
            score += .05;

            starCounter -= 1;

            scoreText.Content = "Survived " + score.ToString("#.#") + " Seconds";

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

            if(moveLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if(moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }

            if(starCounter < 1 )
            {
                MakeStar();
                starCounter = rand.Next(600, 900);
            }

            foreach(var x in MyCanvas.Children.OfType<Rectangle>())
            {

                if((string)x.Tag == "roadMarks")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + speed);

                    if(Canvas.GetTop(x) > 510)
                    {
                        Canvas.SetTop(x, -152);
                    }
                }

                if((string)x.Tag == "Car")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + speed);

                    if(Canvas.GetTop(x) > 500)
                    {
                        Canvas.SetTop(x, (rand.Next(100, 400) * -1));
                        Canvas.SetLeft(x, rand.Next(0, 430));

                        ChangeCars(x);
                    }
                }
            }
        }

        private void OnkeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if(e.Key == Key.Right)
            {
                moveRight = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if(e.Key == Key.Right)
            {
                moveRight = false;
            }

            if(e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }
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
            starImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/playerImage.png"));

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
            i += .5;

            if(i > 4)
            {
                i = 1;
            }

            switch(i)
            {
                case 1:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powermode1.png"));
                    break;
                case 2:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powermode2.png"));
                    break;
                case 3:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powermode3.png"));
                    break;
                case 4:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powermode4.png"));
                    break;
            }

            MyCanvas.Background = Brushes.LightCoral;
        }

        private void MakeStar()
        {
            Rectangle newStar = new Rectangle
            {
                Height = 50,
                Width = 50,
                Tag = "star",
                Fill = starImage
            };

            Canvas.SetLeft(newStar, rand.Next(0, 430));
            Canvas.SetTop(newStar, (rand.Next(100, 400) * -1));

            MyCanvas.Children.Add(newStar);
        }
    }
}