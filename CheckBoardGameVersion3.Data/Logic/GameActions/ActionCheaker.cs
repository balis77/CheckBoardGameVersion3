using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using ConsoleApp2.Logic.GameActions.Contracts;

namespace ConsoleApp2.Logic.GameActions
{
    public class ActionCheaker //: IActionCheaker
    {
        public SetTeam Team { get; set; } = SetTeam.White;

        private ActionCheckerValidator _actionCheckerValidator;
        public ActionCheaker()
        {
            _actionCheckerValidator = new ActionCheckerValidator();
        }
        public Dictionary<string, Cell> Battle(Dictionary<string, Cell> board, Cell clickCell)
        {
            
            
            if (clickCell.Checker != null)
            {
                return board;
            }
           
            string clickChecker = string.Empty;

            foreach (var cell in board)
            {
                if (cell.Value.ClickChecker == true)
                {
                    clickChecker = cell.Key;
                }

            }
            Cell moveChecker = default;
            if (clickChecker != "")
            {
                moveChecker = board[clickChecker];
            }

            var keyclickCell = board.FirstOrDefault(n => n.Value.X == clickCell.X && n.Value.Y == clickCell.Y).Key;
            if (clickCell.CanMove)
            {
                board = BoardMove(board, moveChecker, keyclickCell);
            }
            if (clickCell.CanAttack)
            {
                int jumpedRow = (moveChecker.X + clickCell.X) / 2;
                int jumpedColumn = (moveChecker.Y + clickCell.Y) / 2;
                var keyCheckerBeat = board.FirstOrDefault(n => n.Value.X == jumpedRow && n.Value.Y == jumpedColumn).Key;
                board[keyCheckerBeat].Checker = null;
                board = BoardMove(board, moveChecker, keyclickCell);
            }
            foreach (var item in board)
            {
                board[item.Key].CanMove = false;
                board[item.Key].ClickChecker = false;
                board[item.Key].CanAttack = false;
            }
            Team = _actionCheckerValidator.setTeam(Team);
            return board;

        }

        private static void NewMethod(Dictionary<string, Cell> board, Cell moveChecker, string keyclickCell)
        {
            board[keyclickCell].Checker = new Checker(keyclickCell, moveChecker.Checker.Color, moveChecker.Checker.Team);
            board[moveChecker.Checker.InCellId].Checker = null;
        }

        private Dictionary<string, Cell> BoardMove(Dictionary<string, Cell> board, Cell moveChecker, string keyclickCell)
        {
            board[keyclickCell].Checker = new Checker(keyclickCell, moveChecker.Checker.Color, moveChecker.Checker.Team);
            board[moveChecker.Checker.InCellId].Checker = null;
            return board;
        }

        public Dictionary<string, Cell> AnaliseCanMove(Dictionary<string, Cell> board, Cell clickCell)
        {
            if (clickCell.Checker.Team != Team)
            {
                return board;
            }
            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false;
                board[cell.Key].ClickChecker = false;
                board[cell.Key].CanAttack = false;
            }
            var clickChecker = clickCell.Checker;
            if (clickChecker != null)
            {
                if (clickCell.Checker == null)
                {
                    return board;
                }
                board[clickCell.Checker.InCellId].ClickChecker = true;

                int moveCheckerRow = clickChecker.Color == CheckerColor.White ? -1 : 1;

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


                int rowCheckMove = rowCheck + (rowCheck - clickCell.X);
                int rowBackCheckBeat = rowBackCheck + (rowBackCheck - clickCell.X);
                int columnLeftPossibleMove = columnLeft + (columnLeft - clickCell.Y);
                int columnRightPossibleMove = columnRight + (columnRight - clickCell.Y);

                board = _actionCheckerValidator.ValidatePossibleBeatTheChecker(board, rowCheckMove, columnLeftPossibleMove, LeftPossisionCell, columnLeft);
                board = _actionCheckerValidator.ValidatePossibleBeatTheChecker(board, rowCheckMove, columnRightPossibleMove, RigthPossicionCell, columnRight);

                bool borderBoardBackRow = _actionCheckerValidator.ValidaiteBackDask(rowBackCheck);

                if (borderBoardBackRow)
                    return board;
                
                board = _actionCheckerValidator.ValidatePossibleBeatTheChecker(board, rowBackCheckBeat, columnLeftPossibleMove, leftPossicionBackCell, columnLeft);
                board = _actionCheckerValidator.ValidatePossibleBeatTheChecker(board, rowBackCheckBeat, columnRightPossibleMove, RightpossicionBackCell, columnRight);
            }
            return board;
        }

    }
}
