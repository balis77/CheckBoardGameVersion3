using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
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
        public Dictionary<string, Cell> MoveAnalise(Dictionary<string, Cell> board, Cell clickCell)
        {
            if (clickCell.Checker.Color == Data.Models.Enums.CheckerColor.BlackKing 
                || clickCell.Checker.Color == Data.Models.Enums.CheckerColor.WhiteKing)
            {
                board = _queenCheaker.MoveQueen(Board, clickCell);
            }
            else
            {
                board = _actionCheaker.AnaliseCanMoveAndBeat(Board, clickCell);
            }
           
           
            return board;
        }
    }
}
