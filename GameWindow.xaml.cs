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
using System.Windows.Shapes;

namespace SoftwareTeamXiangQi
{
    /// <summary>
    /// GameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            board = new Board();
            fail = false;
            hint = false;
            buttons = new List<Button>();
            CreateGrid();
            Music.Source = new Uri(Environment.CurrentDirectory + "\\game.mp3");
            FM.Source = new Uri(Environment.CurrentDirectory + "\\fail.mp3");
            MoveSound.Source = new Uri(Environment.CurrentDirectory + "\\move.wav");
            Music.Play(); // 开启背景音乐
            
        }

        public static Board board = new Board();
        public static Boolean fail = false;
        public static Boolean hint = false;
        public static List<Button> buttons = new List<Button>();        
        
        public void CreateGrid()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Button button = new Button();
                    button.Style = FindResource("ChangeStyle") as Style;

                    if (board.chesses[row, col] != null)
                    {
                        button.Background = Button_Background(board.chesses[row, col]);
                    }
                    else
                        button.Background = Brushes.Transparent; //button背景透明色

                    button.Click += new RoutedEventHandler(this.Button_Click);
                    button.BorderBrush = Brushes.Transparent;//button边框透明
                    button.Width = 80;
                    button.Height = 80;

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    XiangqiGrid.Children.Add(button);
                    buttons.Add(button);//ButtonList
                }
            }
        }
        
        private ImageBrush Button_Background(Chess chess)
        {
            ImageBrush brush = new ImageBrush();
            String source = "Resources/";

            if (chess.color == Color.red)
                source += "red";
            else
                source += "black";


            switch (chess.Print)
            {
                case "兵":
                    source += "Soilder.png";
                    break;
                case "炮":
                    source += "Cannon.png";
                    break;
                case "车":
                    source += "Rook.png";
                    break;
                case "马":
                    source += "Horse.png";
                    break;
                case "象":
                    source += "Elephant.png";
                    break;
                case "士":
                    source += "Guard.png";
                    break;
                case "将":
                    source += "King.png";
                    break;
            }
            brush.ImageSource = new BitmapImage(new Uri(source, UriKind.Relative));
            return brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "退出")
            {
                MainWindow mainWindow = new MainWindow();//实例化 开始界面
                mainWindow.Show();
                this.Close();
            }
            else
            {
                if (fail)
                {
                    MessageBox.Show("        " + board.turn.ToString() + " Win!\n" + "The game is over!");
                }
                else
                {
                    HandClick.HandleClick(sender);

                    if (HandClick.eat_music)   //下棋声
                    {
                            MoveSound.Play();
                            Delay(250);
                            MoveSound.Stop();
                    }


                    if (fail)
                    {
                        Music.Stop();
                        FM.Play();
                        MessageBox.Show(board.turn.ToString() + " Win!");
                    }
                    else
                    {
                        if (board.turn == Color.red)
                        {
                            label1.Foreground = Brushes.Red;
                            label1.Content = "红方 / Red Player";
                        }
                        else
                        {
                            label1.Foreground = Brushes.Black;
                            label1.Content = "黑方 / Black Player";
                        }

                        if (board.model == play_model.origin_chess)
                        {
                            label2.Content = "选棋 / Choose chess";
                        }
                        else
                        {
                            label2.Content = "落棋 / Choose position";
                        }
                    }

                }
            }

        }
     
        public void Delay(double delayTime)
        {
            DateTime now = DateTime.Now;
            double s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.Milliseconds;  //Milliseconds是指以毫秒计数
            }
            while (s < delayTime);
        }

        public static void Hint(int ROW, int COL)
        {
            if (hint)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (board.chesses[row, col] == null || board.chesses[ROW, COL].color != board.chesses[row, col].color)
                        {
                            if (board.chesses[ROW, COL].CheckValidMove(row, col))
                            {
                                buttons[row * 9 + col].BorderBrush = Brushes.Black;
                            }
                        }
                    }
                }
            }
        }

        public static void CleanHint()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (buttons[row * 9 + col].BorderBrush == Brushes.Black)
                        buttons[row * 9 + col].BorderBrush = Brushes.Transparent;
                }
            }
        }
    }
}
