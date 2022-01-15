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

namespace SoftwareTeamXiangQi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        
        public MainWindow()
        {
            InitializeComponent();
            Media.Source = new Uri(Environment.CurrentDirectory + "\\start.mp3");
            Media.Play();
        }

        public void Click(object sender, RoutedEventArgs e)
        { 
            switch (((Button)sender).Name)
            {
                case "开始游戏":
                    GameWindow gameWindow = new GameWindow();   //实例化
                    gameWindow.Show();
                    gameWindow.Left = 0;
                    gameWindow.Top = 0;
                    this.Close();  //关闭当前页面
                    break;
                case "退出":
                    this.Close();
                    break;
            }

        }
    }

   
}
