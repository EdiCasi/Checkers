using Dame_2.Models;
using Dame_2.ViewModels;
using Dame_2.Views;
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

        public static Type CurrentPlayer { get; set; }

        public static int NumberOfRedPieces { get; set; }
        public static int NumberOfBlackPieces { get; set; }

        private ObservableCollection<ObservableCollection<Square>> squares;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Square>> squares)
        {
            this.squares = squares;
            possibilities = new List<Square>();
        }

        public void SwitchSelectedSquareForRed(Square square)
        {
            if (selectedSquare != null)
            {
                UnSelectThePiece(selectedSquare);
            }

            square.Type = Type.RED_SELECTED;
            selectedSquare = square;

            HidePossibilities();
            ShowPossibilitiesForRed();
            ShowPossibilitiesForRedToTake(selectedSquare);
        }
        public void SwitchSelectedSquareForBlack(Square square)
        {
            if (selectedSquare != null)
            {
                UnSelectThePiece(selectedSquare);
            }

            square.Type = Type.BLACK_SELECTED;
            selectedSquare = square;

            HidePossibilities();
            ShowPossibilitiesForBlack();
            ShowPossibilitiesForBlackToTake(selectedSquare);
        }
        public void SwitchSelectedSquareForRedKing(Square square)
        {
            if (selectedSquare != null)
            {
                UnSelectThePiece(selectedSquare);
            }

            square.Type = Type.RED_KING_SELECTED;
            selectedSquare = square;

            HidePossibilities();
            ShowPossibilitiesForKingToMove(square);
            ShowPossibilitiesForRedKingToTake(selectedSquare);
        }
        public void SwitchSelectedSquareForBlackKing(Square square)
        {
            if (selectedSquare != null)
            {
                UnSelectThePiece(selectedSquare);
            }

            square.Type = Type.BLACK_KING_SELECTED;
            selectedSquare = square;

            HidePossibilities();
            ShowPossibilitiesForKingToMove(square);
            ShowPossibilitiesForBlackKingToTake(selectedSquare);
        }

        public void UnSelectThePiece(Square square)
        {
            switch (square.Type)
            {
                case Type.RED_SELECTED:
                    square.Type = Type.RED;
                    break;
                case Type.BLACK_SELECTED:
                    square.Type = Type.BLACK;
                    break;
                case Type.RED_KING_SELECTED:
                    square.Type = Type.RED_KING;
                    break;
                case Type.BLACK_KING_SELECTED:
                    square.Type = Type.BLACK_KING;
                    break;
            }
        }

        public void ShowPossibilitiesForRed()
        {
            if (selectedSquare.Line - 1 >= 0)
            {
                if (selectedSquare.Column - 1 >= 0)
                {
                    Square possibilitie = squares[selectedSquare.Line - 1][selectedSquare.Column - 1];
                    if (possibilitie.Type == Type.EMPTY_BLACK)
                    {
                        possibilities.Add(possibilitie);
                        possibilitie.Type = Type.GREEN;
                    }
                }
                if (selectedSquare.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[selectedSquare.Line - 1][selectedSquare.Column + 1];
                    if (possibilitie.Type == Type.EMPTY_BLACK)
                    {
                        possibilities.Add(possibilitie);
                        possibilitie.Type = Type.GREEN;
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
                    if (possibilitie.Type == Type.EMPTY_BLACK)
                    {
                        possibilities.Add(possibilitie);
                        possibilitie.Type = Type.GREEN;
                    }
                }
                if (selectedSquare.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[selectedSquare.Line + 1][selectedSquare.Column + 1];
                    if (possibilitie.Type == Type.EMPTY_BLACK)
                    {
                        possibilities.Add(possibilitie);
                        possibilitie.Type = Type.GREEN;
                    }
                }
            }
        }

        public void ShowPossibilitiesForRedToTake(Square square)
        {
            if (square.Line - 1 >= 0)
            {
                if (square.Column - 1 >= 0)
                {
                    Square possibilitie = squares[square.Line - 1][square.Column - 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
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
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Line - 1 >= 0 && possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
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
                if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
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
                if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
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

        public void ShowPossibilitiesForRedKingToTake(Square king)
        {
            if (king.Line - 1 >= 0)
            {
                if (king.Column - 1 >= 0)
                {
                    Square possibilitie = squares[king.Line - 1][king.Column - 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Line - 1 >= 0 && possibilitie.Column - 1 >= 0)
                        {
                            Square possibleMove = squares[possibilitie.Line - 1][possibilitie.Column - 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForRedKingToTake(possibleMove);
                            }
                        }
                    }
                }
                if (king.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[king.Line - 1][king.Column + 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Line - 1 >= 0 && possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                        {
                            Square possibleMove = squares[possibilitie.Line - 1][possibilitie.Column + 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForRedKingToTake(possibleMove);
                            }
                        }
                    }
                }
            }

            if (king.Line + 1 < GameVM.BOARD_DIMMENSION)
            {
                if (king.Column - 1 >= 0)
                {
                    Square possibilitie = squares[king.Line + 1][king.Column - 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Column - 1 >= 0)
                        {
                            Square possibleMove = squares[possibilitie.Line + 1][possibilitie.Column - 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForRedKingToTake(possibleMove);
                            }
                        }
                    }
                }
                if (king.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[king.Line + 1][king.Column + 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                        {
                            Square possibleMove = squares[possibilitie.Line + 1][possibilitie.Column + 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForRedKingToTake(possibleMove);
                            }
                        }
                    }
                }
            }

        }

        public void ShowPossibilitiesForBlackKingToTake(Square king)
        {
            if (king.Line - 1 >= 0)
            {
                if (king.Column - 1 >= 0)
                {
                    Square possibilitie = squares[king.Line - 1][king.Column - 1];
                    if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                    {
                        if (possibilitie.Line - 1 >= 0 && possibilitie.Column - 1 >= 0)
                        {
                            Square possibleMove = squares[possibilitie.Line - 1][possibilitie.Column - 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForBlackKingToTake(possibleMove);
                            }
                        }
                    }
                }
                if (king.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[king.Line - 1][king.Column + 1];
                    if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                    {
                        if (possibilitie.Line - 1 >= 0 && possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                        {
                            Square possibleMove = squares[possibilitie.Line - 1][possibilitie.Column + 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForBlackKingToTake(possibleMove);
                            }
                        }
                    }
                }
            }

            if (king.Line + 1 < GameVM.BOARD_DIMMENSION)
            {
                if (king.Column - 1 >= 0)
                {
                    Square possibilitie = squares[king.Line + 1][king.Column - 1];
                    if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                    {
                        if (possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Column - 1 >= 0)
                        {
                            Square possibleMove = squares[possibilitie.Line + 1][possibilitie.Column - 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForBlackKingToTake(possibleMove);
                            }
                        }
                    }
                }
                if (king.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[king.Line + 1][king.Column + 1];
                    if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                    {
                        if (possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                        {
                            Square possibleMove = squares[possibilitie.Line + 1][possibilitie.Column + 1];
                            if (possibleMove.Type == Type.EMPTY_BLACK)
                            {
                                possibleMove.Type = Type.TAKE;
                                possibilities.Add(possibleMove);
                                ShowPossibilitiesForBlackKingToTake(possibleMove);
                            }
                        }
                    }
                }
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

        public void MoveRed(Square greenSquare)
        {
            if (greenSquare.Line == 0)
                greenSquare.Type = Type.RED_KING;
            else
                greenSquare.Type = Type.RED;

            selectedSquare.Type = Type.EMPTY_BLACK;

            selectedSquare = null;

            HidePossibilities();

            CurrentPlayer = Type.BLACK;
        }
        public void MoveBlack(Square greenSquare)
        {
            if (greenSquare.Line == GameVM.BOARD_DIMMENSION - 1)
                greenSquare.Type = Type.BLACK_KING;
            else
                greenSquare.Type = Type.BLACK;

            selectedSquare.Type = Type.EMPTY_BLACK;
            selectedSquare = null;
            HidePossibilities();

            CurrentPlayer = Type.RED;
        }

        public void MoveKing(Square greenSquare)
        {
            if (selectedSquare.Type == Type.RED_KING_SELECTED)
            {
                greenSquare.Type = Type.RED_KING;
                CurrentPlayer = Type.BLACK;
            }
            else
            {
                greenSquare.Type = Type.BLACK_KING;
                CurrentPlayer = Type.RED;
            }
            selectedSquare.Type = Type.EMPTY_BLACK;
            selectedSquare = null;
            HidePossibilities();
        }

        public void TakePieceWithRed(Square nextPosition)
        {
            if (nextPosition.Column - 1 >= 0)
            {
                Square possibilitie = squares[nextPosition.Line + 1][nextPosition.Column - 1];
                if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                {
                    if (possibilitie.Column - 1 >= 0)
                    {
                        Square possibilitieTake = squares[possibilitie.Line + 1][possibilitie.Column - 1];
                        if (possibilitieTake.Type == Type.TAKE)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            NumberOfBlackPieces--;
                            TakePieceWithRed(possibilitieTake);
                            return;
                        }
                        else if (possibilitieTake.Type == Type.RED_SELECTED)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            NumberOfBlackPieces--;
                            return;
                        }
                    }
                }
            }
            if (nextPosition.Column + 1 < GameVM.BOARD_DIMMENSION)
            {
                Square possibilitie = squares[nextPosition.Line + 1][nextPosition.Column + 1];
                if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                {
                    if (possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                    {
                        Square possibilitieTake = squares[possibilitie.Line + 1][possibilitie.Column + 1];
                        if (possibilitieTake.Type == Type.TAKE)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            NumberOfBlackPieces--;
                            TakePieceWithRed(possibilitieTake);
                            return;
                        }
                        else if (possibilitieTake.Type == Type.RED_SELECTED)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            NumberOfBlackPieces--;
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
                if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                {
                    if (possibilitie.Column - 1 >= 0)
                    {
                        Square possibilitieTake = squares[possibilitie.Line - 1][possibilitie.Column - 1];
                        if (possibilitieTake.Type == Type.TAKE)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            NumberOfRedPieces--;
                            TakePieceWithBlack(possibilitieTake);
                            return;
                        }
                        else if (possibilitieTake.Type == Type.BLACK_SELECTED)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            NumberOfRedPieces--;
                            return;
                        }
                    }
                }
            }
            if (nextPosition.Column + 1 < GameVM.BOARD_DIMMENSION)
            {
                Square possibilitie = squares[nextPosition.Line - 1][nextPosition.Column + 1];
                if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                {
                    if (possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION)
                    {
                        Square possibilitieTake = squares[possibilitie.Line - 1][possibilitie.Column + 1];
                        if (possibilitieTake.Type == Type.TAKE)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            NumberOfRedPieces--;
                            TakePieceWithBlack(possibilitieTake);
                            return;
                        }
                        else if (possibilitieTake.Type == Type.BLACK_SELECTED)
                        {
                            possibilitie.Type = Type.EMPTY_BLACK;
                            NumberOfRedPieces--;
                            return;
                        }
                    }
                }
            }
        }

        public void TakePieceWithRedKing(Square nextPosition)
        {
            if (nextPosition.Line + 1 < GameVM.BOARD_DIMMENSION)
            {
                if (nextPosition.Column - 1 >= 0)
                {
                    Square possibilitie = squares[nextPosition.Line + 1][nextPosition.Column - 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Column - 1 >= 0 && possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION)
                        {
                            Square possibilitieTake = squares[possibilitie.Line + 1][possibilitie.Column - 1];
                            if (possibilitieTake.Type == Type.TAKE)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfBlackPieces--;
                                TakePieceWithRedKing(possibilitieTake);
                                return;
                            }
                            else if (possibilitieTake.Type == Type.RED_KING_SELECTED)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfBlackPieces--;
                                return;
                            }
                        }
                    }
                }
                if (nextPosition.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[nextPosition.Line + 1][nextPosition.Column + 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION)
                        {
                            Square possibilitieTake = squares[possibilitie.Line + 1][possibilitie.Column + 1];
                            if (possibilitieTake.Type == Type.TAKE)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfBlackPieces--;
                                TakePieceWithRedKing(possibilitieTake);
                                return;
                            }
                            else if (possibilitieTake.Type == Type.RED_KING_SELECTED)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfBlackPieces--;
                                return;
                            }
                        }
                    }
                }
            }
            if (nextPosition.Line - 1 >= 0)
            {
                if (nextPosition.Column - 1 >= 0)
                {
                    Square possibilitie = squares[nextPosition.Line - 1][nextPosition.Column - 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Column - 1 >= 0 && possibilitie.Line - 1 >= 0)
                        {
                            Square possibilitieTake = squares[possibilitie.Line - 1][possibilitie.Column - 1];
                            if (possibilitieTake.Type == Type.TAKE)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfBlackPieces--;
                                TakePieceWithRedKing(possibilitieTake);
                                return;
                            }
                            else if (possibilitieTake.Type == Type.RED_KING_SELECTED)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfBlackPieces--;
                                return;
                            }
                        }
                    }
                }
                if (nextPosition.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[nextPosition.Line - 1][nextPosition.Column + 1];
                    if (possibilitie.Type == Type.BLACK || possibilitie.Type == Type.BLACK_KING)
                    {
                        if (possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Line - 1 >= 0)
                        {
                            Square possibilitieTake = squares[possibilitie.Line - 1][possibilitie.Column + 1];
                            if (possibilitieTake.Type == Type.TAKE)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfBlackPieces--;
                                TakePieceWithRedKing(possibilitieTake);
                                return;
                            }
                            else if (possibilitieTake.Type == Type.RED_KING_SELECTED)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfBlackPieces--;
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void TakePieceWithBlackKing(Square nextPosition)
        {
            if (nextPosition.Line + 1 < GameVM.BOARD_DIMMENSION)
            {
                if (nextPosition.Column - 1 >= 0)
                {
                    Square possibilitie = squares[nextPosition.Line + 1][nextPosition.Column - 1];
                    if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                    {
                        if (possibilitie.Column - 1 >= 0 && possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION)
                        {
                            Square possibilitieTake = squares[possibilitie.Line + 1][possibilitie.Column - 1];
                            if (possibilitieTake.Type == Type.TAKE)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfRedPieces--;
                                TakePieceWithBlackKing(possibilitieTake);
                                return;
                            }
                            else if (possibilitieTake.Type == Type.BLACK_KING_SELECTED)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfRedPieces--;
                                return;
                            }
                        }
                    }
                }
                if (nextPosition.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[nextPosition.Line + 1][nextPosition.Column + 1];
                    if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                    {
                        if (possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Line + 1 < GameVM.BOARD_DIMMENSION)
                        {
                            Square possibilitieTake = squares[possibilitie.Line + 1][possibilitie.Column + 1];
                            if (possibilitieTake.Type == Type.TAKE)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfRedPieces--;
                                TakePieceWithBlackKing(possibilitieTake);
                                return;
                            }
                            else if (possibilitieTake.Type == Type.BLACK_KING_SELECTED)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfRedPieces--;
                                return;
                            }
                        }
                    }
                }
            }
            if (nextPosition.Line - 1 >= 0)
            {
                if (nextPosition.Column - 1 >= 0)
                {
                    Square possibilitie = squares[nextPosition.Line - 1][nextPosition.Column - 1];
                    if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                    {
                        if (possibilitie.Column - 1 >= 0 && possibilitie.Line - 1 >= 0)
                        {
                            Square possibilitieTake = squares[possibilitie.Line - 1][possibilitie.Column - 1];
                            if (possibilitieTake.Type == Type.TAKE)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfRedPieces--;
                                TakePieceWithBlackKing(possibilitieTake);
                                return;
                            }
                            else if (possibilitieTake.Type == Type.BLACK_KING_SELECTED)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfRedPieces--;
                                return;
                            }
                        }
                    }
                }
                if (nextPosition.Column + 1 < GameVM.BOARD_DIMMENSION)
                {
                    Square possibilitie = squares[nextPosition.Line - 1][nextPosition.Column + 1];
                    if (possibilitie.Type == Type.RED || possibilitie.Type == Type.RED_KING)
                    {
                        if (possibilitie.Column + 1 < GameVM.BOARD_DIMMENSION && possibilitie.Line - 1 >= 0)
                        {
                            Square possibilitieTake = squares[possibilitie.Line - 1][possibilitie.Column + 1];
                            if (possibilitieTake.Type == Type.TAKE)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfRedPieces--;
                                TakePieceWithBlackKing(possibilitieTake);
                                return;
                            }
                            else if (possibilitieTake.Type == Type.BLACK_KING_SELECTED)
                            {
                                possibilitie.Type = Type.EMPTY_BLACK;
                                NumberOfRedPieces--;
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void TakePieceAndMoveWithRed(Square redSquare)
        {
            TakePieceWithRed(redSquare);
            selectedSquare.Type = Type.EMPTY_BLACK;

            if (redSquare.Line == 0)
                redSquare.Type = Type.RED_KING;
            else
                redSquare.Type = Type.RED;

            if (NumberOfBlackPieces <= 0)
                MainWindow.board.NavigationService.Navigate(new RedWins());

            HidePossibilities();
            CurrentPlayer = Type.BLACK;
            selectedSquare = null;
        }
        public void TakePieceAndMoveWithBlack(Square redSquare)
        {
            TakePieceWithBlack(redSquare);
            selectedSquare.Type = Type.EMPTY_BLACK;

            if (redSquare.Line == GameVM.BOARD_DIMMENSION - 1)
                redSquare.Type = Type.BLACK_KING;
            else
                redSquare.Type = Type.BLACK;

            if (NumberOfRedPieces <= 0)
                MainWindow.board.NavigationService.Navigate(new BlackWins());

            HidePossibilities();
            CurrentPlayer = Type.RED;
            selectedSquare = null;
        }

        public void TakePieceAndMoveWithRedKing(Square redSquare)
        {
            TakePieceWithRedKing(redSquare);
            selectedSquare.Type = Type.EMPTY_BLACK;

            redSquare.Type = Type.RED_KING;

            if (NumberOfBlackPieces <= 0)
                MainWindow.board.NavigationService.Navigate(new RedWins());


            HidePossibilities();
            CurrentPlayer = Type.BLACK;
            selectedSquare = null;
        }

        public void TakePieceAndMoveWithBlackKing(Square redSquare)
        {
            TakePieceWithBlackKing(redSquare);
            selectedSquare.Type = Type.EMPTY_BLACK;

            redSquare.Type = Type.BLACK_KING;

            if (NumberOfRedPieces <= 0)
                MainWindow.board.NavigationService.Navigate(new BlackWins());

            HidePossibilities();
            CurrentPlayer = Type.RED;
            selectedSquare = null;
        }

        public void ShowPossibilitiesForKingToMove(Square king)
        {
            if (king.Line - 1 >= 0)
            {
                if (king.Column - 1 >= 0 && squares[king.Line - 1][king.Column - 1].Type == Type.EMPTY_BLACK)
                {
                    squares[king.Line - 1][king.Column - 1].Type = Type.GREEN;
                    possibilities.Add(squares[king.Line - 1][king.Column - 1]);
                }
                if (king.Column + 1 < GameVM.BOARD_DIMMENSION && squares[king.Line - 1][king.Column + 1].Type == Type.EMPTY_BLACK)
                {
                    squares[king.Line - 1][king.Column + 1].Type = Type.GREEN;
                    possibilities.Add(squares[king.Line - 1][king.Column + 1]);
                }
            }
            if (king.Line + 1 < GameVM.BOARD_DIMMENSION)
            {
                if (king.Column - 1 >= 0 && squares[king.Line + 1][king.Column - 1].Type == Type.EMPTY_BLACK)
                {
                    squares[king.Line + 1][king.Column - 1].Type = Type.GREEN;
                    possibilities.Add(squares[king.Line + 1][king.Column - 1]);
                }
                if (king.Column + 1 < GameVM.BOARD_DIMMENSION && squares[king.Line + 1][king.Column + 1].Type == Type.EMPTY_BLACK)
                {
                    squares[king.Line + 1][king.Column + 1].Type = Type.GREEN;
                    possibilities.Add(squares[king.Line + 1][king.Column + 1]);
                }
            }

        }

        public void Move(Square currentCell)
        {
            if (CurrentPlayer == Type.RED && currentCell.Type == Type.RED)
                SwitchSelectedSquareForRed(currentCell);


            if (CurrentPlayer == Type.BLACK && currentCell.Type == Type.BLACK)
                SwitchSelectedSquareForBlack(currentCell);

            if (CurrentPlayer == Type.RED && currentCell.Type == Type.RED_KING)
                SwitchSelectedSquareForRedKing(currentCell);

            if (CurrentPlayer == Type.BLACK && currentCell.Type == Type.BLACK_KING)
                SwitchSelectedSquareForBlackKing(currentCell);

            if (currentCell.Type == Type.GREEN && selectedSquare.Type == Type.RED_SELECTED)
                MoveRed(currentCell);

            if (currentCell.Type == Type.GREEN && selectedSquare.Type == Type.BLACK_SELECTED)
                MoveBlack(currentCell);

            if (currentCell.Type == Type.GREEN &&
                (selectedSquare.Type == Type.BLACK_KING_SELECTED || selectedSquare.Type == Type.RED_KING_SELECTED))
                MoveKing(currentCell);

            if (currentCell.Type == Type.TAKE && selectedSquare.Type == Type.RED_SELECTED)
                TakePieceAndMoveWithRed(currentCell);

            if (currentCell.Type == Type.TAKE && selectedSquare.Type == Type.BLACK_SELECTED)
                TakePieceAndMoveWithBlack(currentCell);

            if (currentCell.Type == Type.TAKE && selectedSquare.Type == Type.RED_KING_SELECTED)
                TakePieceAndMoveWithRedKing(currentCell);

            if (currentCell.Type == Type.TAKE && selectedSquare.Type == Type.BLACK_KING_SELECTED)
                TakePieceAndMoveWithBlackKing(currentCell);

        }

        public void ClickAction(Square obj)
        {
            Move(obj);
        }
    }
}