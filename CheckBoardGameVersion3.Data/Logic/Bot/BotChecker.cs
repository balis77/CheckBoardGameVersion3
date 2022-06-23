using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using ConsoleApp2.Logic.GameActions;

namespace CheckBoardGameVersion3.Data.Logic.Bot
{
    
    public class BotChecker
    {
        private Dictionary<string, Cell> _board;
        private ActionCheaker _actionCheaker;
        private QueenCheaker _queenCheaker;

        public BotChecker(Dictionary<string, Cell> board)
        {
            _board = board;

            _actionCheaker = new ActionCheaker();
            _queenCheaker = new QueenCheaker();
        }
        public Dictionary<string, Cell> LogicBotMove(Dictionary<string, Cell> board)
        {
            Dictionary<string, Cell> checkersMove = new Dictionary<string, Cell>();
            Dictionary<string, Cell> checkersBeat = new Dictionary<string, Cell>();

            ChargingAllBotCheckers(board, checkersMove, checkersBeat);
            LogicMoveAndBeatBot(board, checkersMove, checkersBeat);

            return _board;
        }
        #region PrivateMethod
        private void ChargingAllBotCheckers(Dictionary<string, Cell> board, Dictionary<string, Cell> checkersMove, Dictionary<string, Cell> checkersBeat)
        {
            foreach (var cell in board)
            {

                if (cell.Value.Checker == null)
                    continue;

                if (cell.Value.Checker.Team == TeamCheckers.Bot)
                {
                    if (cell.Value.Checker?.Team != TeamCheckers.Team)
                        break;

                    if (cell.Value.Checker?.Color == CheckerColor.BlackQueen|| cell.Value.Checker?.Color == CheckerColor.WhiteQueen)
                    {
                        board = _queenCheaker.AnaliseMoveAndBeatQueen(board, cell.Value);
                    }

                    if (cell.Value.Checker?.Color == CheckerColor.Black|| cell.Value.Checker?.Color == CheckerColor.White)
                    {
                        board = _actionCheaker.AnaliseCanMoveAndBeat(board, cell.Value);
                    }

                    var moveChecker = board.FirstOrDefault(n => n.Value.CanMove);
                    var beatChecker = board.FirstOrDefault(n => n.Value.CanAttack);
                    var clickChecker = board.FirstOrDefault(n => n.Value.ClickChecker);
                    if (moveChecker.Key != null)
                    {
                        checkersMove.Add(clickChecker.Key, clickChecker.Value);
                    }
                    if (beatChecker.Key != null)
                    {
                        checkersBeat.Add(clickChecker.Key, clickChecker.Value);
                    }
                }
            }
            _board = board;
        }

        private void LogicMoveAndBeatBot(Dictionary<string, Cell> board, Dictionary<string, Cell> checkersMove, Dictionary<string, Cell> checkersBeat)
        {
            if (checkersBeat.Any())
            {
                PickAnaliseChecker(board, checkersBeat);

                var beatCellMove = board.Where(n => n.Value.CanAttack).ToList();

                PickRandomMoveAndBeat(board, beatCellMove);
            }
            if (checkersBeat.Any() == false && checkersMove.Any())
            {
                PickAnaliseChecker(board, checkersMove);

                var moveCell = board.Where(n => n.Value.CanMove).ToList();

                PickRandomMoveAndBeat(board, moveCell);
            }
            _board = board;
        }

        private void PickRandomMoveAndBeat(Dictionary<string, Cell> board, List<KeyValuePair<string, Cell>> moveCell)
        {
            Random random = new Random();
            int randomCordinate = random.Next(0, moveCell.Count());
            var randomMove = moveCell.ElementAt(randomCordinate);

            MoveAndBeatChecker(board, randomMove);

            _board = board;
        }


        private void PickAnaliseChecker(Dictionary<string, Cell> board, Dictionary<string, Cell> checkersBeat)
        {
            var checkerClick = choiseRandomChecker(checkersBeat);

            if (checkerClick.Checker?.Color == CheckerColor.BlackQueen || checkerClick.Checker?.Color == CheckerColor.WhiteQueen)
            {
                board = _queenCheaker.AnaliseMoveAndBeatQueen(board, checkerClick);
            }

            if (checkerClick.Checker?.Color == CheckerColor.Black || checkerClick.Checker?.Color == CheckerColor.White)
            {
                board = _actionCheaker.AnaliseCanMoveAndBeat(board, checkerClick);
            }

            _board = board;
        }

        private void MoveAndBeatChecker(Dictionary<string, Cell> board, KeyValuePair<string, Cell> moveCell)
        {
            var checkerClick = board.FirstOrDefault(n => n.Value.ClickChecker);
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
            _board = board;

        }

        private Cell choiseRandomChecker(Dictionary<string, Cell> checkersMoveAndBeat)
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
        #endregion
    }
}
