using CheckBoardGameVersion3.Data.Models;

namespace CheckBoardGameVersion3.Data.Logic.Validate.ValidateQueen
{
    public class DataCalculateQueenMoveAndBeat
    {
        public Dictionary<string, Cell> Board { get; set; }
        public Cell ClickQueen { get; set; }
        public DataCalculateQueenMoveAndBeat(Dictionary<string, Cell> _board, Cell _clickQueen)
        {
            ClickQueen = _clickQueen;
            Board = _board;
        }
    }
}
