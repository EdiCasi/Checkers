using Dame_2.Models;
using Dame_2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Type = Dame_2.Models.Type;

namespace Dame_2.ViewModels
{
    class GameVM
    {
        private static GameBusinessLogic bl;

        public static int BOARD_DIMMENSION = 8;

        public static GameStatusVM InitialGameStatus { get; set; }

        public static ObservableCollection<ObservableCollection<SquareVM>> GameBoard { get; set; }
        public static ObservableCollection<ObservableCollection<Square>> Board { get; set; }

        public GameVM()
        {
            InitialGameStatus = new GameStatusVM(System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/SatrtGame.txt");

            Board = Helper.ConvertIntegerMatrixToSquareMatrix(InitialGameStatus.Status.GameBoard);

            bl = new GameBusinessLogic(Board);

            GameBusinessLogic.CurrentPlayer = (Type)InitialGameStatus.Status.CurrentPlayer;

            GameBusinessLogic.NumberOfRedPieces = InitialGameStatus.Status.NumberOfRedPieces;

            GameBusinessLogic.NumberOfBlackPieces = InitialGameStatus.Status.NumberOfBlackPieces;

            GameBoard = Helper.CellBoardToCellVMBoard(Board, bl);
        }

        public static void ResetGame()
        {
            Board = Helper.ConvertIntegerMatrixToSquareMatrix(InitialGameStatus.Status.GameBoard);

            bl = new GameBusinessLogic(Board);

            for (int line = 0; line < BOARD_DIMMENSION; line++)
            {
                for (int column = 0; column < BOARD_DIMMENSION; column++)
                {
                    GameBoard[line][column].SimpleSquare.Type = Board[line][column].Type;
                }
            }
            GameBusinessLogic.CurrentPlayer = (Type)InitialGameStatus.Status.CurrentPlayer;

            GameBusinessLogic.NumberOfRedPieces = InitialGameStatus.Status.NumberOfRedPieces;

            GameBusinessLogic.NumberOfBlackPieces = InitialGameStatus.Status.NumberOfBlackPieces;
        }

        public static void RestoreGame(string path)
        {
            GameStatusVM status = new GameStatusVM(path);

            Board = Helper.ConvertIntegerMatrixToSquareMatrix(status.Status.GameBoard);

            for (int line = 0; line < BOARD_DIMMENSION; line++)
            {
                for (int column = 0; column < BOARD_DIMMENSION; column++)
                {
                    GameBoard[line][column].SimpleSquare.Type = Board[line][column].Type;
                }
            }
            GameBusinessLogic.CurrentPlayer = (Type)status.Status.CurrentPlayer;

            GameBusinessLogic.NumberOfRedPieces = status.Status.NumberOfRedPieces;

            GameBusinessLogic.NumberOfBlackPieces = status.Status.NumberOfBlackPieces;
        }

        public static void Save(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            string stringLine = "";
            for (int line = 0; line < BOARD_DIMMENSION; line++)
            {
                for (int column = 0; column < BOARD_DIMMENSION; column++)
                {
                    stringLine += (int)GameBoard[line][column].SimpleSquare.Type + " ";
                }
            }
            sw.WriteLine(stringLine);
            stringLine = "";

            stringLine += (int)GameBusinessLogic.CurrentPlayer + " ";
            stringLine += GameBusinessLogic.NumberOfRedPieces + " ";
            stringLine += GameBusinessLogic.NumberOfBlackPieces;

            sw.WriteLine(stringLine);
            sw.Close();
        }
    }
}
