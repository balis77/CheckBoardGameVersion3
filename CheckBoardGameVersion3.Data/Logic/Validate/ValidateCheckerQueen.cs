using CheckBoardGameVersion3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Logic.Validate
{
    public class ValidateCheckerQueen
    {
        public const int LIMIT_BOARD_MAX_VALUE = 8;
        public const int LIMIT_BOARD_MIN_VALUE = 0;

        public bool ValidaiteBackDask(int BoardBack)
        => BoardBack >= LIMIT_BOARD_MAX_VALUE || BoardBack < LIMIT_BOARD_MIN_VALUE;
        public KeyValuePair<string, Cell> GetCell(Dictionary<string, Cell> board, int row, int column)
        {
            return board.FirstOrDefault(n => n.Value.X == row && n.Value.Y == column);
        }
        public Dictionary<string, Cell> MoveQueenPossible(Dictionary<string, Cell> board, KeyValuePair<string, Cell> nextCellMove, Cell clickQueen)
        {
            
            if (nextCellMove.Value == null || clickQueen.Checker == null)
            {
                return board;
            }
            if (nextCellMove.Value.Checker?.Team == clickQueen.Checker.Team)
            {
                return board;
            }
            board[nextCellMove.Key].CanMove = true;
            return board;
        }
        public Dictionary<string, Cell> QueenMove(Dictionary<string, Cell> board, Cell moveChecker, string keyclickCell)
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
