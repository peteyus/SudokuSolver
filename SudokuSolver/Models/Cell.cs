namespace SudokuSolver.Models
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("Row {Row}, Col {Column}, Solved = {IsSolved}")]
    public class Cell
    {
        private readonly int[] possibleValues = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private int value;

        public Cell(int row, int col)
        {
            this.Row = row;
            this.Column = col;
            this.PossibleValues = new List<int>(this.possibleValues);
        }

        public int Row { get; }

        public int Column { get; }

        public int Value
        {
            get => value;
            set
            {
                this.value = value;

                if (value == 0)
                {
                    this.PossibleValues = new List<int>(this.possibleValues);
                    return;
                }
                
                this.PossibleValues.Clear();
                this.PossibleValues.Add(value);
            }
        }

        public bool IsSolved => this.Value != 0;

        public List<int> PossibleValues { get; private set; }

        public Squares Square
        {
            get
            {
                var result = this switch
                    {
                        var c when (c.Row <= 3 && c.Column <= 3) => Squares.TopLeft,
                        var c when (c.Row <= 6 && c.Column <= 3) => Squares.CenterLeft,
                        var c when (c.Row <= 9 && c.Column <= 3) => Squares.BottomLeft,
                        var c when (c.Row <= 3 && c.Column <= 6) => Squares.TopMiddle,
                        var c when (c.Row <= 6 && c.Column <= 6) => Squares.CenterMiddle,
                        var c when (c.Row <= 9 && c.Column <= 6) => Squares.BottomMiddle,
                        var c when (c.Row <= 3 && c.Column <= 9) => Squares.TopRight,
                        var c when (c.Row <= 6 && c.Column <= 9) => Squares.CenterRight,
                        _ => Squares.BottomRight
                    };

                return result;
            }
        }
    }
}