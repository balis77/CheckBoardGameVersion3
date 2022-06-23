using CheckBoardGameVersion3.Data.Models;

namespace CheckBoardGameVersion3.Data.Logic.Validate.ValidateChecker
{
    public class DataQueenMoveAndBeat
    {
        public Dictionary<string, Cell> Board { get; set; }
        public Cell ClickQueen { get; set; }
        public KeyValuePair<string, Cell> ClickCell { get; set; }

        public DataQueenMoveAndBeat(Dictionary<string, Cell> _board, Cell _clickQueen, KeyValuePair<string, Cell> _clickCell)
        {
            Board = _board;
            ClickQueen = _clickQueen;
            ClickCell = _clickCell;
        }
    }
}
