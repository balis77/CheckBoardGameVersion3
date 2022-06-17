using CheckBoardGameVersion3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Logic.Validate
{
    public class ActionCheckerValidator
    {
        public const int LIMIT_BOARD_MAX_VALUE = 8;
        public const int LIMIT_BOARD_MIN_VALUE = 0;

        public bool ValidaiteBackDask(int BoardBack)
        => BoardBack >= LIMIT_BOARD_MAX_VALUE || BoardBack < LIMIT_BOARD_MIN_VALUE;

        public Dictionary<string, Cell> ValidatePossibleBeatTheChecker(Dictionary<string, Cell> board, int row, int column, KeyValuePair<string, Cell> PossibleMoveBoard, int borderBoard)
        {
            var checkerClick = board.FirstOrDefault(n => n.Value.ClickChecker == true);
            if ((PossibleMoveBoard.Value?.Checker != null
                   && !ValidaiteBackDask(borderBoard)))
            {
                if (PossibleMoveBoard.Value?.Checker?.Color == checkerClick.Value?.Checker?.Color)
                {
                    return board;
                }
                var CellPossibleBeat = board.FirstOrDefault(n => n.Value.X == row && n.Value.Y == column);

                if (CellPossibleBeat.Value?.Checker == null && CellPossibleBeat.Key != null)
                {
                    board[CellPossibleBeat.Key].CanAttack = true;
                }

            }

            return board;
        }
        public KeyValuePair<string, Cell> GetCell(Dictionary<string, Cell> board, int row, int column)
        {
            return board.FirstOrDefault(n => n.Value.X == row && n.Value.Y == column);
        }
        public Dictionary<string, Cell> MoveChecker(Dictionary<string, Cell> board, KeyValuePair<string, Cell> PossisionCell, bool BorderBoardColumn)
        {
            if (PossisionCell.Value?.Checker == null
                           && !BorderBoardColumn)
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
    }



}
