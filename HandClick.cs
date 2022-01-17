using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoftwareTeamXiangQi
{ 
    internal class HandClick
    { 
        public static int selectRow;
        public static int selectCol;
        public static bool finish = false;
        public static bool eat_music = false;  
        public static Button? FristButton,SecondButton;
        public static ValueTuple<Chess,Brush> record;


        public static void HandleClick(object sender)
        {
            eat_music = false;
            switch (((Button)sender).Name)
            {
                case "悔棋":
                case "换棋":
                case "提示":
                case "退出":
                    specialClick(sender);
                    break;
                default:
                    normalClick(sender);
                    break;
            }
        }

        public static void specialClick(object sender){
            switch (((Button)sender).Name)
            {
                case "换棋":
                    if (FristButton == null || FristButton.Background == Brushes.Transparent)
                        MessageBox.Show("You don't chose any chess");
                    else{
                        GameWindow.CleanHint();
                        GameWindow.board.model = play_model.origin_chess;                       
                    }
                    break;
                case "悔棋": 
                    if (SecondButton == null || finish != true)
                        MessageBox.Show("You don't move any chess");
                    else
                    {
                        int origin_row = GameWindow.buttons.IndexOf(SecondButton) / 9;
                        int origin_col = GameWindow.buttons.IndexOf(SecondButton) % 9;
                        int destation_row = GameWindow.buttons.IndexOf(FristButton) / 9;
                        int destation_col = GameWindow.buttons.IndexOf(FristButton) % 9;

                        GameWindow.board.chesses[origin_row, origin_col].MoveChess(origin_row, origin_col, destation_row, destation_col);
                        FristButton.Background = SecondButton.Background;
                        SecondButton.Background = record.Item2;
                        if (record.Item2 != Brushes.Transparent)
                            GameWindow.board.chesses[origin_row, origin_col] = record.Item1;

                        GameWindow.board.Turn(GameWindow.board.turn);
                        SecondButton = null;
                        eat_music = true;   //开启下棋声
                    }
                    break;
                case "提示":
                    GameWindow.hint = !GameWindow.hint;
                    if (GameWindow.hint)
                        ((Button)sender).Content = "取消提示 / Clean hint";
                    else
                        ((Button)sender).Content = "提示 / Hint";
                    break;
                
            }


        }
        public static void normalClick(object sender) {

            int row = GameWindow.buttons.IndexOf((Button)sender) / 9;
            int col = GameWindow.buttons.IndexOf((Button)sender) % 9;

            try
            {
                switch (GameWindow.board.model)
                {
                    case play_model.origin_chess:
                        finish = false;
                        GameWindow.board.selectChess(row, col);
                        selectRow = row;
                        selectCol = col;
                        GameWindow.Hint(selectRow, selectCol);
                        FristButton = (Button)sender;
                        break;
                    case play_model.destination:
                        if (GameWindow.board.chesses[row, col] != null && GameWindow.board.chesses[row, col].color == GameWindow.board.turn)
                            throw new Exception("You can't choose the same color chess");

                        if (GameWindow.board.chesses[selectRow, selectCol].CheckValidMove(row, col))
                        {
                            if (GameWindow.board.chesses[row, col] != null && GameWindow.board.chesses[row, col].Print == "将")
                            {
                                    GameWindow.fail = true;
                            }
                            SecondButton = (Button)sender;
                            if(SecondButton.Background != Brushes.Transparent)
                            {
                                record.Item1 = GameWindow.board.chesses[row, col];
                                record.Item2 = SecondButton.Background;
                            }
                            else
                                record.Item2 = Brushes.Transparent;
                            GameWindow.board.chesses[selectRow, selectCol].MoveChess(selectRow, selectCol, row, col);
                            SecondButton.Background = FristButton.Background;
                            FristButton.Background = Brushes.Transparent;
                            GameWindow.CleanHint();
                            if (!GameWindow.fail)
                            {
                                GameWindow.board.model = play_model.origin_chess;
                                GameWindow.board.Turn(GameWindow.board.turn);
                            }
                            finish = true;
                            eat_music = true;  //开启下棋声
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
                
        }
    }
}
