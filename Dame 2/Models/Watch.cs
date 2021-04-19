using System;
using System.Collections.Generic;
using System.Text;

namespace Dame_2.Models
{
    public enum Type
    {
        EMPTY_BLACK = 0,
        EMPTY_WHITE = 1,
        BLACK = 2,
        RED = 3,
        RED_SELECTED = 4,
        BLACK_SELECTED = 5,
        GREEN = 6,
        TAKE = 7,
        BLACK_KING = 8,
        RED_KING = 9,
        BLACK_KING_SELECTED = 10,
        RED_KING_SELECTED = 11
    }
    class Watch
    {
        private static Dictionary<Type, string> filePaths;
        public Watch()
        {
            filePaths = new Dictionary<Type, string>()
            {
                { Type.EMPTY_BLACK, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\emptyBalck.png" },
                { Type.EMPTY_WHITE, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\emptyWhite.png" },
                { Type.BLACK, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\black.png" },
                { Type.RED, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\red.png" },
                { Type.RED_SELECTED, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\redSelected.png" },
                { Type.BLACK_SELECTED, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\blackSelected.png" },
                { Type.GREEN, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\green.png" },
                { Type.TAKE, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\take.png" },
                { Type.BLACK_KING, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\blackKing.png" },
                { Type.RED_KING, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\redKing.png" },
                { Type.BLACK_KING_SELECTED, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\blackKingSelected.png" },
                { Type.RED_KING_SELECTED, @"C:\Users\Edi\source\repos\Dame 2\Dame 2\Resources\redKingSelected.png" }
            };
        }
        public static string GetPath(int value)
        {
            return filePaths[(Type)value];
        }
        public static string GetPath(Type type)
        {
            return filePaths[type];
        }
    }
}
