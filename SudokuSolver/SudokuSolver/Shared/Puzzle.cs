
namespace SudokuSolver.Shared
{
    public class Puzzle
    {
        public List<string> Symbols {get; set; }
        public List<List<Cell>> GameBoard { get; set; }

        public Puzzle(string inputData)
        {
            Symbols = new List<string>();
            GameBoard = new List<List<Cell>>();

            Console.WriteLine(GameBoard.Count);

            using (StringReader reader = new StringReader(inputData))
            {
                string? line = string.Empty;
                int lineNum = 0;
                int puzzleDimensions;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        if(lineNum == 0)
                            puzzleDimensions = int.Parse(line);
                        else if(lineNum == 1)
                            Symbols = line.Split(' ').ToList();
                        else
                        {
                            var rawCellVals = line.Split(' ');
                            if(rawCellVals.Length > 2)
                            {
                                List<Cell> cellRow = new List<Cell>();
                                foreach (var rawCellVal in rawCellVals)
                                {
                                    var cellVal = rawCellVal == "-" ? null : rawCellVal;
                                    cellRow.Add(new Cell(cellVal));
                                }
                                GameBoard.Add(cellRow);
                            }
                        }
                        lineNum++;
                    }

                } while (line != null);
            }

            foreach(var cellRow in GameBoard)
            {
                Console.WriteLine(cellRow.Count);
            }
        }

        public bool Solve()
        {
            return false;
        }

        public class Cell
        {
            public string? Value { get; set; }
            public List<string> notes { get; set; }
            public Cell(string? value)
            {
                notes = new List<string>();
                Value = value;
            }
        }
    }
}
