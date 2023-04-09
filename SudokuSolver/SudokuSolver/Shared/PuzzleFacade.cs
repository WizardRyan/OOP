namespace SudokuSolver.Shared
{
    //Facade design pattern, no need to expose the complexity of the Puzzle class, it's generation, and it's utility methods to the UI
    public class PuzzleFacade
    {
        public Puzzle Puzzle;
        public PuzzleFacade(string inputData)
        {
            Puzzle = PuzzleStringAdapter.StringToPuzzle(inputData);
        }

        public bool Solve()
        {
            return Puzzle.Solve();
        }

        public string GetStringRepresentation()
        {
            return PuzzleStringAdapter.PuzzleToString(Puzzle);
        }

    }
}
