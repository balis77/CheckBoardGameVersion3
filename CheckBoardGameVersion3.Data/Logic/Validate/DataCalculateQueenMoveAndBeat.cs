using CheckBoardGameVersion3.Data.Models;

namespace CheckBoardGameVersion3.Data.Logic.Validate
{
    public class DataCalculateQueenMoveAndBeat
    {
        public Dictionary<string, Cell> board { get; set; }
        public Cell clickQueen { get; set; }
        public DataCalculateQueenMoveAndBeat(Dictionary<string,Cell> _board,Cell _clickQueen)
        {
            clickQueen = _clickQueen;
            board = _board;
        }
    }
}
