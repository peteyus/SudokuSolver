namespace SudokuSolver.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public static class BruteForceSolver
    {
        public static bool Solve(Puzzle puzzle)
        {
            if (puzzle == null)
            {
                throw new ArgumentNullException(nameof(puzzle), "You should probably give me a puzzle to solve.");
            }

            var firstCell = NextCandidateCell(puzzle);

            if (firstCell == null)
            {
                return true; // already solved.
            }

            return TrySolve(puzzle, firstCell);
        }

        private static bool TrySolve(Puzzle puzzle, Cell activeCell)
        {
            bool solved = false;
            var originalValues = activeCell.PossibleValues.ToArray();
            var possibleValues = new Queue<int>(originalValues);

            while (possibleValues.Any())
            {
                int currentValue = possibleValues.Dequeue();
                if (DoesValueConflict(puzzle, activeCell, currentValue))
                {
                    continue;
                }

                activeCell.Value = currentValue;

                var nextCell = NextCandidateCell(puzzle);
                solved = nextCell == null || TrySolve(puzzle, nextCell);
            }

            if (!solved)
            {
                activeCell.Value = 0;
                activeCell.PossibleValues.Clear();
                activeCell.PossibleValues.AddRange(originalValues);
            }

            return solved;
        }

        private static Cell NextCandidateCell(Puzzle puzzle)
        {
            return puzzle.Cells.OrderBy(cell => cell.PossibleValues.Count)
                .FirstOrDefault(cell => !cell.IsSolved);
        }

        private static bool DoesValueConflict(Puzzle puzzle, Cell currentCell, int value)
        {
            var rowValues = puzzle.Cells.Where(cell => cell.Row == currentCell.Row && cell.IsSolved).Select(cell => cell.Value);
            var colValues = puzzle.Cells.Where(cell => cell.Column == currentCell.Column && cell.IsSolved).Select(cell => cell.Value);
            var squareValues = puzzle.Cells.Where(cell => cell.Square == currentCell.Square && cell.IsSolved)
                .Select(cell => cell.Value);

            var conflictingValues = rowValues.Union(colValues).Union(squareValues).ToList();

            return conflictingValues.Contains(value);
        }
    }
}