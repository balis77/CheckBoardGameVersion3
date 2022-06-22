using CheckBoardGameVersion3.Data.Logic.Bot;
using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Logic.Validate.ValidateBoard;
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
        private ValidateBoard _validateBoard;
        private BotBlackChecker _botBlackChecker;

        protected override void OnInitialized()
        {

            _actionCheaker = new ActionCheaker();
            _queenCheaker = new QueenCheaker();
            _repositoryBoard = new RepositoryBoard();
            _validateBoard = new ValidateBoard();
            
            Board = _repositoryBoard.CreateDesk();
        }
        public Dictionary<string, Cell> MoveAnalise(Dictionary<string, Cell> board, Cell clickChecker)
        {
           
           
            if (clickChecker.Checker?.Color == CheckerColor.BlackQueen
                || clickChecker.Checker?.Color == CheckerColor.WhiteQueen)
            {
                board = _queenCheaker.AnaliseMoveAndBeatQueen(Board, clickChecker);
            }
            else
            {
                board = _actionCheaker.AnaliseCanMoveAndBeat(Board, clickChecker);
            }
            if (clickChecker.Checker?.Team == SetTeam.Black && clickChecker.Checker?.Team == TeamCheckers.Team)
            {
                _botBlackChecker = new BotBlackChecker(board);
                _botBlackChecker.LogicBotMove(board);
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
            
            if (checkerClick.Value.Checker.Color == CheckerColor.BlackQueen 
                || checkerClick.Value.Checker.Color == CheckerColor.WhiteQueen)
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
