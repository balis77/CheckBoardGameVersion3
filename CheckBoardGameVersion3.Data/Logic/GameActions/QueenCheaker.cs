using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions.Contracts;

namespace ConsoleApp2.Logic.GameActions
{
    public class BoardModel
    {
        //Dictionary<string, Cell> board, Cell clickQueen, string keyClickCell, int xQueen, int yQueen, ref Checker RemoveChecker
        public Dictionary<string, Cell> board { get; set; }
        public Cell clickQueen { get; set; }
        public string keyClickCell { get; set; }
        public int xQueen { get; set; }
        public int yQueen { get; set; }
        public BoardModel(Dictionary<string, Cell> _board, Cell _clickQueen, string _keyClickCell, int _xQueen, int _yQueen)
        {
            this.board = _board;
            this.clickQueen = _clickQueen;
            this.keyClickCell = _keyClickCell;
            this.xQueen = _xQueen;
            this.yQueen = _yQueen;
        }
    }
    public class QueenCheaker //: IQueenCheaker
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

            var keyClickCell = _validateCheckerQueen.GetKey(board, clickCell.X, clickCell.Y);

            int xQueen = clickCell.X;
            int yQueen = clickCell.Y;
            Checker RemoveChecker = new Checker();
            if (clickCell.CanAttack)
            {
                BoardModel boardModel = new BoardModel(board, clickQueen, keyClickCell, xQueen, yQueen);
                board = BottonRight(board, clickQueen, keyClickCell, xQueen, yQueen, ref RemoveChecker);
                board = TopLeft(board, clickQueen, keyClickCell, xQueen, yQueen, ref RemoveChecker);
                board = BottonLeft(board, clickQueen, keyClickCell, xQueen, yQueen, ref RemoveChecker);
                board = TopRight(board, clickQueen, keyClickCell, xQueen, yQueen, ref RemoveChecker);
            }
            if (clickCell.CanMove)
            {
                board = _validateCheckerQueen.QueenMove(board, clickQueen, keyClickCell);
                TeamCheckers.Team = TeamCheckers.setTeam(TeamCheckers.Team);
            }
            return board;
        }
        #region PrivateMethods BeatChecker
        private Dictionary<string, Cell> TopRight(Dictionary<string, Cell> board, Cell clickQueen, string keyClickCell, int xQueen, int yQueen, ref Checker RemoveChecker)
        {
            for (int count = 1; xQueen - count >= 0 && yQueen + count <= 8; count++)
            {
                int row = xQueen - count;
                int column = yQueen + count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                bool cordinate = _validateCheckerQueen.CheckerByCordinate(clickQueen, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (!cordinate)
                    {
                        RemoveChecker = nextCellMove.Value?.Checker;
                    }
                }
                if (cordinate)
                {
                    board = _validateCheckerQueen.BeatChecker(board, clickQueen, keyClickCell, RemoveChecker);
                }
            }
            return board;
        }

        private Dictionary<string, Cell> BottonLeft(Dictionary<string, Cell> board, Cell clickQueen, string keyClickCell, int xQueen, int yQueen, ref Checker RemoveChecker)
        {
            for (int count = 1; xQueen + count <= 8 && yQueen - count >= 0; count++)
            {
                int row = xQueen + count;
                int column = yQueen - count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                bool cordinate = _validateCheckerQueen.CheckerByCordinate(clickQueen, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (!cordinate)
                    {
                        RemoveChecker = nextCellMove.Value?.Checker;
                    }
                }
                if (cordinate)
                {
                    board = _validateCheckerQueen.BeatChecker(board, clickQueen, keyClickCell, RemoveChecker);

                }
            }
            return board;
        }

        private Dictionary<string, Cell> TopLeft(Dictionary<string, Cell> board, Cell clickQueen, string keyClickCell, int xQueen, int yQueen, ref Checker RemoveChecker)
        {
            for (int count = 1; xQueen - count >= 0 && yQueen - count >= 0; count++)
            {
                int row = xQueen - count;
                int column = yQueen - count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                bool cordinate = _validateCheckerQueen.CheckerByCordinate(clickQueen, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (!cordinate)
                    {
                        RemoveChecker = nextCellMove.Value?.Checker;
                    }
                }
                if (cordinate)
                {
                    board = _validateCheckerQueen.BeatChecker(board, clickQueen, keyClickCell, RemoveChecker);
                }
            }
            return board;
        }

        private Dictionary<string, Cell> BottonRight(Dictionary<string, Cell> board, Cell clickQueen, string keyClickCell, int xQueen, int yQueen, ref Checker RemoveChecker)
        {
            for (int count = 1; xQueen + count <= 8 && yQueen + count <= 8; count++)
            {
                int row = xQueen + count;
                int column = yQueen + count;
                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                bool cordinate = _validateCheckerQueen.CheckerByCordinate(clickQueen, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (!cordinate)
                    {
                        RemoveChecker = nextCellMove.Value?.Checker;
                    }
                }
                if (cordinate)
                {
                    board = _validateCheckerQueen.BeatChecker(board, clickQueen, keyClickCell, RemoveChecker);
                }
            }
            return board;
        }
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

            board = AnaliseCanPossibleMoveAndAttack(board, clickQueen);

            return board;
        }

        private Dictionary<string, Cell> AnaliseCanPossibleMoveAndAttack(Dictionary<string, Cell> board, Cell clickQueen)
        {

            int xQueen = clickQueen.X;
            int yQueen = clickQueen.Y;
            for (int count = 1; xQueen + count <= 8 && yQueen + count <= 8; count++)
            {

                int row = xQueen + count;
                int column = yQueen + count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                var checkerClick = board.FirstOrDefault(n => n.Value.ClickChecker == true);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                    {
                        break;
                    }
                    for (int counterBeat = 1; nextCellMove.Value.X + counterBeat <= 8 && nextCellMove.Value.Y + counterBeat <= 8; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X + counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y + counterBeat;
                        board = PossbileBeatChecker(board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    board = _validateCheckerQueen.MoveQueenPossible(board, nextCellMove, clickQueen);
                }

            }
            for (int count = 1; xQueen - count >= 0 && yQueen - count >= 0; count++)
            {
                int row = xQueen - count;
                int column = yQueen - count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                var checkerClick = board.FirstOrDefault(n => n.Value.ClickChecker == true);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                    {
                        break;
                    }
                    for (int counterBeat = 1; nextCellMove.Value.X - counterBeat >= 0 && nextCellMove.Value.Y - counterBeat >= 0; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X - counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y - counterBeat;
                        board = PossbileBeatChecker(board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    board = _validateCheckerQueen.MoveQueenPossible(board, nextCellMove, clickQueen);
                }
            }
            for (int count = 1; xQueen + count <= 8 && yQueen - count >= 0; count++)
            {
                int row = xQueen + count;
                int column = yQueen - count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                    {
                        break;
                    }
                    for (int counterBeat = 1; nextCellMove.Value.X + counterBeat <= 8 && nextCellMove.Value.Y - counterBeat >= 0; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X + counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y - counterBeat;
                        board = PossbileBeatChecker(board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    board = _validateCheckerQueen.MoveQueenPossible(board, nextCellMove, clickQueen);
                }
            }
            for (int count = 1; xQueen - count >= 0 && yQueen + count <= 8; count++)
            {
                int row = xQueen - count;
                int column = yQueen + count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(board, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                    {
                        break;
                    }
                    for (int counterBeat = 1; nextCellMove.Value.X - counterBeat >= 0 && nextCellMove.Value.Y + counterBeat <= 8; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X - counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y + counterBeat;
                        board = PossbileBeatChecker(board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    board = _validateCheckerQueen.MoveQueenPossible(board, nextCellMove, clickQueen);
                }
            }

            return board;
        }
        private Dictionary<string, Cell> PossbileBeatChecker(Dictionary<string, Cell> board, int rowCheckBeat, int columnCheckBeat)
        {
            KeyValuePair<string, Cell> nextBeatCell = _validateCheckerQueen.GetCell(board, rowCheckBeat, columnCheckBeat);

            if (nextBeatCell.Value?.Checker == null && nextBeatCell.Key != null)
            {
                board[nextBeatCell.Key].CanAttack = true;
            }
            return board;
        }
    }
}
