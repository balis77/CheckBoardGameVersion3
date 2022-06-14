using CheckBoardGameVersion3.Data.Models;

namespace ConsoleApp2.Logic.GameActions.Contracts
{
    public interface IActionCheaker
    {
        Dictionary<string, Cell> MoveAndBeatCheckers(Dictionary<string, Cell> board, Cell clickCell);
        Dictionary<string, Cell> AnaliseCanMoveAndBeat(Dictionary<string, Cell> board, Cell clickCell);

    }
}
