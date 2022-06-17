using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions.Contracts;

namespace ConsoleApp2.Logic.GameActions
{
    public class QueenCheaker //: IQueenCheaker
    {
        private ValidateCheckerQueen _validateCheckerQueen;
        public QueenCheaker()
        {
            _validateCheckerQueen = new ValidateCheckerQueen();
        }
        /// <summary>
        /// поверх создай метод який буде провіряти чи клікнули на дамку якщо да то кидає в цей метод
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Dictionary<string, Cell> Battle(Dictionary<string, Cell> board, Cell clickQueen)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, Cell> MoveQueen(Dictionary<string, Cell> board, Cell clickQueen)
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


            //if ((board[_keyBoard].Checker?.Color == CheckBoardGameVersion3.Data.Models.Enums.CheckerColor.BlackKing && TeamCheckers.Team==SetTeam.Black) ||
            //    board[_keyBoard].Checker?.Color == CheckBoardGameVersion3.Data.Models.Enums.CheckerColor.WhiteKing && TeamCheckers.Team == SetTeam.White)

            int xQueen = clickQueen.X;
            int yQueen = clickQueen.Y;
            board = AnaliseMoveQueen(board, clickQueen);
            return board;
        }

        private Dictionary<string, Cell> AnaliseMoveQueen(Dictionary<string, Cell> board, Cell clickQueen)
        {

            int xQueen = clickQueen.X;
            int yQueen = clickQueen.Y;
            for (int count = 1; xQueen + count <= 8 && yQueen + count <= 8; count++)
            {

                int row = xQueen + count;
                int column = yQueen + count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    break;
                }
                board = _validateCheckerQueen.MoveQueenPossible(board, nextCellMove, clickQueen);
            }
            for (int count = 1; xQueen - count >= 0 && yQueen - count >= 0; count++)
            {
                int row = xQueen - count;
                int column = yQueen - count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    break;
                }
                board = _validateCheckerQueen.MoveQueenPossible(board, nextCellMove, clickQueen);
            }
            for (int count = 1; xQueen + count <= 8 && yQueen - count >= 0; count++)
            {
                int row = xQueen + count;
                int column = yQueen - count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    break;
                }
                board = _validateCheckerQueen.MoveQueenPossible(board, nextCellMove, clickQueen);
            }
            for (int count = 1; xQueen - count >= 0 && yQueen + count <= 8; count++)
            {
                int row = xQueen - count;
                int column = yQueen + count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    break;
                }
                board = _validateCheckerQueen.MoveQueenPossible(board, nextCellMove, clickQueen);
            }

            return board;
        }

    }
}
