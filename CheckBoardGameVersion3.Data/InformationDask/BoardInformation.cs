using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.InformationDask
{
    public class BoardInformation
    {

        public int CountWhite { get; private set; }
        public int CountBlack { get; private set; }


        public int AmountBlackCheckers(Dictionary<string, Cell> board)
        {
            int numberBlack = board.Where(n => n.Value?.Checker?.Color == CheckerColor.Black).Count();
            return numberBlack;
        }
        public int AmountWhiteCheckers(Dictionary<string, Cell> board)
        {
            int numberWhite = board.Where(n => n.Value?.Checker?.Color == CheckerColor.White).Count();
            return numberWhite;
        }
        public int AmountBlackQueenCheckers(Dictionary<string, Cell> board)
        {
            int numberBlack = board.Where(n => n.Value?.Checker?.Color == CheckerColor.BlackQueen).Count();
            return numberBlack;
        }
        public int AmountWhiteQueenCheckers(Dictionary<string, Cell> board)
        {
            int numberWhite = board.Where(n => n.Value?.Checker?.Color == CheckerColor.WhiteQueen).Count();
            return numberWhite;
        }
        public void GameOver(Dictionary<string, Cell> board)
        {
            int numberBlackChecker = board.Where(n => n.Value?.Checker?.Team == SetTeam.Black).Count();
            int numberWhiteChecker = board.Where(n => n.Value?.Checker?.Team == SetTeam.White).Count();

            if (numberBlackChecker.Equals(0))
            {
                CountWhite++;
            }

            if (numberWhiteChecker.Equals(0))
            {
                CountBlack++;
            }

            if (CountBlack.Equals(3) || CountWhite.Equals(3))
            {
                CountBlack = 0;
                CountWhite = 0;
            }

        }
    }
}
