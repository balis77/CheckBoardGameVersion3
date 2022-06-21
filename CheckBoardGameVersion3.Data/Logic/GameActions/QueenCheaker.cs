using CheckBoardGameVersion3.Data.Logic.Validate;
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
            }
            if (clickCell.CanMove)
            {
                board = _validateCheckerQueen.QueenMove(board, clickQueen, keyClickCell.Key);
                TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);
            }
            return board;
        }
        #region PrivateMethods BeatChecker
       
        #endregion
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

        //private Dictionary<string, Cell> AnaliseCanPossibleMoveAndAttack(Dictionary<string, Cell> board, Cell clickQueen)
        //{

        //    int xQueen = clickQueen.X;
        //    int yQueen = clickQueen.Y;
        //    //bottom right
        //    //board = BottomRight(board, clickQueen);
        //    ////top left
        //    //board = TopLeft(board, clickQueen);
        //    ////bottom left
        //    //board = BottomLeft(board, clickQueen);
        //    ////top right
        //    //board = TopRight(board, clickQueen);
        //    var canAttack = board.FirstOrDefault(n => n.Value.CanAttack == true);
        //    if (canAttack.Value != null)
        //    {
        //        foreach (var cell in board)
        //        {
        //            board[cell.Key].CanMove = false;
        //        }
        //    }

        //    return board;
        //}

       

       
    }
}
