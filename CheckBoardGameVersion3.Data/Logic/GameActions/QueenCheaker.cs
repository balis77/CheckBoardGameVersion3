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
