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
  
        public Dictionary<string, Cell> ValidatePossibleBeatTheChecker(Dictionary<string,Cell> board,int row,int column, KeyValuePair<string, Cell> PossibleMoveBoard,int borderBoard)
        {
            if (PossibleMoveBoard.Value?.Checker != null
                   && !ValidaiteBackDask(borderBoard))
            {

                var leftCell = board.FirstOrDefault(n => n.Value.X == row && n.Value.Y == column);

                if (leftCell.Value?.Checker == null && leftCell.Key != null)
                {
                    board[leftCell.Key].CanAttack = true;
                }
            }
            return board;
        }
        public KeyValuePair<string, Cell> GetCell(Dictionary<string, Cell> board, int row, int column)
        {
          return  board.FirstOrDefault(n => n.Value.X == row && n.Value.Y == column);
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
        public SetTeam setTeam(SetTeam team) 
            => team == SetTeam.White ? SetTeam.Black : SetTeam.White;
    }
}
