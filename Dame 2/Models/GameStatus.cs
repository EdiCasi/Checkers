using System;
using System.Collections.Generic;
using System.Text;

namespace Dame_2.Models
{
    class GameStatus
    {
        public List<int> GameBoard { get; set; }

        public int CurrentPlayer { get; set; }

        public int NumberOfRedPieces { get; set; }

        public int NumberOfBlackPieces { get; set; }

        public GameStatus()
        {

        }
    }
}
