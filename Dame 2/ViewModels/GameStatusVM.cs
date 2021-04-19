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
        private GameStatus gameStatus;

        public GameStatus GameStatus
        {
            get { return gameStatus; }
            set { gameStatus = value; }
        }


        public GameStatusVM()
        {
            gameStatus = new GameStatus();
            ReadGame(@"C:/Users/Edi/source/repos/Dame 2/Dame 2/Resources/SatrtGame.txt");
        }

        public void ReadGame(string path)
        {
            TextReader reader = File.OpenText(path);

            string text = reader.ReadLine();

            string[] bits = text.Split(' ');

            if (bits.Length < GameVM.BOARD_DIMMENSION * GameVM.BOARD_DIMMENSION)
                throw new Exception("nu e bine");

            List<int> integerList = new List<int>();

            for (int index = 0; index < bits.Length; index++)
            {
                integerList.Add(int.Parse(bits[index]));
            }

            gameStatus.GameBoard = integerList;
        }
    }
}
