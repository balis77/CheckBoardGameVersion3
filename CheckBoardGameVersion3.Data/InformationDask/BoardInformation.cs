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
        //колво шашок
        //який колір ходить
        //колво дамок
        //А і тре ще партії регати і виводити
        //.
        private Dictionary<string, Cell> _board;
        public  int CountWhite { get; set; }
        public int CountBlack { get; set; }

        //public BoardInformation(Dictionary<string, Cell> board)
        //{
        //    _board = board;
        //}
        public int AmountBlackCheckers(Dictionary<string, Cell> board)
        {
            int numberBlack;
            numberBlack = board.Where(n => n.Value?.Checker?.Color == CheckerColor.Black).Count();
            return numberBlack;
        }
        public int AmountWhiteCheckers(Dictionary<string, Cell> board)
        {
            int numberWhite;
            numberWhite = board.Where(n => n.Value?.Checker?.Color == CheckerColor.White).Count();
            return numberWhite;
        }
        public int AmountBlackQueenCheckers(Dictionary<string, Cell> board)
        {
            int numberBlack;
            numberBlack = board.Where(n => n.Value?.Checker?.Color == CheckerColor.BlackQueen).Count();
            return numberBlack;
        }
        public int AmountWhiteQueenCheckers(Dictionary<string, Cell> board)
        {
            int numberWhite;
            numberWhite = board.Where(n => n.Value?.Checker?.Color == CheckerColor.WhiteQueen).Count();
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
            
        }
    }
}
