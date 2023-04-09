namespace SudokuSolver.Shared
{
    public abstract class CellAlgorithm
    {
        //Simple implementation of template method design pattern. DoTechnique will be overidden in inherited classes
        public bool TrySolve(Puzzle puzzle, int row, int column)
        {
            DoTechnique(puzzle, row, column);
            return SolutionWasFound(puzzle, row, column);
        }

        public abstract void DoTechnique(Puzzle puzzle, int row, int column);
        public bool SolutionWasFound(Puzzle puzzle, int row, int column)
        {
            return puzzle.GameBoard[row][column].Value != null;
        }
    }
}
