namespace SudokuSolver.Shared
{
    //Adapter design pattern, converts string to Puzzle and vice-versa
    public static class PuzzleStringAdapter
    {
        public static Puzzle StringToPuzzle(string inputData)
        {
            Puzzle puzzle = new Puzzle();
            using (StringReader reader = new StringReader(inputData))
            {
                string? line = string.Empty;
                int lineNum = 0;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        if (lineNum == 1)
                            puzzle.Symbols = line.Split(' ').ToList();
                        else
                        {
                            var rawCellVals = line.Split(' ');
                            if (rawCellVals.Length > 2)
                            {
                                List<Puzzle.Cell> cellRow = new List<Puzzle.Cell>();
                                foreach (var rawCellVal in rawCellVals)
                                {
                                    var cellVal = rawCellVal == "-" ? null : rawCellVal;
                                    cellRow.Add(new Puzzle.Cell(cellVal));
                                }
                                puzzle.GameBoard.Add(cellRow);
                            }
                        }
                        lineNum++;
                    }

                } while (line != null);
            }
            return puzzle;
        }

        public static string PuzzleToString(Puzzle puzzle)
        {
            string str = $"{puzzle.GameBoard.Count}\n";
            for(int i = 0; i < puzzle.Symbols.Count; i++)
            {
                str += puzzle.Symbols[i] + (i == puzzle.Symbols.Count - 1 ? "\n" : " ");
            }
            for(int i = 0; i < puzzle.GameBoard.Count; i++)
            {
                for(int j = 0; j < puzzle.GameBoard.Count; j++)
                {
                    str += (puzzle.GameBoard[i][j].Value == null ? "-" : puzzle.GameBoard[i][j].Value) + (j == puzzle.GameBoard.Count - 1 ? "\n" : " ");
                }
            }
            return str;
        }
    }
}
