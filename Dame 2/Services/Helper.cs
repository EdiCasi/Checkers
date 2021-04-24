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
        public static ObservableCollection<ObservableCollection<Square>> ConvertIntegerMatrixToSquareMatrix(List<int> game_board)
        {
            Watch watch = new Watch();

            ObservableCollection<ObservableCollection<Square>> gameBoard = new ObservableCollection<ObservableCollection<Square>>();

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
        public static ObservableCollection<ObservableCollection<SquareVM>> 
            CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Square>> board, GameBusinessLogic bl)
        {
            ObservableCollection<ObservableCollection<SquareVM>> result = new ObservableCollection<ObservableCollection<SquareVM>>();
            for (int line = 0; line < GameVM.BOARD_DIMMENSION; line++)
            {
                ObservableCollection<SquareVM> row = new ObservableCollection<SquareVM>();
                for (int column = 0; column < GameVM.BOARD_DIMMENSION; column++)
                {
                    SquareVM squareVM = new SquareVM(board[line][column], bl);
                    row.Add(squareVM);
                }
                result.Add(row);
            }
            return result;
        }






    }
}
