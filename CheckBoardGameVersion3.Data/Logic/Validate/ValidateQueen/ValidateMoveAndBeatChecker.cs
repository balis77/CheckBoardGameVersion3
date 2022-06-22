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
            Checker RemoveChecker = new Checker();

            for (int count = 1; parametr.ClickCell.Value.X - count >= 0 && parametr.ClickCell.Value.Y + count <= 8; count++)
            {
                int row = parametr.ClickCell.Value.X - count;
                int column = parametr.ClickCell.Value.Y + count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(parametr.board, row, column);
                bool queenCordinate = _validateCheckerQueen.CheckerByCordinate(parametr.clickQueen, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (!queenCordinate)
                    {
                        RemoveChecker = nextCellMove.Value.Checker;
                    }
                }
                if (queenCordinate)
                {
                    parametr.board = _validateCheckerQueen.BeatChecker(parametr.board, parametr.clickQueen, parametr.ClickCell, RemoveChecker);
                }
            }
            _board = parametr.board;
        }

        private void CheckBottomLeft(DataQueenMoveAndBeat parametr)
        {
            Checker RemoveChecker = new Checker();

            for (int count = 1; parametr.ClickCell.Value.X + count <= 8 && parametr.ClickCell.Value.Y - count >= 0; count++)
            {
                int row = parametr.ClickCell.Value.X + count;
                int column = parametr.ClickCell.Value.Y - count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(parametr.board, row, column);
                bool queenCordinate = _validateCheckerQueen.CheckerByCordinate(parametr.clickQueen, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (!queenCordinate)
                    {
                        RemoveChecker = nextCellMove.Value.Checker;
                    }
                }
                if (queenCordinate)
                {
                    parametr.board = _validateCheckerQueen.BeatChecker(parametr.board, parametr.clickQueen, parametr.ClickCell, RemoveChecker);

                }
            }
            _board = parametr.board;
        }

        private void CheckTopLeft(DataQueenMoveAndBeat data)
        {
            Checker RemoveChecker = new Checker();
            for (int count = 1; data.ClickCell.Value.X - count >= 0 && data.ClickCell.Value.Y - count >= 0; count++)
            {
                int row = data.ClickCell.Value.X - count;
                int column = data.ClickCell.Value.Y - count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(data.board, row, column);
                bool queenCordinate = _validateCheckerQueen.CheckerByCordinate(data.clickQueen, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (!queenCordinate)
                    {
                        RemoveChecker = nextCellMove.Value.Checker;
                    }
                }
                if (queenCordinate)
                {
                    data.board = _validateCheckerQueen.BeatChecker(data.board, data.clickQueen, data.ClickCell, RemoveChecker);
                }
            }
            _board = data.board;
        }

        private void CheckBottomRight(DataQueenMoveAndBeat parametr)
        {
            Checker RemoveChecker = new Checker();

            for (int count = 1; parametr.ClickCell.Value.X + count <= 8 && parametr.ClickCell.Value.Y + count <= 8; count++)
            {
                int row = parametr.ClickCell.Value.X + count;
                int column = parametr.ClickCell.Value.Y + count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(parametr.board, row, column);
                bool queenCordinate = _validateCheckerQueen.CheckerByCordinate(parametr.clickQueen, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (!queenCordinate)
                    {
                        RemoveChecker = nextCellMove.Value.Checker;
                    }
                }
                if (queenCordinate)
                {
                    parametr.board = _validateCheckerQueen.BeatChecker(parametr.board, parametr.clickQueen, parametr.ClickCell, RemoveChecker);
                }
            }
            _board = parametr.board;
        }
    }
}
