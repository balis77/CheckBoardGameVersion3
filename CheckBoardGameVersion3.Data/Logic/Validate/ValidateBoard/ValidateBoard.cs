﻿using CheckBoardGameVersion3.Data.Models;
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

                if (cell.Value.Checker?.Team != TeamCheckers.PlayerMove || TeamCheckers.PlayerMove != TeamCheckers.Dask)
                    continue;

                if (cell.Value.Checker == null)
                    continue;


                if (cell.Value.Checker?.Color == CheckerColor.WhiteQueen || cell.Value.Checker?.Color == CheckerColor.BlackQueen)
                {
                    board = _queenCheaker.AnaliseMoveAndBeatQueen(board, cell.Value);
                }

                if (cell.Value.Checker?.Color == CheckerColor.White || cell.Value.Checker?.Color == CheckerColor.Black)
                {
                    board = _actionCheaker.AnaliseCanMoveAndBeat(board, cell.Value);
                }

                var beatChecker = board.FirstOrDefault(n => n.Value.CanAttack);
                var clickChecker = board.FirstOrDefault(n => n.Value.ClickChecker);

                if (beatChecker.Key != null)
                {
                    board[clickChecker.Key].LockChecker = true;
                    board[clickChecker.Key].ClickChecker = true;
                }
                else
                {
                    board[clickChecker.Key].ClickChecker = false;
                }
            }
            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false; 
            }
            return board;
        }
    }
}
