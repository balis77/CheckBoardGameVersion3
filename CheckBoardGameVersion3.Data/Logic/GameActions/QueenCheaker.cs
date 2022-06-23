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
                if (cell.Value.ClickChecker == true)
                {
                    clickCheckerKey = cell.Key;
                }

            }
            Cell clickQueen = new Cell();
            if (clickCheckerKey != "")
            {
                clickQueen = board[clickCheckerKey];
            }

            KeyValuePair<string, Cell> keyClickCell = _validateCheckerQueen.GetCell(board, clickCell.X, clickCell.Y);
            if (clickCell.CanAttack)
            {
                ValidateMoveAndBeatChecker validate = new ValidateMoveAndBeatChecker(board);
                DataQueenMoveAndBeat parametrs = new DataQueenMoveAndBeat(board, clickQueen, keyClickCell);
                board = validate.MoveAndBeatChecker(parametrs);
                board = AnaliseMoveAndBeatQueen(board, keyClickCell.Value);
                //var biba = board.FirstOrDefault(n => n.Value.CanAttack == true);
                //if (biba.Key == null)
                //{
                //    TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);
                //}
                TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);

            }
            if (clickCell.CanMove)
            {
                board = _validateCheckerQueen.QueenMove(board, clickQueen, keyClickCell.Key);
                    TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);

            }
            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false;

                if (cell.Value.CanAttack == true)
                {
                    TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);
                    board[keyClickCell.Key].ClickChecker = true;
                }
            }
            return board;
        }
    
        public Dictionary<string, Cell> AnaliseMoveAndBeatQueen(Dictionary<string, Cell> board, Cell clickQueen)
        {
            if (clickQueen.Checker?.Team != TeamCheckers.Team)
                return board;

            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false;
                board[cell.Key].ClickChecker = false;
                board[cell.Key].CanAttack = false;
            }
            board[clickQueen.Checker.InCellId].ClickChecker = true;
            DataCalculateQueenMoveAndBeat data = new DataCalculateQueenMoveAndBeat(board,clickQueen);
            ValidateCalculateMoveAndBeatQueen validate = new ValidateCalculateMoveAndBeatQueen(board);
            board = validate.CanPossibleMoveAndAttack(data);
           
            var canAttack = board.FirstOrDefault(n => n.Value.CanAttack == true);
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
