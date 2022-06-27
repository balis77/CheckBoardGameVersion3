
using CheckBoardGameVersion3.Data.InformationDask;
using CheckBoardGameVersion3.Data.Logic.Bot;
using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Logic.Validate.ValidateBoard;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Text.Json;

namespace CheckBoardGameVersion3.Client.Pages
{
    public partial class CheckBoard
    {
       [Parameter] public Dictionary<string, Cell> Board { get; set; } = new Dictionary<string, Cell>();
        private RepositoryBoard _repositoryBoard;
        private ActionCheaker _actionCheaker;
        private QueenCheaker _queenCheaker;
        private ValidateBoard _validateBoard;
        private BotChecker _botChecker;
        private BoardInformation _boardInformation;
        protected override async Task OnInitializedAsync()
        {
            _actionCheaker = new ActionCheaker();
            _queenCheaker = new QueenCheaker();
            _repositoryBoard = new RepositoryBoard();
            _validateBoard = new ValidateBoard();
            _boardInformation = new BoardInformation();
            TeamCheckers.SetPlayerGame(SetTeam.White);
            
            
                Board = _repositoryBoard.CreateDesk();
            await Read();


        }
       

        public  Dictionary<string, Cell> MoveAnalise(Dictionary<string, Cell> board, Cell clickChecker)
        {
            board = _validateBoard.ValidateFullBoard(board);

            if (clickChecker.Checker?.Color == CheckerColor.BlackQueen
                || clickChecker.Checker?.Color == CheckerColor.WhiteQueen)
            {
                foreach (var cell in board)
                {
                    if (cell.Value.LockChecker && clickChecker.ClickChecker == false)
                    {
                        board = _queenCheaker.AnaliseMoveAndBeatQueen(Board, cell.Value);

                    }
                }

                var lockchecker = board.FirstOrDefault(n => n.Value.LockChecker == true);

                if (lockchecker.Key != null)
                {
                    return board;
                }

                board = _queenCheaker.AnaliseMoveAndBeatQueen(Board, clickChecker);
            }
            else
            {
                foreach (var cell in board)
                {
                    if (cell.Value.LockChecker && clickChecker.ClickChecker == false)
                    {
                        board = _actionCheaker.AnaliseCanMoveAndBeat(Board, cell.Value);

                    }
                }
                var lockchecker = board.FirstOrDefault(n => n.Value.LockChecker == true);

                if (lockchecker.Key != null)
                {
                    return board;
                }

                board = _actionCheaker.AnaliseCanMoveAndBeat(Board, clickChecker);
            }
           
            return board;
        }
        public Dictionary<string, Cell> MoveAndBeatChecker(Dictionary<string, Cell> board, Cell clickCell)
        {
            _boardInformation.GameOver(Board);
            var checkerClick = board.FirstOrDefault(n => n.Value.ClickChecker == true);

            if (clickCell.Checker != null || checkerClick.Key == null)
            {
                return board;
            }

            if (checkerClick.Value?.Checker?.Color == CheckerColor.BlackQueen
                || checkerClick.Value?.Checker?.Color == CheckerColor.WhiteQueen)
            {
                board = _queenCheaker.MoveAndBeatQueen(Board, clickCell);
            }
            else
            {
                board = _actionCheaker.MoveAndBeatCheckers(Board, clickCell);

            }

            _botChecker = new BotChecker(board);
            board = _botChecker.LogicBotMove(board);

            Save();

            return board;
        }
      

        public async Task Save()
        {
            string jsonFileBoard = JsonSerializer.Serialize(Board);
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "CheckBoard", jsonFileBoard);
        }

        public async Task Read()
        {
          string currentInputValue = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "CheckBoard");

            if (currentInputValue == null)
                return;
            
            var jsonFileBoard = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,Cell>>(currentInputValue);

            if (!jsonFileBoard.Equals(null))
                Board = jsonFileBoard;
        }

        public async Task Delete()
        {
            await JSRuntime.InvokeAsync<string>("localStorage.removeItem", "CheckBoard");
        }
    }

    

}
