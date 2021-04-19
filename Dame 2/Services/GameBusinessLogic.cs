using Dame_2.Models;
using Dame_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Type = Dame_2.Models.Type;

namespace Dame_2.Services
{
    class GameBusinessLogic
    {
        private Square selectedSquare = null;

        private List<Square> possibilities;

        private Type currentPlayer = Type.RED;

        private ObservableCollection<ObservableCollection<Square>> squares;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Square>> squares)
        {
            this.squares = squares;
            possibilities = new List<Square>();
        }

        public void SwitchSelectedSquare(Square square)
        {
            if (selectedSquare == null)
            {

                switch (square.Type)
                {
                    case Type.RED:
                        square.Type = Type.RED_SELECTED;
                        selectedSquare = square;
                        break;
                    case Type.BLACK:
                        square.Type = Type.BLACK_SELECTED;
                        selectedSquare = square;
                        break;
                    case Type.RED_KING:
                        square.Type = Type.RED_KING_SELECTED;
                        selectedSquare = square;
                        break;
                    case Type.BLACK_KING:
                        square.Type = Type.BLACK_KING_SELECTED;
                        selectedSquare = square;
                        break;
                }
            }
            switch (selectedSquare.Type)
            {
                case Type.RED_SELECTED:
                    selectedSquare.Type = Type.RED;
                    square.Type = Type.RED_SELECTED;
                    selectedSquare = square;
                    break;
                case Type.BLACK_SELECTED:
                    selectedSquare.Type = Type.BLACK;
                    square.Type = Type.BLACK_SELECTED;
                    selectedSquare = square;
                    break;
                case Type.RED_KING_SELECTED:
                    selectedSquare.Type = Type.RED_KING;

                    if (square.Type == Type.RED)
                        square.Type = Type.RED_SELECTED;
                    else
                        square.Type = Type.RED_KING_SELECTED;

                    selectedSquare = square;
                    break;
                case Type.BLACK_KING_SELECTED:
                    selectedSquare.Type = Type.BLACK_KING;

                    if (square.Type == Type.BLACK)
                        square.Type = Type.BLACK_SELECTED;
                    else
                        square.Type = Type.BLACK_KING_SELECTED;

                    selectedSquare = square;
                    break;
            }
            HidePossibilities();
            if (selectedSquare.Type == Type.RED_SELECTED)
            {
                ShowPossibilitiesForRed();
                ShowPossibilitiesForRedToTake(selectedSquare);
            }
            else
            {
                ShowPossibilitiesForBlack();
                ShowPossibilitiesForBlackToTake(selectedSquare);
            }

        }

        public void ShowPossibilitiesForRed()
        {
            if (selectedSquare.Line - 1 >= 0)
            {
                if (selectedSquare.Column - 1 >= 0)
                {
                    Square possibilitie = squares[selectedSquare.Line - 1][selectedSquare.Column - 1];
                    if (possibilitie.Type == Type.EMPTY_BLACK) //Sa fac si pt cazul in care asta e red de sus
                        possibilities.Add(possibilitie);
                }
                if (selectedSquare.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[selectedSquare.Line - 1][selectedSquare.Column + 1];
                    if (possibilitie.Type == Type.EMPTY_BLACK)
                        possibilities.Add(possibilitie);
                }
            }
            foreach (Square square in possibilities)
            {
                square.Type = Type.GREEN;
            }
        }

        public void ShowPossibilitiesForRedToTake(Square square)
        {
            if (square.Line - 1 >= 0)
            {
                if (square.Column - 1 >= 0)
                {
                    Square possibilitie = squares[square.Line - 1][square.Column - 1];
                    if (possibilitie.Type == Type.BLACK)
                    {
                        if (possibilitie.Line - 1 >= 0 && possibilitie.Column - 1 >= 0)
                        {
                            Square possibleMove = squares[possibilitie.Line - 1][possibilitie.Column - 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForRedToTake(possibleMove);
                            }
                        }
                    }
                }
                if (square.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[square.Line - 1][square.Column + 1];
                    if (possibilitie.Type == Type.BLACK)
                    {
                        if (possibilitie.Line - 1 >= 0 && possibilitie.Column + 1 >= 0)
                        {
                            Square possibleMove = squares[possibilitie.Line - 1][possibilitie.Column + 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForRedToTake(possibleMove);
                            }
                        }
                    }
                }
            }
        }

