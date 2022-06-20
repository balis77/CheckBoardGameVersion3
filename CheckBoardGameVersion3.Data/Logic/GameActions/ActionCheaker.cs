using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions.Contracts;
using System.Linq;

namespace ConsoleApp2.Logic.GameActions
{
    public class ActionCheaker : IActionCheaker
    {
        private ActionCheckerValidator _actionCheckerValidator;
        public ActionCheaker()
        {
            _actionCheckerValidator = new ActionCheckerValidator();
        }
        public Dictionary<string, Cell> MoveAndBeatCheckers(Dictionary<string, Cell> board, Cell clickCell)
        {
            string clickChecker = string.Empty;

            foreach (var cell in board)
            {
                if (cell.Value.ClickChecker == true)
                {
                    clickChecker = cell.Key;
                }

            }
            Cell moveChecker = new Cell();
            if (clickChecker != "")
            {
                moveChecker = board[clickChecker];
            }

            var keyclickCell = _actionCheckerValidator.GetCell(board, clickCell.X, clickCell.Y);

            if (clickCell.CanMove)
            {
                board = _actionCheckerValidator.CheckerMove(board, moveChecker, keyclickCell.Key);
                TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);

            }
            if (clickCell.CanAttack)
            {
                int jumpedRow = (moveChecker.X + clickCell.X) / 2;
                int jumpedColumn = (moveChecker.Y + clickCell.Y) / 2;
                var keyCheckerBeat = _actionCheckerValidator.GetCell(board, jumpedRow, jumpedColumn);

                board[keyCheckerBeat.Key].Checker = null;
                board = _actionCheckerValidator.CheckerMove(board, moveChecker, keyclickCell.Key);
                board = AnaliseCanMoveAndBeat(board, clickCell);
                TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);
            }
            bool canAgainAttack = false;

            foreach (var cell in board)
            {
                if (cell.Value.CanAttack == true)
                {
                    canAgainAttack = true;
                }
                board[cell.Key].CanMove = false;
            }
            if (!canAgainAttack)
            {
                return board;
            }

            return board;

        }


        public Dictionary<string, Cell> AnaliseCanMoveAndBeat(Dictionary<string, Cell> board, Cell clickCell)
        {


            if (clickCell.Checker?.Team != TeamCheckers.Team)
                return board;

            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false;
                board[cell.Key].ClickChecker = false;
                board[cell.Key].CanAttack = false;
            }


            if (clickCell.Checker == null)
                return board;
            if (clickCell.X == 7 && (clickCell.Checker.Color == CheckerColor.Black))
            {
                board[clickCell.Checker.InCellId].Checker.Color = CheckerColor.BlackKing;
            }
            if (/*clickCell.X == 0 && */(clickCell.Checker.Color == CheckerColor.White))
            {
                board[clickCell.Checker.InCellId].Checker.Color = CheckerColor.WhiteKing;
            }
            board[clickCell.Checker.InCellId].ClickChecker = true;

            int moveCheckerRow = clickCell.Checker.Color == CheckerColor.White ? -1 : 1;

            int rowCheck = clickCell.X + moveCheckerRow;
            int columnLeft = clickCell.Y - 1;
            int columnRight = clickCell.Y + 1;

            bool BorderBoardRow = _actionCheckerValidator.ValidaiteBackDask(rowCheck);
            if (BorderBoardRow)
                return board;

            KeyValuePair<string, Cell> LeftPossisionCell = _actionCheckerValidator.GetCell(board, rowCheck, columnLeft);
            KeyValuePair<string, Cell> RigthPossicionCell = _actionCheckerValidator.GetCell(board, rowCheck, columnRight);

            bool BorderBoardColumnLeft = _actionCheckerValidator.ValidaiteBackDask(columnLeft);
            bool BorderBoardColumnRight = _actionCheckerValidator.ValidaiteBackDask(columnRight);

            board = _actionCheckerValidator.MoveChecker(board, LeftPossisionCell, BorderBoardColumnLeft);
            board = _actionCheckerValidator.MoveChecker(board, RigthPossicionCell, BorderBoardColumnRight);


            int beatCheckerRow = clickCell.Checker.Color == CheckerColor.White ? 1 : -1;
            int rowBackCheck = clickCell.X + beatCheckerRow;

            var leftPossicionBackCell = _actionCheckerValidator.GetCell(board, rowBackCheck, columnLeft);
            var RightpossicionBackCell = _actionCheckerValidator.GetCell(board, rowBackCheck, columnRight);


            int rowCheckBeat = rowCheck + (rowCheck - clickCell.X);
            int rowBackCheckBeat = rowBackCheck + (rowBackCheck - clickCell.X);
            int columnLeftPossibleMove = columnLeft + (columnLeft - clickCell.Y);
            int columnRightPossibleMove = columnRight + (columnRight - clickCell.Y);


            board = _actionCheckerValidator.ValidatePossibleBeatTheChecker(board, rowCheckBeat, columnLeftPossibleMove, LeftPossisionCell, columnLeft);
            board = _actionCheckerValidator.ValidatePossibleBeatTheChecker(board, rowCheckBeat, columnRightPossibleMove, RigthPossicionCell, columnRight);

            bool borderBoardBackRow = _actionCheckerValidator.ValidaiteBackDask(rowBackCheck);

            if (borderBoardBackRow)
                return board;

            board = _actionCheckerValidator.ValidatePossibleBeatTheChecker(board, rowBackCheckBeat, columnLeftPossibleMove, leftPossicionBackCell, columnLeft);
            board = _actionCheckerValidator.ValidatePossibleBeatTheChecker(board, rowBackCheckBeat, columnRightPossibleMove, RightpossicionBackCell, columnRight);

            var canAttack = board.FirstOrDefault(n=>n.Value.CanAttack == true);
            
            
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
