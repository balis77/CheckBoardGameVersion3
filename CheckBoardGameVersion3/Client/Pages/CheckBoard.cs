using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions;

namespace CheckBoardGameVersion3.Client.Pages
{
    public partial class CheckBoard
    {
        public List<string> CordinateCellMovePossible { get; set; } = new List<string>();
        public   Dictionary<string,Cell> Board { get; set; }
        private RepositoryBoard _repositoryBoard;
        private ActionCheaker _actionCheaker;
        

        protected override void OnInitialized()
        {
            
            _actionCheaker = new ActionCheaker();
            _repositoryBoard = new RepositoryBoard();
            Board = _repositoryBoard.CreateDesk();
        }
    }
}
