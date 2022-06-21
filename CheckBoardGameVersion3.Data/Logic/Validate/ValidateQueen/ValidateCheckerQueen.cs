using CheckBoardGameVersion3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Logic.Validate.ValidateQueen
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
        public string GetKey(Dictionary<string, Cell> board, int row, int column)
        {
            return board.FirstOrDefault(n => n.Value.X == row && n.Value.Y == column).Key;
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

        public Dictionary<string, Cell> BeatChecker(Dictionary<string, Cell> board, Cell clickQueen, KeyValuePair<string, Cell> keyClickCell, Checker RemoveChecker)
        {
            board[keyClickCell.Key].Checker = new Checker(keyClickCell.Key, clickQueen.Checker.Color, clickQueen.Checker.Team);
            board[RemoveChecker.InCellId].Checker = null;
            board[clickQueen.Checker.InCellId].Checker = null;

            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false;
                board[cell.Key].ClickChecker = false;
                board[cell.Key].CanAttack = false;
            }
            TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);
            return board;
        }
        public Dictionary<string, Cell> PossbileBeatChecker(Dictionary<string, Cell> board, int rowCheckBeat, int columnCheckBeat)
        {
            KeyValuePair<string, Cell> nextBeatCell = GetCell(board, rowCheckBeat, columnCheckBeat);

            if (nextBeatCell.Value?.Checker == null && nextBeatCell.Key != null)
            {
                board[nextBeatCell.Key].CanAttack = true;
            }
            return board;
        }
        public bool CheckerByCordinate(Cell clickQueen, int row, int column)
            => row == clickQueen.X && column == clickQueen.Y;
    }
}
