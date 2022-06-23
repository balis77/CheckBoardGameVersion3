using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
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
        public Dictionary<string, Cell> LogicBotMove(Dictionary<string, Cell> board)
        {
            Dictionary<string, Cell> CheckersMove = new Dictionary<string, Cell>();
            Dictionary<string, Cell> CheckersBeat = new Dictionary<string, Cell>();

            foreach (var cell in board)
            {
                
                if (cell.Value.Checker == null)
                    continue;
                if (cell.Value.Checker.Team == SetTeam.Black)
                {
                    if (cell.Value.Checker?.Team != TeamCheckers.Team)
                        break;
                    if (cell.Value.Checker?.Color == CheckerColor.BlackQueen)
                    {
                        board = _queenCheaker.AnaliseMoveAndBeatQueen(board, cell.Value);
                    }
                    if (cell.Value.Checker?.Color == CheckerColor.Black)
                    {
                        board = _actionCheaker.AnaliseCanMoveAndBeat(board, cell.Value);
                    }

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
            if (CheckersBeat.Any())
            {

                var checkerClick = RandomCheckerClick(board, CheckersBeat);
                if (checkerClick.Checker?.Color == CheckerColor.BlackQueen)
                {
                    board = _queenCheaker.AnaliseMoveAndBeatQueen(board, checkerClick);
                }
                if (checkerClick.Checker?.Color == CheckerColor.Black)
                {
                    board = _actionCheaker.AnaliseCanMoveAndBeat(board, checkerClick);
                }
                var beatCellMove = board.Where(n => n.Value.CanAttack == true).ToList();
                Random random = new Random();
                int randomCordinate = random.Next(0, beatCellMove.Count());
                var randomMove = beatCellMove.ElementAt(randomCordinate);
                board = MoveAndBeatChecker(board, randomMove);


            }
            if (CheckersBeat.Any() == false && CheckersMove.Any())
            {
                var checkerClick = RandomCheckerClick(board, CheckersMove);
                if (checkerClick.Checker?.Color == CheckerColor.BlackQueen)
                {
                    board = _queenCheaker.AnaliseMoveAndBeatQueen(board, checkerClick);
                }
                if (checkerClick.Checker?.Color == CheckerColor.Black)
                {
                    board = _actionCheaker.AnaliseCanMoveAndBeat(board, checkerClick);
                }
                var moveCell = board.Where(n => n.Value.CanMove == true).ToList();
                Random random = new Random();
                int randomCordinate = random.Next(0, moveCell.Count());
                var randomMove = moveCell.ElementAt(randomCordinate);
                board = MoveAndBeatChecker(board, randomMove);
            }

            return _board = board;
        }

        private Dictionary<string, Cell> MoveAndBeatChecker(Dictionary<string, Cell> board, KeyValuePair<string, Cell> moveCell)
        {
            var checkerClick = board.FirstOrDefault(n => n.Value.ClickChecker == true);
            if (moveCell.Value.Checker == null || checkerClick.Key != null)
            {

                if (checkerClick.Value.Checker.Color == CheckerColor.BlackQueen
                   || checkerClick.Value.Checker.Color == CheckerColor.WhiteQueen)
                {
                    board = _queenCheaker.MoveAndBeatQueen(board, moveCell.Value);

                }
                else
                {
                    board = _actionCheaker.MoveAndBeatCheckers(board, moveCell.Value);

                }
            }
            return board;

        }

        private Cell RandomCheckerClick(Dictionary<string, Cell> board, Dictionary<string, Cell> checkersMoveAndBeat)
        {
            Random random = new Random();
            List<string> keyCordinateCheckers = new List<string>();
            foreach (var checker in checkersMoveAndBeat)
            {
                keyCordinateCheckers.Add(checker.Key);
            }
            int randomCordinate = random.Next(0, keyCordinateCheckers.Count);
            string keyMoveCheckers = keyCordinateCheckers[randomCordinate];
            var checkerClick = checkersMoveAndBeat[keyMoveCheckers];


            return checkerClick;

        }


    }
}
