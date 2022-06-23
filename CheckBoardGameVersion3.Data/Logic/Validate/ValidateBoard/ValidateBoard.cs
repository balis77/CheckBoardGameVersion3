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
        //прімерна логіка хука,ми провіряєм всіма кліками,чи є на дошці десь canAttack якщо вона є треба залочити шашку
        //булова лок якщо є на боарді лок тру ,і це не нажата шашка ,то нас вертає к чорту і не запускають проверки і бити теж залочено ,тільки тими хто може ходити 
        //окей начить тре лібо форіч лібо обичних два форіка для пробівки
        //якщо є шашка і яка,тіпа обична і дамка,якщо видає десь
        //
        //
        //.
        // .
        public Dictionary<string, Cell> ValidateFullBoard(Dictionary<string, Cell> board)
        {
           

            foreach (var boardEntry in board)
            {
                board[boardEntry.Key].LockChecker = false;
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
