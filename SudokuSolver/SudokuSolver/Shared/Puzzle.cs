
using Microsoft.AspNetCore.Components.Forms;

namespace SudokuSolver.Shared
{
    public class Puzzle
    {
        public List<string> Symbols {get; set; }
        public List<List<Cell>> GameBoard { get; set; }

        public Puzzle()
        {
            Symbols = new List<string>();
            GameBoard = new List<List<Cell>>();
        }

        public bool Solve()
        {
            var algorithms = new List<CellAlgorithm> { new LastFreeCell(), new LastPossibleSymbol()};

            bool cellHasChanged;
            do
            {
                cellHasChanged = false;
                for (int i = 0; i < GameBoard.Count; i++)
                {
                    for (int j = 0; j < GameBoard[i].Count; j++)
                    {
                        if (GameBoard[i][j].Value == null)
                        {
                            foreach (var algorithm in algorithms)
                            {
                                //Strategy pattern with polymorphism 
                                if (algorithm.TrySolve(this, i, j))
                                {
                                    cellHasChanged = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            } while (cellHasChanged);
            return GameBoardIsFilled();
        }

        public bool GameBoardIsFilled()
        {
            foreach(var row in GameBoard)
            {
                foreach(var cell in row)
                {
                    if (cell.Value == null) return false;
                }
            }
            return true;
        }


        private void GenerateNotes()
        {
            for(int i = 0; i < GameBoard.Count; i++)
            {
                for(int j = 0; j < GameBoard.Count; j++)
                {
                    if (GameBoard[i][j].Value == null)
                    {
                        GameBoard[i][j].Notes = new List<string>();
                        foreach(var symbol in Symbols)
                        {
                            if(ValueIsValid(symbol, i, j))
                            {
                                GameBoard[i][j].Notes.Add(symbol);
                            }
                        }
                    }
                }
            }
        }

        public List<Cell> GetColumn(int column)
        {
            var columnList = new List<Cell>();
            for (int i = 0; i < GameBoard.Count; i++)
            {
                for (int j = 0; j < GameBoard.Count; j++)
                {
                    if (j == column)
                    {
                        columnList.Add(GameBoard[i][j]);
                    }
                }
            }
            return columnList;
        }

        public List<Cell> GetBlock(int row, int column)
        {
            int blockSize = (int)Math.Sqrt(GameBoard.Count);
            bool foundBlock = false;
            List<Cell> blockList = new List<Cell>();

            for(int i = 0; i < GameBoard.Count; i += blockSize)
            {
                for(int j = 0; j < GameBoard.Count; j += blockSize)
                {
                    blockList = new List<Cell>();
                    for (int k = 0; k < blockSize; k++)
                    {
                        for(int l = 0; l < blockSize; l++)
                        {
                            blockList.Add(GameBoard[j + k][i + l]);
                            if (!foundBlock)
                            {
                                foundBlock = (row == (j + k)) && (column == (i + l));
                            }
                        }
                    }
                    if (foundBlock) break;
                }
                if(foundBlock) break;
            }

            return blockList;
        }

        public bool ValueInList(List<Cell> list, string value)
        {
            foreach(Cell cell in list)
            {
                if(cell.Value == value)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValueInRow(string value, int row)
        {
            return ValueInList(GameBoard[row], value);
        }

        public bool ValueInColumn(string value, int column)
        {
            return ValueInList(GetColumn(column), value);
        }

        public bool ValueInBlock(string value, int row, int column)
        {
            return ValueInList(GetBlock(row, column), value);
        }

        public bool ValueIsValid(string? value, int row, int column)
        {
            if (value == null) return false;
            return !ValueInRow(value, row) && !ValueInColumn(value, column) && !ValueInBlock(value, row, column);
        }

        public List<Cell> GetOnlyCellsWithValues(List<Cell> cells)
        {
            var filteredVals = new List<Cell>();
            foreach(Cell cell in cells)
            {
                if(cell.Value != null) filteredVals.Add(cell);
            }
            return filteredVals;
        }

        public class Cell
        {
            public string? Value { get; set; }
            public List<string> Notes { get; set; }
            public Cell(string? value)
            {
                Notes = new List<string>();
                Value = value;
            }
        }
    }
}
