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
            for (int count = 1; data.ClickQueen.X - count >= 0 && data.ClickQueen.Y + count <= 8; count++)
            {
                bool oneMoreBeatChecker = false;

                int row = data.ClickQueen.X - count;
                int column = data.ClickQueen.Y + count;
                
                var nextCellMove = _validateCheckerQueen.GetCell(data.Board, row, column);

                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                        break;

                    for (int counterBeat = 1; nextCellMove.Value.X - counterBeat >= 0 && nextCellMove.Value.Y + counterBeat <= 8; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X - counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y + counterBeat;

                        var nextBeatCell = _validateCheckerQueen.GetCell(data.Board, rowCheckBeat, columnCheckBeat);

                        if (nextBeatCell.Value?.Checker != null)
                        {
                            oneMoreBeatChecker = true;
                            break;
                        }

                        data.Board = _validateCheckerQueen.PossbileBeatChecker(data.Board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    data.Board = _validateCheckerQueen.MoveQueenPossible(data.Board, nextCellMove, data.ClickQueen);
                }

                if (oneMoreBeatChecker)
                {
                    break;
                }
            }

            _board = data.Board;
        }

        private void BottomLeft(DataCalculateQueenMoveAndBeat data)
        {
            for (int count = 1; data.ClickQueen.X + count <= 8 && data.ClickQueen.Y - count >= 0; count++)
            {
                bool oneMoreBeatChecker = false;

                int row = data.ClickQueen.X + count;
                int column = data.ClickQueen.Y - count;
                
                var nextCellMove = _validateCheckerQueen.GetCell(data.Board, row, column);
               
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                        break;
                    for (int counterBeat = 1; nextCellMove.Value.X + counterBeat <= 8 && nextCellMove.Value.Y - counterBeat >= 0; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X + counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y - counterBeat;

                        var nextBeatCell = _validateCheckerQueen.GetCell(data.Board, rowCheckBeat, columnCheckBeat);

                        if (nextBeatCell.Value?.Checker != null)
                        {
                            oneMoreBeatChecker = true;
                            break;
                        }

                        data.Board = _validateCheckerQueen.PossbileBeatChecker(data.Board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    data.Board = _validateCheckerQueen.MoveQueenPossible(data.Board, nextCellMove, data.ClickQueen);
                }

                if (oneMoreBeatChecker)
                {
                    break;
                }
            }

            _board = data.Board;
        }

        private void TopLeft(DataCalculateQueenMoveAndBeat data)
        {
            for (int count = 1; data.ClickQueen.X - count >= 0 && data.ClickQueen.Y - count >= 0; count++)
            {
                bool oneMoreBeatChecker = false;
                int row = data.ClickQueen.X - count;
                int column = data.ClickQueen.Y - count;
                
                var nextCellMove = _validateCheckerQueen.GetCell(data.Board, row, column);

                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                        break;

                    for (int counterBeat = 1; nextCellMove.Value.X - counterBeat >= 0 && nextCellMove.Value.Y - counterBeat >= 0; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X - counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y - counterBeat;
                        KeyValuePair<string, Cell> nextBeatCell = _validateCheckerQueen.GetCell(data.Board, rowCheckBeat, columnCheckBeat);
                        if (nextBeatCell.Value?.Checker != null)
                        {
                            oneMoreBeatChecker = true;
                            break;
                        }
                        data.Board = _validateCheckerQueen.PossbileBeatChecker(data.Board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    data.Board = _validateCheckerQueen.MoveQueenPossible(data.Board, nextCellMove, data.ClickQueen);
                }

                if (oneMoreBeatChecker)
                {
                    break;
                }
            }

            _board = data.Board;
        }

        private void BottomRight(DataCalculateQueenMoveAndBeat data)
        {
            for (int count = 1; data.ClickQueen.X + count <= 8 && data.ClickQueen.Y + count <= 8; count++)
            {
                bool OneMoreBeatChecker = false;

                int row = data.ClickQueen.X + count;
                int column = data.ClickQueen.Y + count;

                var nextCellMove = _validateCheckerQueen.GetCell(data.Board, row, column);
               
                if (nextCellMove.Value?.Checker != null)
                {
                    if (nextCellMove.Value?.Checker.Team == TeamCheckers.Team)
                        break;

                    for (int counterBeat = 1; nextCellMove.Value.X + counterBeat <= 8 && nextCellMove.Value.Y + counterBeat <= 8; counterBeat++)
                    {
                        int rowCheckBeat = nextCellMove.Value.X + counterBeat;
                        int columnCheckBeat = nextCellMove.Value.Y + counterBeat;
                        KeyValuePair<string, Cell> nextBeatCell = _validateCheckerQueen.GetCell(data.Board, rowCheckBeat, columnCheckBeat);
                        if (nextBeatCell.Value?.Checker != null)
                        {
                            OneMoreBeatChecker = true;
                            break;
                        }
                        data.Board = _validateCheckerQueen.PossbileBeatChecker(data.Board, rowCheckBeat, columnCheckBeat);
                    }
                }
                else
                {
                    data.Board = _validateCheckerQueen.MoveQueenPossible(data.Board, nextCellMove, data.ClickQueen);
                }

                if (OneMoreBeatChecker)
                {
                    break;
                }
            }

            _board = data.Board;
        }
    }
}
