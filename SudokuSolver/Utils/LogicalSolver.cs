namespace SudokuSolver.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    // Logical steps based on Section 4 of http://www.math.kent.edu/~malexand/Latex/Examples/Article%20Example/YSU_Sudoku.pdf
    public static class LogicalSolver
    {
        public static bool Solve(Puzzle puzzle)
        {
            if (puzzle == null)
            {
                throw new ArgumentNullException(nameof(puzzle), "You should probably give me a puzzle to solve.");
            }

            return TrySolve(puzzle);
        }

        private static bool TrySolve(Puzzle puzzle)
        {
            int recursionCount = 0;

            while (puzzle.Cells.Any(cell => !cell.IsSolved) && recursionCount < 81)
            {
                // Solve for "Hidden Singles"
                // Solve naked singles shaken out by this logic.
                // Loop until no more hidden singles are found.
                bool foundResult;
                do
                {
                    foundResult = FindHiddenSingles(puzzle);
                    SetCellValueForNakedSingles(puzzle);
                } while (foundResult);

                // Eliminate locked candidate values from other squares.
                FindLockedCandidates(puzzle);

                // Eliminate naked pairs from other cells in each square/row/column
                FindNakedPairs(puzzle);

                // This feels like it needs one more step. TODO PRJ: Read the paper more.
                // second paper.. xwings? swordfish? xy pairs?

                SetCellValueForNakedSingles(puzzle);

                recursionCount++;
            }

            return puzzle.Cells.Any(cell => !cell.IsSolved);
        }

        public static void SetCellValueForNakedSingles(Puzzle puzzle)
        {
            foreach (var solvableCell in puzzle.Cells.Where(cell => !cell.IsSolved && cell.PossibleValues.Count == 1))
            {
                puzzle.SetCellValue(solvableCell.Row, solvableCell.Column, solvableCell.PossibleValues.Single());
            }
        }

        private static bool FindHiddenSingles(Puzzle puzzle)
        {
            var foundResult = false;
            for (int row = 1; row <= 9; row++)
            {
                var cells = puzzle.Cells.Where(cell => cell.Row == row && !cell.IsSolved).ToList();
                foundResult |= FindSinglePossibleResultInCells(puzzle, cells);
            }

            for (int col = 1; col <= 9; col++)
            {
                var cells = puzzle.Cells.Where(cell => cell.Column == col && !cell.IsSolved).ToList();
                foundResult |= FindSinglePossibleResultInCells(puzzle, cells);
            }

            foreach (var square in (Squares[])Enum.GetValues(typeof(Squares)))
            {
                var cells = puzzle.Cells.Where(cell => cell.Square == square && !cell.IsSolved).ToList();
                foundResult |= FindSinglePossibleResultInCells(puzzle, cells);
            }

            return foundResult;
        }

        private static bool FindSinglePossibleResultInCells(Puzzle puzzle, IList<Cell> cells)
        {
            var values = new Dictionary<int, int>();
            bool foundResult = false;

            foreach (var value in cells.SelectMany(cell => cell.PossibleValues))
            {
                if (values.ContainsKey(value))
                {
                    values[value]++;
                }
                else
                {
                    values.Add(value, 1);
                }
            }

            foreach (var solvedValue in values.Where(value => value.Value == 1))
            {
                var solvedCell = cells.Single(cell => cell.PossibleValues.Contains(solvedValue.Key));
                puzzle.SetCellValue(solvedCell.Row, solvedCell.Column, solvedValue.Key);
                foundResult = true;
            }

            return foundResult;
        }

        private static void FindLockedCandidates(Puzzle puzzle)
        {
            foreach (var square in (Squares[])Enum.GetValues(typeof(Squares)))
            {
                var squareCells = puzzle.Cells.Where(cell => cell.Square == square && !cell.IsSolved).ToList();

                for (int possibleValue = 1; possibleValue <= 9; possibleValue++)
                {
                    var cellsWithValue =
                        squareCells.Where(cell => cell.PossibleValues.Contains(possibleValue)).ToList();
                    if (ValidateSameValue(cellsWithValue.Select(cell => cell.Row)))
                    {
                        foreach (var otherCell in puzzle.Cells.Where(cell =>
                            cell.Row == cellsWithValue.First().Row && cell.Square != square && !cell.IsSolved &&
                            cell.PossibleValues.Contains(possibleValue)))
                        {
                            otherCell.PossibleValues.Remove(possibleValue);
                        }
                    }

                    if (ValidateSameValue(cellsWithValue.Select(cell => cell.Column)))
                    {
                        foreach (var otherCell in puzzle.Cells.Where(cell =>
                            cell.Column == cellsWithValue.First().Column && cell.Square != square && !cell.IsSolved &&
                            cell.PossibleValues.Contains(possibleValue)))
                        {
                            otherCell.PossibleValues.Remove(possibleValue);
                        }
                    }
                }
            }
        }

        // TODO PRJ: Keep working on this
        private static void FindNakedPairs(Puzzle puzzle)
        {
            foreach (var square in (Squares[])Enum.GetValues(typeof(Squares)))
            {
                var squareCells = puzzle.Cells.Where(cell => cell.Square == square && !cell.IsSolved).ToList();
            }
        }

        private static bool ValidateSameValue(IEnumerable<int> value)
        {
            return value.Distinct().Count() == 1;
        }
    }
}