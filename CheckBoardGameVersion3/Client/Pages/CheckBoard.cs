using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions;

namespace CheckBoardGameVersion3.Client.Pages
{
    public partial class CheckBoard
    {
        public Dictionary<string, Cell> Board { get; set; }
        private RepositoryBoard _repositoryBoard;
        private ActionCheaker _actionCheaker;
        private QueenCheaker _queenCheaker;


        protected override void OnInitialized()
        {

            _actionCheaker = new ActionCheaker();
            _queenCheaker = new QueenCheaker();
            _repositoryBoard = new RepositoryBoard();
            Board = _repositoryBoard.CreateDesk();
        }
        public Dictionary<string, Cell> MoveAnalise(Dictionary<string, Cell> board, Cell clickChecker)
        {
            if (clickChecker.Checker.Color == CheckerColor.BlackKing
                || clickChecker.Checker.Color == CheckerColor.WhiteKing)
            {
                board = _queenCheaker.AnaliseMoveAndBeatQueen(Board, clickChecker);
            }
            else
            {
                board = _actionCheaker.AnaliseCanMoveAndBeat(Board, clickChecker);
            }


            return board;
        }
        public Dictionary<string, Cell> MoveAndBeatChecker(Dictionary<string, Cell> board, Cell clickCell)
        {
           var checkerClick = board.FirstOrDefault(n=>n.Value.ClickChecker == true);
            if (clickCell.Checker != null|| checkerClick.Key == null)
            {
                return board;
            }
            if (checkerClick.Value.Checker.Color == CheckerColor.BlackKing 
                || checkerClick.Value.Checker.Color == CheckerColor.WhiteKing)
            {
                board = _queenCheaker.MoveAndBeatQueen(Board, clickCell);
            }
            else
            {
                board = _actionCheaker.MoveAndBeatCheckers(Board, clickCell);

            }
            return board;
        }
    }
}
