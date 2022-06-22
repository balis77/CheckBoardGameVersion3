using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Logic.Validate.ValidateChecker
{
    public class ActionCheckerValidator
    {
        public const int LIMIT_BOARD_MAX_VALUE = 8;
        public const int LIMIT_BOARD_MIN_VALUE = 0;

        public bool ValidaiteBackDask(int BoardBack)
        => BoardBack >= LIMIT_BOARD_MAX_VALUE || BoardBack < LIMIT_BOARD_MIN_VALUE;

        public KeyValuePair<string, Cell> GetCell(Dictionary<string, Cell> board, int row, int column)
        {
            return board.FirstOrDefault(n => n.Value.X == row && n.Value.Y == column);
        }
        public Dictionary<string, Cell> MoveChecker(Dictionary<string, Cell> board, KeyValuePair<string, Cell> PossisionCell)
        {
            if (PossisionCell.Value?.Checker == null)
            {
                board[PossisionCell.Key].CanMove = true;
            }
            return board;
        }

        public Dictionary<string, Cell> CheckerMove(Dictionary<string, Cell> board, Cell moveChecker, string keyclickCell)
        {
            board[keyclickCell].Checker = new Checker(keyclickCell, moveChecker.Checker.Color, moveChecker.Checker.Team);
            board[moveChecker.Checker.InCellId].Checker = null;
            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false;
                board[cell.Key].ClickChecker = false;
                board[cell.Key].CanAttack = false;
            }
            return board;
        }

        public Dictionary<string, Cell> BeatChecker(Dictionary<string, Cell> board, Cell clickChecker, int row, int column)
        {
            var biba = GetCell(board, row, column);
            if (biba.Value != null)
            {
                if (biba.Value.Checker != null)
                {
                    if (biba.Value?.Checker?.Team == TeamCheckers.Team)
                    {
                        return board;
                    }
                    int rowCheckBeat = row + (row - clickChecker.X);
                    int columnCheckBeat = column + (column - clickChecker.Y);
                    var CellPossibleBeat = board.FirstOrDefault(n => n.Value.X == rowCheckBeat && n.Value.Y == columnCheckBeat);
                    if (CellPossibleBeat.Value?.Checker == null && CellPossibleBeat.Key != null)
                    {
                        board[CellPossibleBeat.Key].CanAttack = true;
                    }
                }

            }
            return board;
        }
    }



}
