using Dame_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dame_2.ViewModels
{
    class GameStatusVM
    {


        public GameStatus Status { get; set; }

        public GameStatusVM(string path)
        {
            Status = ReadGameStatus(path);
        }

        public GameStatus ReadGameStatus(string path)
        {
            TextReader reader = File.OpenText(path);

            GameStatus status = new GameStatus();

            string text = reader.ReadLine();

            string[] bits = text.Split(' ');

            if (bits.Length < GameVM.BOARD_DIMMENSION * GameVM.BOARD_DIMMENSION)
                throw new Exception("nu e bine");

            List<int> integerList = new List<int>();

            for (int index = 0; index < bits.Length; index++)
            {
                if (bits[index] != "")
                    integerList.Add(int.Parse(bits[index]));
            }

            text = reader.ReadLine();

            bits = text.Split(' ');

            if (bits.Length < 3)
                throw new Exception("nu e bine");

            status.CurrentPlayer = int.Parse(bits[0]);

            status.NumberOfRedPieces = int.Parse(bits[1]);

            status.NumberOfBlackPieces = int.Parse(bits[2]);


            status.GameBoard = integerList;

            return status;
        }

    }
}
