using CheckBoardGameVersion3.Data.Logic.Validate.ValidateChecker;
using CheckBoardGameVersion3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Logic.Validate.ValidateQueen
{
    public class ValidateMoveAndBeatChecker
    {
        private Dictionary<string, Cell> _board;
        private ValidateCheckerQueen _validateCheckerQueen;

        public ValidateMoveAndBeatChecker(Dictionary<string, Cell> board)
        {
            _board = board;
            _validateCheckerQueen = new ValidateCheckerQueen();
        }

        public Dictionary<string, Cell> MoveAndBeatChecker(DataQueenMoveAndBeat parametr)
        {
            CheckTopRight(parametr);
            CheckTopLeft(parametr);
            CheckBottomRight(parametr);
            CheckBottomLeft(parametr);

            return _board;
        }

        private void CheckTopRight(DataQueenMoveAndBeat parametr)
        {
            Checker removeChecker = new Checker();

            for (int count = 1; parametr.ClickCell.Value.X - count >= 0 && parametr.ClickCell.Value.Y + count <= 8; count++)
            {
                int row = parametr.ClickCell.Value.X - count;
                int column = parametr.ClickCell.Value.Y + count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(parametr.Board, row, column);
                bool queenCordinate = _validateCheckerQueen.CheckerByCordinate(parametr.ClickQueen, row, column);

                if (nextCellMove.Value?.Checker != null)
                {
                    if (!queenCordinate)
                    {
                        removeChecker = nextCellMove.Value.Checker;
                    }
                }

                if (queenCordinate)
                {
                    parametr.Board = _validateCheckerQueen.BeatChecker(parametr.Board, parametr.ClickQueen, parametr.ClickCell, removeChecker);
                }
            }
            _board = parametr.Board;
        }

        private void CheckBottomLeft(DataQueenMoveAndBeat parametr)
        {
            Checker removeChecker = new Checker();

            for (int count = 1; parametr.ClickCell.Value.X + count <= 8 && parametr.ClickCell.Value.Y - count >= 0; count++)
            {
                int row = parametr.ClickCell.Value.X + count;
                int column = parametr.ClickCell.Value.Y - count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(parametr.Board, row, column);
                bool queenCordinate = _validateCheckerQueen.CheckerByCordinate(parametr.ClickQueen, row, column);

                if (nextCellMove.Value?.Checker != null)
                {
                    if (!queenCordinate)
                    {
                        removeChecker = nextCellMove.Value.Checker;
                    }
                }

                if (queenCordinate)
                {
                    parametr.Board = _validateCheckerQueen.BeatChecker(parametr.Board, parametr.ClickQueen, parametr.ClickCell, removeChecker);

                }
            }
            _board = parametr.Board;
        }

        private void CheckTopLeft(DataQueenMoveAndBeat data)
        {
            Checker removeChecker = new Checker();
            for (int count = 1; data.ClickCell.Value.X - count >= 0 && data.ClickCell.Value.Y - count >= 0; count++)
            {
                int row = data.ClickCell.Value.X - count;
                int column = data.ClickCell.Value.Y - count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(data.Board, row, column);
                bool queenCordinate = _validateCheckerQueen.CheckerByCordinate(data.ClickQueen, row, column);

                if (nextCellMove.Value?.Checker != null)
                {
                    if (!queenCordinate)
                    {
                        removeChecker = nextCellMove.Value.Checker;
                    }
                }

                if (queenCordinate)
                {
                    data.Board = _validateCheckerQueen.BeatChecker(data.Board, data.ClickQueen, data.ClickCell, removeChecker);
                }
            }
            _board = data.Board;
        }

        private void CheckBottomRight(DataQueenMoveAndBeat parametr)
        {
            Checker removeChecker = new Checker();

            for (int count = 1; parametr.ClickCell.Value.X + count <= 8 && parametr.ClickCell.Value.Y + count <= 8; count++)
            {
                int row = parametr.ClickCell.Value.X + count;
                int column = parametr.ClickCell.Value.Y + count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(parametr.Board, row, column);
                bool queenCordinate = _validateCheckerQueen.CheckerByCordinate(parametr.ClickQueen, row, column);

                if (nextCellMove.Value?.Checker != null)
                {
                    if (!queenCordinate)
                    {
                        removeChecker = nextCellMove.Value.Checker;
                    }
                }
                if (queenCordinate)
                {
                    parametr.Board = _validateCheckerQueen.BeatChecker(parametr.Board, parametr.ClickQueen, parametr.ClickCell, removeChecker);
                }
            }
            _board = parametr.Board;
        }
    }
}
