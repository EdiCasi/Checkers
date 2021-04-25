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
                { Type.EMPTY_BLACK, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/emptyBalck.png" },
                { Type.EMPTY_WHITE, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/emptyWhite.png" },
                { Type.BLACK, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/black.png" },
                { Type.RED, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/red.png" },
                { Type.RED_SELECTED, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/redSelected.png" },
                { Type.BLACK_SELECTED, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/blackSelected.png" },
                { Type.GREEN, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/green.png" },
                { Type.TAKE, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/take.png" },
                { Type.BLACK_KING, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/blackKing.png" },
                { Type.RED_KING, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/redKing.png" },
                { Type.BLACK_KING_SELECTED, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/blackKingSelected.png" },
                { Type.RED_KING_SELECTED, System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/redKingSelected.png" }
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
