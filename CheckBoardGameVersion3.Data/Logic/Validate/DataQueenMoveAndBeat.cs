using CheckBoardGameVersion3.Data.Models;

namespace CheckBoardGameVersion3.Data.Logic.Validate
{
    public class DataQueenMoveAndBeat
    {
        public Dictionary<string, Cell> board { get; set; }
        public Cell clickQueen { get; set; }
        public KeyValuePair<string, Cell> ClickCell { get; set; }

        public DataQueenMoveAndBeat(Dictionary<string, Cell> _board, Cell _clickQueen, KeyValuePair<string, Cell> _clickCell)
        {
            board = _board;
            clickQueen = _clickQueen;
            ClickCell = _clickCell;
        }
    }
}
