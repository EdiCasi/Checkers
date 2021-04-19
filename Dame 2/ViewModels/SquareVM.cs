using Dame_2.Commands;
using Dame_2.Models;
using Dame_2.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Dame_2.ViewModels
{
    class SquareVM : BaseNotification
    {
        GameBusinessLogic bl;
        public SquareVM(Square square, GameBusinessLogic bl)
        {
            SimpleSquare = square;
            this.bl = bl;
        }

        private Square simpleSquare;
        public Square SimpleSquare
        {
            get { return simpleSquare; }
            set
            {
                simpleSquare = value;
                NotifyPropertyChanged("SimpleCell");
            }
        }

        private ICommand clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (clickCommand == null)
                {
                    clickCommand = new RelayCommand<Square>(bl.ClickAction);
                }
                return clickCommand;
            }
        }
    }
}
