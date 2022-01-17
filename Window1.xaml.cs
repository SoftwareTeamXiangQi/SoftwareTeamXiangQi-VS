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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            BGM.Source = new Uri(Environment.CurrentDirectory + "\\start.mp3");
            BGM.Play();
              }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "开始游戏":
                    MainWindow mainWindow = new MainWindow();

                    
                    App.Current.MainWindow = mainWindow;

                    
                    this.Close();

                    
                    mainWindow.Show();
                    break;
                case "退出":
                    this.Close();
                        break;
                
            }
           
        }
       
        
    }
}
