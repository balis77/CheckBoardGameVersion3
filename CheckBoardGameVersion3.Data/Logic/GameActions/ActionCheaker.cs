using CheckBoardGameVersion3.Data.Logic.Bot;
using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Logic.Validate.ValidateBoard;
using CheckBoardGameVersion3.Data.Logic.Validate.ValidateChecker;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions.Contracts;
using System.Linq;

namespace ConsoleApp2.Logic.GameActions
{
    public class ActionCheaker : IActionCheaker
    {
        private const int BOARD_END = 7;
        private const int BOARD_START = 0;
        private ActionCheckerValidator _actionCheckerValidator;

        public ActionCheaker()
        {
            _actionCheckerValidator = new ActionCheckerValidator();

        }
        public Dictionary<string, Cell> MoveAndBeatCheckers(Dictionary<string, Cell> board, Cell clickCell)
        {
            
            if (!clickCell.CanMove && !clickCell.CanAttack)
            {
                foreach (var cell in board)
                {
                    board[cell.Key].CanMove = false;
                    board[cell.Key].ClickChecker = false;
                    board[cell.Key].CanAttack = false;
                }
                return board;
            }

            string clickChecker = string.Empty;

            foreach (var cell in board)
            {
                if (cell.Value.ClickChecker)
                {
                    clickChecker = cell.Key;
                }

            }

            Cell moveChecker = new Cell();

            if (clickChecker != string.Empty)
            {
                moveChecker = board[clickChecker];
            }

            var keyclickCell = _actionCheckerValidator.GetCell(board, clickCell.X, clickCell.Y);

            
            if (clickCell.CanMove)
            {
                board = _actionCheckerValidator.CheckerMove(board, moveChecker, keyclickCell.Key);
                TeamCheckers.PlayerMove = TeamCheckers.setTeam(TeamCheckers.PlayerMove);
                
                return board;
            }

            if (clickCell.CanAttack)
            {
                int jumpedRow = (moveChecker.X + clickCell.X) / 2;
                int jumpedColumn = (moveChecker.Y + clickCell.Y) / 2;

                var keyCheckerBeat = _actionCheckerValidator.GetCell(board, jumpedRow, jumpedColumn);

                board[keyCheckerBeat.Key].Checker = null;

                board = _actionCheckerValidator.CheckerMove(board, moveChecker, keyclickCell.Key);

                board = AnaliseCanMoveAndBeat(board, board[keyclickCell.Key]);
                TeamCheckers.PlayerMove = TeamCheckers.setTeam(TeamCheckers.PlayerMove);

                foreach (var cell in board)
                {
                    board[cell.Key].CanMove = false;

                    if (cell.Value.CanAttack)
                    {
                        TeamCheckers.PlayerMove = TeamCheckers.setTeam(TeamCheckers.PlayerMove);
                        board[keyclickCell.Key].ClickChecker = true;
                    }
                }
            }
            return board;
        }


        public Dictionary<string, Cell> AnaliseCanMoveAndBeat(Dictionary<string, Cell> board, Cell clickChecker)
        {
            if (clickChecker.Checker?.Team != TeamCheckers.PlayerMove)
                return board;

            foreach (var cell in board)
            {
                board[cell.Key].CanMove = false;
                board[cell.Key].ClickChecker = false;
                board[cell.Key].CanAttack = false;
            }

            if (clickChecker.Checker == null)
                return board;

            if (clickChecker.X == BOARD_END && (clickChecker.Checker.Color == CheckerColor.Black))
            {
                board[clickChecker.Checker.InCellId].Checker.Color = CheckerColor.BlackQueen;
            }

            if (clickChecker.X == BOARD_START && (clickChecker.Checker.Color == CheckerColor.White))
            {
                board[clickChecker.Checker.InCellId].Checker.Color = CheckerColor.WhiteQueen;
            }

            board[clickChecker.Checker.InCellId].ClickChecker = true;

            int moveCheckerRow = clickChecker.Checker.Color == CheckerColor.White ? -1 : 1;
            int rowCheck = clickChecker.X + moveCheckerRow;

            bool BorderBoardRow = _actionCheckerValidator.ValidaiteBackDask(rowCheck);

            if (BorderBoardRow)
                return board;

            board = MovelogicChecker(board, clickChecker);
            board = BeatLogicChecker(board, clickChecker);

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

        private Dictionary<string, Cell> BeatLogicChecker(Dictionary<string, Cell> board, Cell clickChecker)
        {
            int moveCheckerRow = clickChecker.Checker?.Color == CheckerColor.White ? -1 : 1;
            int rowCheck = clickChecker.X + moveCheckerRow;
            int columnLeft = clickChecker.Y - 1;
            int columnRight = clickChecker.Y + 1;

            int beatCheckerRow = clickChecker.Checker?.Color == CheckerColor.White ? 1 : -1;
            int rowBackCheck = clickChecker.X + beatCheckerRow;

            List<int> rowsPossible = new List<int>();
            rowsPossible.Add(rowCheck);
            rowsPossible.Add(rowBackCheck);

            foreach (var row in rowsPossible)
            {

                board = _actionCheckerValidator.BeatChecker(board, clickChecker, row, columnLeft);
                board = _actionCheckerValidator.BeatChecker(board, clickChecker, row, columnRight);
            }
            return board;
        }

        private Dictionary<string, Cell> MovelogicChecker(Dictionary<string, Cell> board, Cell clickChecker)
        {
            int moveCheckerRow = clickChecker.Checker?.Color == CheckerColor.White ? -1 : 1;

            int rowCheck = clickChecker.X + moveCheckerRow;
            int columnLeft = clickChecker.Y - 1;
            int columnRight = clickChecker.Y + 1;

            KeyValuePair<string, Cell> leftPossisionCell = _actionCheckerValidator.GetCell(board, rowCheck, columnLeft);
            KeyValuePair<string, Cell> rigthPossicionCell = _actionCheckerValidator.GetCell(board, rowCheck, columnRight);

            bool borderBoardColumnLeft = _actionCheckerValidator.ValidaiteBackDask(columnLeft);
            bool borderBoardColumnRight = _actionCheckerValidator.ValidaiteBackDask(columnRight);

            if (!borderBoardColumnLeft)
            {
                board = _actionCheckerValidator.MoveChecker(board, leftPossisionCell);
            }

            if (!borderBoardColumnRight)
            {
                board = _actionCheckerValidator.MoveChecker(board, rigthPossicionCell);

            }
            return board;
        }
    }
}
