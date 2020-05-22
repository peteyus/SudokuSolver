namespace SudokuSolver.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Utils;

    public class Puzzle
    {
        private bool solving;

        public Puzzle()
        {
            this.Cells = new List<Cell>();

            // TODO: Can we make this more generic? Accept other size of puzzles?
            for (int row = 1; row <= 9; row++)
            {
                for (int col = 1; col <= 9; col++)
                {
                    this.Cells.Add(new Cell(row, col));
                }
            }
        }

        public List<Cell> Cells { get; }

        public bool BruteForceSolve { get; set; }

        public bool Solved { get; private set; }

        public void SetCellValue(int row, int col, int value)
        {
            Cell activeCell = this.Cells.SingleOrDefault(cell => cell.Row == row && cell.Column == col);
            if (activeCell == null)
            {
                throw new Exception("You shouldn't have done that, he's just a cell mmmhmmm.");
            }

            activeCell.Value = value;

            // remove given value from possibilities for row
            foreach (var rowCell in this.Cells.Where(cell => cell.Row == row && !cell.IsSolved))
            {
                rowCell.PossibleValues.Remove(value);
            }

            // remove given value from possibilities for column
            foreach (var colCell in this.Cells.Where(cell => cell.Column == col && !cell.IsSolved))
            {
                colCell.PossibleValues.Remove(value);
            }

            // remove given value from possibilities for square
            foreach (var squareCell in this.Cells.Where(cell => cell.Square == activeCell.Square && !cell.IsSolved))
            {
                squareCell.PossibleValues.Remove(value);
            }

            if (this.solving)
            {
                // solve any others that only have one possibility
                LogicalSolver.SetCellValueForNakedSingles(this);
            }
        }

        public void SolvePuzzle()
        {
            this.solving = true;

            if (!this.BruteForceSolve)
            {
                LogicalSolver.Solve(this);

                // not solved, do we fall back?
                Debugger.Break();
            }

            else
            {
                LogicalSolver.SetCellValueForNakedSingles(this);

                this.Solved = BruteForceSolver.Solve(this);
                if (!this.Solved)
                {
                    Debugger.Break();
                }
            }
        }

        public string PrintResult()
        {
            var board = new StringBuilder();
            board.AppendLine("  1 2 3 4 5 6 7 8 9 ");
            for (int row = 1; row <= 9; row++)
            {
                board.Append($"{row} ");
                for (int col = 1; col <= 9; col++)
                {
                    board.Append($"{this.Cells.Single(cell => cell.Row == row && cell.Column == col).Value} ");
                }

                board.AppendLine();
            }

            return board.ToString();
        }
    }
}