﻿using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using CheckBoardGameVersion3.Data.RepositoryBoard;
namespace CheckBoardGameVersion3.Data.InformationDask
{
    public class BoardInformation
    {
        public const int MAXIMUN_SCORE = 3;
        public const int LACK_OF_CHECKER = 0;
        public int CountWhite { get; private set; }
        public int CountBlack { get; private set; }
        private MockRepositoryBoard repositoryBoard;
        public BoardInformation()
        {
            repositoryBoard = new MockRepositoryBoard();
        }

        public int AmountBlackCheckers(Dictionary<string, Cell> board)
            => board.Where(n => n.Value?.Checker?.Color == CheckerColor.Black).Count();

        public int AmountWhiteCheckers(Dictionary<string, Cell> board)
            => board.Where(n => n.Value?.Checker?.Color == CheckerColor.White).Count();

        public int AmountBlackQueenCheckers(Dictionary<string, Cell> board)
            => board.Where(n => n.Value?.Checker?.Color == CheckerColor.BlackQueen).Count();

        public int AmountWhiteQueenCheckers(Dictionary<string, Cell> board)
            => board.Where(n => n.Value?.Checker?.Color == CheckerColor.WhiteQueen).Count();

        public Dictionary<string, Cell> GameOver(Dictionary<string, Cell> board)
        {
            int numberBlackChecker = board.Where(n => n.Value?.Checker?.Team == SetTeam.Black).Count();
            int numberWhiteChecker = board.Where(n => n.Value?.Checker?.Team == SetTeam.White).Count();

            if (numberBlackChecker.Equals(LACK_OF_CHECKER))
            {
                CountWhite++;
                board = repositoryBoard.CreateDesk();
            }

            if (numberWhiteChecker.Equals(LACK_OF_CHECKER))
            {
                CountBlack++;
                board = repositoryBoard.CreateDesk();
            }

            if (CountBlack.Equals(MAXIMUN_SCORE) || CountWhite.Equals(MAXIMUN_SCORE))
            {
                CountBlack = 0;
                CountWhite = 0;

            }
            return board;
        }
    }
}
