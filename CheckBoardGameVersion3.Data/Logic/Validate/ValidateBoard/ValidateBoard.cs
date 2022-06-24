using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using ConsoleApp2.Logic.GameActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Logic.Validate.ValidateBoard
{
    public class ValidateBoard
    {
        private ActionCheaker _actionCheaker;
        private QueenCheaker _queenCheaker; 
        public ValidateBoard()
        {
            _actionCheaker = new ActionCheaker();
            _queenCheaker = new QueenCheaker();
        }
         
        public Dictionary<string, Cell> ValidateFullBoard(Dictionary<string, Cell> board)
        {
            foreach (var cell in board)
            {
                board[cell.Key].LockChecker = false;
            }
            foreach (var cell in board)
            {

                if (cell.Value.Checker?.Team != TeamCheckers.Team)
                    continue;

                if (cell.Value.Checker == null)
                    continue;

                if (cell.Value.Checker.Team == SetTeam.White)
                {
                    if (cell.Value.Checker?.Color == CheckerColor.WhiteQueen)
                    {
                        board = _queenCheaker.AnaliseMoveAndBeatQueen(board, cell.Value);
                    }

                    if (cell.Value.Checker?.Color == CheckerColor.White)
                    {
                        board = _actionCheaker.AnaliseCanMoveAndBeat(board, cell.Value);
                    }

                    var beatChecker = board.FirstOrDefault(n => n.Value.CanAttack == true);
                    var clickChecker = board.FirstOrDefault(n => n.Value.ClickChecker == true);
                    
                    if (beatChecker.Key != null)
                    {
                        board[clickChecker.Key].LockChecker = true;
                        board[clickChecker.Key].ClickChecker = true;
                    }
                }
                
            }
           
            return board;
        }
    }
}
