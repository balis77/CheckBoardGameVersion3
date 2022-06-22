using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using ConsoleApp2.Logic.GameActions;

namespace CheckBoardGameVersion3.Data.Logic.Bot
{
    /// <summary>
    /// берем всі чорні шашки
    /// проганяєм через форік з використанням обичної проверки ходьби
    /// потім ми берем лібо рандом походити,лібо обов'язково побити
    /// все це рандомиться к чортовій матері і тоді шашка на чілі собі ходить
    /// </summary>
    public class BotBlackChecker
    {
        private Dictionary<string, Cell> _board;
        private ActionCheaker _actionCheaker;
        private QueenCheaker _queenCheaker;

        public BotBlackChecker(Dictionary<string, Cell> board)
        {
            _board = board;
            _actionCheaker = new ActionCheaker();
            _queenCheaker = new QueenCheaker();
        }
        public void LogicBotMove(Dictionary<string, Cell> board)
        {
            Dictionary<string, Cell> CheckersMove = new Dictionary<string, Cell>();
            Dictionary<string, Cell> CheckersBeat = new Dictionary<string, Cell>();

            foreach (var cell in board)
            {
                if (cell.Value.Checker?.Team != TeamCheckers.Team)
                    continue;
                if (cell.Value.Checker == null)
                    continue;
                if (cell.Value.Checker.Team == SetTeam.Black)
                {
                    board = _actionCheaker.AnaliseCanMoveAndBeat(board, cell.Value);
                    var moveChecker = board.FirstOrDefault(n => n.Value.CanMove == true);
                    var beatChecker = board.FirstOrDefault(n => n.Value.CanAttack == true);
                    var clickChecker = board.FirstOrDefault(n => n.Value.ClickChecker == true);
                    if (moveChecker.Key != null)
                    {
                        CheckersMove.Add(clickChecker.Key, clickChecker.Value);
                    }
                    if (beatChecker.Key != null)
                    {
                        CheckersBeat.Add(clickChecker.Key, clickChecker.Value);
                    }
                }
            }
            //KeyValuePair<string, Cell> checkerClick;
           
            if (CheckersBeat.Any())
            {
                board = validateCheckerMoveAndBeat(board, CheckersBeat);
                var beatCellMove = board.FirstOrDefault(n => n.Value.CanAttack == true);
                board = _actionCheaker.MoveAndBeatCheckers(board, beatCellMove.Value);
            }
            else
            {
                board = validateCheckerMoveAndBeat(board, CheckersMove);
                var moveCell = board.FirstOrDefault(n => n.Value.CanMove == true);
                board = _actionCheaker.MoveAndBeatCheckers(board, moveCell.Value);
            }

            _board = board;
        }

        private Dictionary<string, Cell> validateCheckerMoveAndBeat(Dictionary<string, Cell> board, Dictionary<string, Cell> CheckersMove)
        {
            Random random = new Random();
            List<string> keyCordinateCheckers = new List<string>();
            foreach (var checker in CheckersMove)
            {
                keyCordinateCheckers.Add(checker.Key);
            }
            int randomCordinate = random.Next(0, keyCordinateCheckers.Count);
            var keyMoveCheckers = keyCordinateCheckers[randomCordinate];
            var checkerClick = CheckersMove[keyMoveCheckers];
            board = _actionCheaker.AnaliseCanMoveAndBeat(board, checkerClick);
            return board;
        }

       
    }
}
