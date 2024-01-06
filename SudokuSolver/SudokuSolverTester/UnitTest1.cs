using SudokuSolver.Shared;

namespace SudokuSolverTester
{
    public class UnitTest1
    {

        [Fact]
        public void PuzzleSolvesCorrectly()
        {
            var puzzle = GetPuzzle();
            Assert.True(puzzle.Solve());
            Assert.Equal("4", puzzle.GameBoard[0][1].Value);
            Assert.Equal("2", puzzle.GameBoard[1][2].Value);
            Assert.Equal("2", puzzle.GameBoard[2][3].Value);
            Assert.Equal("4", puzzle.GameBoard[3][0].Value);
        }

        [Fact]
        public void PuzzleChecksSolvedCorrectly()
        {
            var puzzle = GetPuzzle();
            puzzle.Solve();
            Assert.True(puzzle.GameBoardIsFilled());
        }

        [Fact]
        public void PuzzleAdapterOutputsCorrectly()
        {
            var puzzle = GetPuzzle();
            puzzle.Solve();
            string expectedOutput = "4\n1 2 3 4\n2 4 3 1\n1 3 2 4\n3 1 4 2\n4 2 1 3\n";
            Assert.Equal(expectedOutput, PuzzleStringAdapter.PuzzleToString(puzzle));
        }

        [Fact]
        public void PuzzleAdapterReadsInputCorrectly()
        {
            var puzzle = GetPuzzle();
            string input = "4\n1 2 3 4\n2 - 3 1\n1 3 - 4\n3 1 4 -\n- 2 1 3\n";

            var puzzleFromAdapter = PuzzleStringAdapter.StringToPuzzle(input);

            var stringFromPuzzle = PuzzleStringAdapter.PuzzleToString(puzzle);
            var stringFromAdaptedPuzzle = PuzzleStringAdapter.PuzzleToString(puzzleFromAdapter);

            Assert.Equal(stringFromAdaptedPuzzle, stringFromPuzzle);
        }

        [Fact]
        public void TestGetBlock()
        {
            var puzzle = GetPuzzle();
            var block = puzzle.GetBlock(0, 0);
            Assert.Equal("2", block[0].Value);
            Assert.Null(block[1].Value);
            Assert.Equal("1", block[2].Value);
            Assert.Equal("3", block[3].Value);
        }

        [Fact]
        public void TestLastFreeCell()
        {
            var puzzle = GetPuzzle();
            CellAlgorithm algo = new LastFreeCell();
            Assert.True(algo.TrySolve(puzzle, 0, 1));
            Assert.Equal("4", puzzle.GameBoard[0][1].Value);
        }

        [Fact]
        public void TestLastPossibleSymbol()
        {
            var puzzle = GetPuzzle();
            CellAlgorithm algo = new LastPossibleSymbol();
            Assert.True(algo.TrySolve(puzzle, 0, 1));
            Assert.Equal("4", puzzle.GameBoard[0][1].Value);
        }

        [Fact]
        public void TestValueIsValid()
        {
            var puzzle = GetPuzzle();
            Assert.True(puzzle.ValueIsValid("4", 0, 1));
        }


        private Puzzle GetPuzzle()
        {
            var puzzle = new Puzzle();
            puzzle.GameBoard.Add(new List<Puzzle.Cell> {new Puzzle.Cell("2"), new Puzzle.Cell(null),new Puzzle.Cell("3"), new Puzzle.Cell("1") });
            puzzle.GameBoard.Add(new List<Puzzle.Cell>{ new Puzzle.Cell("1"), new Puzzle.Cell("3"), new Puzzle.Cell(null),new Puzzle.Cell("4") });
            puzzle.GameBoard.Add(new List<Puzzle.Cell>{ new Puzzle.Cell("3"), new Puzzle.Cell("1"), new Puzzle.Cell("4"), new Puzzle.Cell(null) });
            puzzle.GameBoard.Add(new List<Puzzle.Cell>{ new Puzzle.Cell(null),new Puzzle.Cell("2"), new Puzzle.Cell("1"), new Puzzle.Cell("3") });

            puzzle.Symbols = new List<string> {"1", "2", "3", "4"};
            return puzzle;
        }
    }
}