using CheckBoardGameVersion3.Data.Models;

namespace CheckBoardGameVersion3.Data.Logic.Validate.ValidateQueen
{
    public class ValidateCalculateMoveAndBeatQueen
    {
        private Dictionary<string, Cell> _board;
        private ValidateCheckerQueen _validateCheckerQueen;
        public ValidateCalculateMoveAndBeatQueen(Dictionary<string, Cell> board)
        {
            _board = board;
            _validateCheckerQueen = new ValidateCheckerQueen();
        }
        public Dictionary<string, Cell> CanPossibleMoveAndAttack(DataCalculateQueenMoveAndBeat data)
        {
            BottomRight(data);
            TopLeft(data);
            BottomLeft(data);
            TopRight(data);

            return _board;
        }
        private void TopRight(DataCalculateQueenMoveAndBeat data)
        {
            for (int count = 1; data.clickQueen.X - count >= 0 && data.clickQueen.Y + count <= 8; count++)
            {
                int row = data.clickQueen.X - count;
                int column = data.clickQueen.Y + count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(data.board, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                        break;

                    for (int counterBeat = 1; nextCellMove.Value.X - counterBeat >= 0 && nextCellMove.Value.Y + counterBeat <= 8; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X - counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y + counterBeat;
                        data.board = _validateCheckerQueen.PossbileBeatChecker(data.board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    data.board = _validateCheckerQueen.MoveQueenPossible(data.board, nextCellMove, data.clickQueen);
                }
            }

            _board = data.board;
        }

        private void BottomLeft(DataCalculateQueenMoveAndBeat data)
        {
            for (int count = 1; data.clickQueen.X + count <= 8 && data.clickQueen.Y - count >= 0; count++)
            {
                int row = data.clickQueen.X + count;
                int column = data.clickQueen.Y - count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(data.board, row, column);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                        break;

                    for (int counterBeat = 1; nextCellMove.Value.X + counterBeat <= 8 && nextCellMove.Value.Y - counterBeat >= 0; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X + counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y - counterBeat;
                        data.board = _validateCheckerQueen.PossbileBeatChecker(data.board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    data.board = _validateCheckerQueen.MoveQueenPossible(data.board, nextCellMove, data.clickQueen);
                }
            }

            _board = data.board;
        }

        private void TopLeft(DataCalculateQueenMoveAndBeat data)
        {
            for (int count = 1; data.clickQueen.X - count >= 0 && data.clickQueen.Y - count >= 0; count++)
            {
                int row = data.clickQueen.X - count;
                int column = data.clickQueen.Y - count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(data.board, row, column);
                var checkerClick = data.board.FirstOrDefault(n => n.Value.ClickChecker == true);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                        break;

                    for (int counterBeat = 1; nextCellMove.Value.X - counterBeat >= 0 && nextCellMove.Value.Y - counterBeat >= 0; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X - counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y - counterBeat;
                        data.board = _validateCheckerQueen.PossbileBeatChecker(data.board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    data.board = _validateCheckerQueen.MoveQueenPossible(data.board, nextCellMove, data.clickQueen);
                }
            }

            _board = data.board;
        }

        private void BottomRight(DataCalculateQueenMoveAndBeat data)
        {
            for (int count = 1; data.clickQueen.X + count <= 8 && data.clickQueen.Y + count <= 8; count++)
            {

                int row = data.clickQueen.X + count;
                int column = data.clickQueen.Y + count;

                KeyValuePair<string, Cell> nextCellMove = _validateCheckerQueen.GetCell(data.board, row, column);
                var checkerClick = data.board.FirstOrDefault(n => n.Value.ClickChecker == true);
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                        break;

                    for (int counterBeat = 1; nextCellMove.Value.X + counterBeat <= 8 && nextCellMove.Value.Y + counterBeat <= 8; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X + counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y + counterBeat;
                        data.board = _validateCheckerQueen.PossbileBeatChecker(data.board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    data.board = _validateCheckerQueen.MoveQueenPossible(data.board, nextCellMove, data.clickQueen);
                }

            }

            _board = data.board;
        }
    }
}