        public void ShowPossibilitiesForBlackToTake(Square square)
        {
            if (square.Line + 1 >= GameVM.BOARD_DIMMENSION)
                return;

            if (square.Column - 1 >= 0)
            {
                Square possibilitie = squares[square.Line + 1][square.Column - 1];
                if (possibilitie.Type == Type.RED)
                {
                    if (possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Column - 1 >= 0)
                    {
                        Square possibleMove = squares[possibilitie.Line + 1][possibilitie.Column - 1];
                        if (possibleMove.Type == Type.EMPTY_BLACK)
                        {
                            possibleMove.Type = Type.TAKE;
                            possibilities.Add(possibleMove);
                            ShowPossibilitiesForBlackToTake(possibleMove);
                        }
                    }
                }
            }
            if (square.Column + 1 < GameVM.BOARD_DIMMENSION)
            {
                Square possibilitie = squares[square.Line + 1][square.Column + 1];
                if (possibilitie.Type == Type.RED)
                {
                    if (possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                    {
                        Square possibleMove = squares[possibilitie.Line + 1][possibilitie.Column + 1];
                        if (possibleMove.Type == Type.EMPTY_BLACK)
                        {
                            possibleMove.Type = Type.TAKE;
                            possibilities.Add(possibleMove);
                            ShowPossibilitiesForBlackToTake(possibleMove);
                        }
                    }
                }
            }

        }

        public void ShowPossibilitiesForBlack()
        {
            if (selectedSquare.Line + 1 < GameVM.BOARD_DIMMENSION)
            {
                if (selectedSquare.Column - 1 >= 0)
                {
                    Square possibilitie = squares[selectedSquare.Line + 1][selectedSquare.Column - 1];
                    if (possibilitie.Type == Type.EMPTY_BLACK) //Sa fac si pt cazul in care asta e red de sus
                        possibilities.Add(possibilitie);
                }
                if (selectedSquare.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[selectedSquare.Line + 1][selectedSquare.Column + 1];
                    if (possibilitie.Type == Type.EMPTY_BLACK)
                        possibilities.Add(possibilitie);
                }
            }
            foreach (Square square in possibilities)
            {
                square.Type = Type.GREEN;
            }
        }

        public void HidePossibilities()
        {
            if (possibilities.Count == 0)
                return;
            foreach (Square square in possibilities)
            {
                if (square.Type == Type.GREEN || square.Type == Type.TAKE)
                    square.Type = Type.EMPTY_BLACK;
            }
            possibilities.Clear();
        }

        public void MovePiece(Square currentCell)
        {
            if (selectedSquare.Type == Type.BLACK_SELECTED)
            {
                if (currentCell.Line == GameVM.BOARD_DIMMENSION - 1)
                    currentCell.Type = Type.BLACK_KING;
                else
                    currentCell.Type = Type.BLACK;
            }
            else
            {
                if (currentCell.Line == 0)
                    currentCell.Type = Type.RED_KING;
                else
                    currentCell.Type = Type.RED;
            }

            selectedSquare.Type = Type.EMPTY_BLACK;
            selectedSquare = null;
            HidePossibilities();

            if (currentPlayer == Type.RED)
                currentPlayer = Type.BLACK;
            else
                currentPlayer = Type.RED;
        }

        public void TakePieceWithRed(Square nextPosition)
        {
            if (nextPosition.Column - 1 >= 0)
            {
                Square possibilitie = squares[nextPosition.Line + 1][nextPosition.Column - 1];
                if (possibilitie.Type == Type.BLACK)
                {
                    if (possibilitie.Column - 1 >= 0)
                    {
                        Square possibilitieTake = squares[possibilitie.Line + 1][possibilitie.Column - 1];
                        if (possibilitieTake.Type == Type.TAKE)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            TakePieceWithRed(possibilitieTake);
                            return;
                        }
                        else if (possibilitieTake.Type == Type.RED_SELECTED)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            return;
                        }
                    }
                }
            }
            if (nextPosition.Column + 1 < GameVM.BOARD_DIMMENSION)
            {
                Square possibilitie = squares[nextPosition.Line + 1][nextPosition.Column + 1];
                if (possibilitie.Type == Type.BLACK)
                {
                    if (possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                    {
                        Square possibilitieTake = squares[possibilitie.Line + 1][possibilitie.Column + 1];
                        if (possibilitieTake.Type == Type.TAKE)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            TakePieceWithRed(possibilitieTake);
                            return;
                        }
                        else if (possibilitieTake.Type == Type.RED_SELECTED)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            return;
                        }
                    }
                }
            }
        }

        public void TakePieceWithBlack(Square nextPosition)
        {
            if (nextPosition.Column - 1 >= 0)
            {
                Square possibilitie = squares[nextPosition.Line - 1][nextPosition.Column - 1];
                if (possibilitie.Type == Type.RED)
                {
                    if (possibilitie.Column - 1 >= 0)
                    {
                        Square possibilitieTake = squares[possibilitie.Line - 1][possibilitie.Column - 1];
                        if (possibilitieTake.Type == Type.TAKE)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            TakePieceWithBlack(possibilitieTake);
                            return;
                        }
                        else if (possibilitieTake.Type == Type.BLACK_SELECTED)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            return;
                        }
                    }
                }
            }
            if (nextPosition.Column + 1 < GameVM.BOARD_DIMMENSION)
            {
                Square possibilitie = squares[nextPosition.Line - 1][nextPosition.Column + 1];
                if (possibilitie.Type == Type.RED)
                {
                    if (possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                    {
                        Square possibilitieTake = squares[possibilitie.Line - 1][possibilitie.Column + 1];
                        if (possibilitieTake.Type == Type.TAKE)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            TakePieceWithBlack(possibilitieTake);
                            return;
                        }
                        else if (possibilitieTake.Type == Type.BLACK_SELECTED)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            return;
                        }
                    }
                }
            }
        }

        public void TakePiece(Square nextPosition)
        {
            if (selectedSquare.Type == Type.RED_SELECTED)
            {
                TakePieceWithRed(nextPosition);
                selectedSquare.Type = Type.EMPTY_BLACK;
                nextPosition.Type = Type.RED;
                HidePossibilities();
                currentPlayer = Type.BLACK;
                selectedSquare = null;
            }
            else if (selectedSquare.Type == Type.BLACK_SELECTED)
            {
                TakePieceWithBlack(nextPosition);
                selectedSquare.Type = Type.EMPTY_BLACK;
                nextPosition.Type = Type.BLACK;
                HidePossibilities();
                currentPlayer = Type.RED;
                selectedSquare = null;
            }
        }

        public void Move(Square currentCell)
        {
            if (currentCell.Type == currentPlayer ||
                currentCell.Type == Type.RED_KING ||
                currentCell.Type == Type.BLACK_KING)
                SwitchSelectedSquare(currentCell);
            if (currentCell.Type == Type.GREEN)
                MovePiece(currentCell);
            if (currentCell.Type == Type.TAKE)
                TakePiece(currentCell);
        }


        public void ClickAction(Square obj)
        {
            Move(obj);
        }
    }
}
