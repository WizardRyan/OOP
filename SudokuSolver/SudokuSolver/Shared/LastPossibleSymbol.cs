namespace SudokuSolver.Shared
{
    public class LastPossibleSymbol : CellAlgorithm
    {
        public override void DoTechnique(Puzzle puzzle, int row, int column)
        {
            var foundSymbols = new List<string>();

            AddSymbols(foundSymbols, puzzle.GameBoard[row]);
            AddSymbols(foundSymbols, puzzle.GetColumn(column));
            AddSymbols(foundSymbols, puzzle.GetBlock(row, column));

            if(foundSymbols.Count == puzzle.Symbols.Count - 1)
            {
                foreach(var symbol in puzzle.Symbols) {
                    if (!foundSymbols.Contains(symbol))
                    {
                        puzzle.GameBoard[row][column].Value = symbol;
                        break;
                    }
                }
            }
        }

        private void AddSymbols(List<string> foundSymbols, List<Puzzle.Cell> cells)
        {
            foreach (var cell in cells)
            {
                if (cell.Value != null && !foundSymbols.Contains(cell.Value)) foundSymbols.Add(cell.Value);
            }
        }
    }
}
