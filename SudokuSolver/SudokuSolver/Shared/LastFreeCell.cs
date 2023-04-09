namespace SudokuSolver.Shared
{
    public class LastFreeCell : CellAlgorithm
    {
        public override void DoTechnique(Puzzle puzzle, int row, int column)
        {
            if (puzzle.GetOnlyCellsWithValues(puzzle.GameBoard[row]).Count == puzzle.GameBoard.Count - 1)
            {
                foreach(var symbol in puzzle.Symbols)
                {
                    if(!puzzle.ValueInRow(symbol, row))
                    {
                        puzzle.GameBoard[row][column].Value = symbol;
                        break;
                    }
                }
            }
            else if(puzzle.GetOnlyCellsWithValues(puzzle.GetColumn(column)).Count == puzzle.GameBoard.Count - 1)
            {
                foreach (var symbol in puzzle.Symbols)
                {
                    if (!puzzle.ValueInColumn(symbol, row))
                    {
                        puzzle.GameBoard[row][column].Value = symbol;
                        break;
                    }
                }
            }
            else if(puzzle.GetOnlyCellsWithValues(puzzle.GetBlock(row, column)).Count == puzzle.GameBoard.Count - 1)
            {
                foreach(var symbol in puzzle.Symbols)
                {
                    if (!puzzle.ValueInBlock(symbol, row, column))
                    {
                        puzzle.GameBoard[row][column].Value = symbol;
                        break;
                    }
                }
            }
        }
    }
}
