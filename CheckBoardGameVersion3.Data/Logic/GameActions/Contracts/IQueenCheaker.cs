using CheckBoardGameVersion3.Data.Models;

namespace ConsoleApp2.Logic.GameActions.Contracts
{
    public interface IQueenCheaker
    {
        Dictionary<string, Cell> MoveAndBeatQueen(Dictionary<string, Cell> board, Cell clickCell);
        Dictionary<string, Cell> AnaliseMoveAndBeatQueen(Dictionary<string, Cell> board, Cell clickCell);
    }
}
