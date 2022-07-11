using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Logic.Validate.ValidateChecker;
using CheckBoardGameVersion3.Data.Logic.Validate.ValidateQueen;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions.Contracts;

namespace ConsoleApp2.Logic.GameActions
{
    public class QueenCheaker : IQueenCheaker
    {
        private ValidateCheckerQueen _validateCheckerQueen;
        public QueenCheaker()
        {
            _validateCheckerQueen = new ValidateCheckerQueen();
        }

        public Dictionary<string, Cell> MoveAndBeatQueen(Dictionary<string, Cell> board, Cell clickCell)
        {

            string clickCheckerKey = string.Empty;

            foreach (var cell in board)
            {
                if (cell.Value.ClickChecker)
                {
                    clickCheckerKey = cell.Key;
                }
            }
            if (board.Where(n => n.Value.CanAttack).Any())
            {
                foreach (var cell in board)
                {
                    cell.Value.CanMove = false;
                }
            }
           
            Cell clickQueen = new Cell();

            if (clickCheckerKey != string.Empty)
            {
                clickQueen = board[clickCheckerKey];
            }

            KeyValuePair<string, Cell> keyClickCell = _validateCheckerQueen.GetCell(board, clickCell.X, clickCell.Y);

            if (clickCell.CanMove)
            {
                board = _validateCheckerQueen.QueenMove(board, clickQueen, keyClickCell.Key);
                TeamCheckers.PlayerMove = TeamCheckers.setTeam(TeamCheckers.PlayerMove);
                return board;
            }
            
            if (clickCell.CanAttack)
            {
                ValidateMoveAndBeatChecker validate = new ValidateMoveAndBeatChecker(board);
                DataQueenMoveAndBeat parametrs = new DataQueenMoveAndBeat(board, clickQueen, keyClickCell);

                board = validate.MoveAndBeatChecker(parametrs);
                board = AnaliseMoveAndBeatQueen(board, keyClickCell.Value);

                TeamCheckers.PlayerMove = TeamCheckers.setTeam(TeamCheckers.PlayerMove);
                foreach (var cell in board)
                {
                    board[cell.Key].CanMove = false;
                }
                if (board.Where(n => n.Value.CanAttack).Any())
                {
                    TeamCheckers.PlayerMove = TeamCheckers.setTeam(TeamCheckers.PlayerMove);
                    board[keyClickCell.Key].ClickChecker = true;
                }
            }

            return board;
        }

        public Dictionary<string, Cell> AnaliseMoveAndBeatQueen(Dictionary<string, Cell> board, Cell clickQueen)
        {
            if (clickQueen.Checker?.Team != TeamCheckers.PlayerMove)
                return board;

            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false;
                board[cell.Key].ClickChecker = false;
                board[cell.Key].CanAttack = false;
            }

            board[clickQueen.Checker.InCellId].ClickChecker = true;

            DataCalculateQueenMoveAndBeat data = new DataCalculateQueenMoveAndBeat(board, clickQueen);
            ValidateCalculateMoveAndBeatQueen validate = new ValidateCalculateMoveAndBeatQueen(board);

            board = validate.CanPossibleMoveAndAttack(data);

            var canAttack = board.FirstOrDefault(n => n.Value.CanAttack);

            if (canAttack.Value != null)
            {
                foreach (var cell in board)
                {
                    board[cell.Key].CanMove = false;
                }
            }

            return board;
        }
    }
}
