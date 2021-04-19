using Dame_2.Models;
using Dame_2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Dame_2.ViewModels
{
    class GameVM
    {
        private GameBusinessLogic bl;

        public static int BOARD_DIMMENSION = 8;

        public GameVM()
        {
            ObservableCollection<ObservableCollection<Square>> board = Helper.InitGameBoard();
            bl = new GameBusinessLogic(board);
            GameBoard = CellBoardToCellVMBoard(board);
        }

        private ObservableCollection<ObservableCollection<SquareVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Square>> board)
        {
            ObservableCollection<ObservableCollection<SquareVM>> result = new ObservableCollection<ObservableCollection<SquareVM>>();
            for (int line = 0; line < BOARD_DIMMENSION; line++)
            {
                ObservableCollection<SquareVM> row = new ObservableCollection<SquareVM>();
                for (int column = 0; column < BOARD_DIMMENSION; column++)
                {
                    SquareVM squareVM = new SquareVM(board[line][column], bl);
                    row.Add(squareVM);
                }
                result.Add(row);
            }
            return result;
        }

        public ObservableCollection<ObservableCollection<SquareVM>> GameBoard { get; set; }
    }
}
