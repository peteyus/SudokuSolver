namespace SudokuSolverTests.Models
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SudokuSolver.Models;

    [TestClass]
    public class CellTests
    {
        [TestClass]
        public class SquareTests : CellTests
        {
            [TestMethod]
            public void ReturnsTopLeftWhenR1C1()
            {
                // arrange
                // act
                var classUnderTest = new Cell(1, 1);

                // assert
                Assert.AreEqual(Squares.TopLeft, classUnderTest.Square, "Should have been in the top left");
            }

            [TestMethod]
            public void ReturnsCenterMiddleWhenR5C5()
            {
                // arrange
                // act
                var classUnderTest = new Cell(5, 5);

                // assert
                Assert.AreEqual(Squares.CenterMiddle, classUnderTest.Square, "Should have been in the center middle.");
            }

            [TestMethod]
            public void ReturnsBottomRightWhenR9C9()
            {
                // arrange
                // act
                var classUnderTest = new Cell(9, 9);

                // assert
                Assert.AreEqual(Squares.BottomRight, classUnderTest.Square, "Should have been in the bottom right.");
            }

            [TestMethod]
            public void ReturnsCorrectSquareForRandomLocation()
            {
                // arrange
                var random = new Random();
                int row = random.Next(1, 9);
                int col = random.Next(1, 9);

                Squares expected;
                if (row <= 3 && col <= 3)
                {
                    expected = Squares.TopLeft;
                }
                else if (row <= 6 && col <= 3)
                {
                    expected = Squares.CenterLeft;
                }
                else if (col <= 3)
                {
                    expected = Squares.BottomLeft;
                }
                else if (row <= 3 && col <= 6)
                {
                    expected = Squares.TopMiddle;
                }
                else if (row <= 6 && col <= 6)
                {
                    expected = Squares.CenterMiddle;
                }
                else if (col <= 6)
                {
                    expected = Squares.BottomMiddle;
                }
                else if (row <= 3)
                {
                    expected = Squares.TopRight;
                }
                else if (row <= 6)
                {
                    expected = Squares.CenterRight;
                }
                else
                {
                    expected = Squares.BottomRight;
                }

                // act
                var classUnderTest = new Cell(row, col);

                // assert
                Assert.AreEqual(expected, classUnderTest.Square, "Failed to set the expected square.");
            }
        }
    }
}