using Dame_2.Models;
using Dame_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using Type = Dame_2.Models.Type;

namespace Dame_2.Services
{
    class Helper
    {
        private static Watch watch = new Watch();

        private static GameStatusVM gameStatusVM = new GameStatusVM();
        public static Square CurrentCell { get; set; }
        public static Square PreviousCell { get; set; }
        public static ObservableCollection<ObservableCollection<Square>> InitGameBoard()
        {
            ObservableCollection<ObservableCollection<Square>> gameBoard = new ObservableCollection<ObservableCollection<Square>>();

            List<int> game_board = new List<int>();
            game_board = gameStatusVM.GameStatus.GameBoard;

            for (int line = 0; line < GameVM.BOARD_DIMMENSION; line++)
            {
                ObservableCollection<Square> row = new ObservableCollection<Square>();
                for (int column = 0; column < GameVM.BOARD_DIMMENSION; column++)
                {
                    row.Add(new Square(line, column, (Type)game_board[line * GameVM.BOARD_DIMMENSION + column]));
                }
                gameBoard.Add(row);
            }

            return gameBoard;
        }
    }
}
