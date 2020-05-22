namespace SudokuSolverTests.Models
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SudokuSolver.Models;

    [TestClass]
    public class PuzzleTests
    {
        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestClass]
        public class SetCellValueTests : PuzzleTests
        {
            [TestMethod]
            public void ThrowsExceptionWhenInvalidCellSpecified()
            {
                // arrange
                var classUnderTest = new Puzzle();

                // act
                try
                {
                    classUnderTest.SetCellValue(-1, -1, 99);
                    Assert.Fail("Should have thrown an exception.");
                }
                catch (Exception e)
                {
                    // assert
                    Assert.AreEqual("You shouldn't have done that, he's just a cell mmmhmmm.", e.Message,
                        "Threw the wrong exception.");
                }
            }

            [TestMethod]
            public void DoesNotSolveUntilCallingSolve()
            {
                /*
                 *   1 2 3 4 5 6 7 8 9
                 * 1 - - - 2 6 - 7 - 1
                 * 2 6 8 - - 7 - - 9 -
                 * 3 1 9 - - - 4 5 - -
                 * 4 8 2 - 1 - - - 4 -
                 * 5 - - 4 6 - 2 9 - -
                 * 6 - 5 - - - 3 - 2 8
                 * 7 - - 9 3 - - - 7 4
                 * 8 - 4 - - 5 - - 3 6
                 * 9 7 - 3 - 1 8 - - -
                 */
                // arrange
                var classUnderTest = new Puzzle();

                // act
                classUnderTest.SetCellValue(1, 4, 2);
                classUnderTest.SetCellValue(1, 5, 6);
                classUnderTest.SetCellValue(1, 7, 7);
                classUnderTest.SetCellValue(1, 9, 1);

                classUnderTest.SetCellValue(2, 1, 6);
                classUnderTest.SetCellValue(2, 2, 8);
                classUnderTest.SetCellValue(2, 5, 7);
                classUnderTest.SetCellValue(2, 8, 9);

                classUnderTest.SetCellValue(3, 1, 1);
                classUnderTest.SetCellValue(3, 2, 9);
                classUnderTest.SetCellValue(3, 6, 4);
                classUnderTest.SetCellValue(3, 7, 5);

                classUnderTest.SetCellValue(4, 1, 8);
                classUnderTest.SetCellValue(4, 2, 2);
                classUnderTest.SetCellValue(4, 4, 1);
                classUnderTest.SetCellValue(4, 8, 4);

                classUnderTest.SetCellValue(5, 3, 4);
                classUnderTest.SetCellValue(5, 4, 6);
                classUnderTest.SetCellValue(5, 6, 2);
                classUnderTest.SetCellValue(5, 7, 9);

                classUnderTest.SetCellValue(6, 2, 5);
                classUnderTest.SetCellValue(6, 6, 3);
                classUnderTest.SetCellValue(6, 8, 2);
                classUnderTest.SetCellValue(6, 9, 8);

                classUnderTest.SetCellValue(7, 3, 9);
                classUnderTest.SetCellValue(7, 4, 3);
                classUnderTest.SetCellValue(7, 8, 7);
                classUnderTest.SetCellValue(7, 9, 4);

                classUnderTest.SetCellValue(8, 2, 4);
                classUnderTest.SetCellValue(8, 5, 5);
                classUnderTest.SetCellValue(8, 8, 3);
                classUnderTest.SetCellValue(8, 9, 6);

                classUnderTest.SetCellValue(9, 1, 7);
                classUnderTest.SetCellValue(9, 3, 3);
                classUnderTest.SetCellValue(9, 5, 1);
                classUnderTest.SetCellValue(9, 6, 8);

                // assert
                Assert.AreEqual(45, classUnderTest.Cells.Count(cell => !cell.IsSolved),
                    "Should not have solved all the squares.");
                TestContext.WriteLine(classUnderTest.PrintResult());
            }
        }

        [TestClass]
        public class SolvePuzzleTests : PuzzleTests
        {

            [TestMethod]
            public void SolvesEasyExample()
            {
                /*
                 *   1 2 3 4 5 6 7 8 9
                 * 1 - - - 2 6 - 7 - 1
                 * 2 6 8 - - 7 - - 9 -
                 * 3 1 9 - - - 4 5 - -
                 * 4 8 2 - 1 - - - 4 -
                 * 5 - - 4 6 - 2 9 - -
                 * 6 - 5 - - - 3 - 2 8
                 * 7 - - 9 3 - - - 7 4
                 * 8 - 4 - - 5 - - 3 6
                 * 9 7 - 3 - 1 8 - - -
                 */
                // arrange
                var classUnderTest = new Puzzle();
                classUnderTest.SetCellValue(1, 4, 2);
                classUnderTest.SetCellValue(1, 5, 6);
                classUnderTest.SetCellValue(1, 7, 7);
                classUnderTest.SetCellValue(1, 9, 1);

                classUnderTest.SetCellValue(2, 1, 6);
                classUnderTest.SetCellValue(2, 2, 8);
                classUnderTest.SetCellValue(2, 5, 7);
                classUnderTest.SetCellValue(2, 8, 9);

                classUnderTest.SetCellValue(3, 1, 1);
                classUnderTest.SetCellValue(3, 2, 9);
                classUnderTest.SetCellValue(3, 6, 4);
                classUnderTest.SetCellValue(3, 7, 5);

                classUnderTest.SetCellValue(4, 1, 8);
                classUnderTest.SetCellValue(4, 2, 2);
                classUnderTest.SetCellValue(4, 4, 1);
                classUnderTest.SetCellValue(4, 8, 4);

                classUnderTest.SetCellValue(5, 3, 4);
                classUnderTest.SetCellValue(5, 4, 6);
                classUnderTest.SetCellValue(5, 6, 2);
                classUnderTest.SetCellValue(5, 7, 9);

                classUnderTest.SetCellValue(6, 2, 5);
                classUnderTest.SetCellValue(6, 6, 3);
                classUnderTest.SetCellValue(6, 8, 2);
                classUnderTest.SetCellValue(6, 9, 8);

                classUnderTest.SetCellValue(7, 3, 9);
                classUnderTest.SetCellValue(7, 4, 3);
                classUnderTest.SetCellValue(7, 8, 7);
                classUnderTest.SetCellValue(7, 9, 4);

                classUnderTest.SetCellValue(8, 2, 4);
                classUnderTest.SetCellValue(8, 5, 5);
                classUnderTest.SetCellValue(8, 8, 3);
                classUnderTest.SetCellValue(8, 9, 6);

                classUnderTest.SetCellValue(9, 1, 7);
                classUnderTest.SetCellValue(9, 3, 3);
                classUnderTest.SetCellValue(9, 5, 1);
                classUnderTest.SetCellValue(9, 6, 8);

                // act
                classUnderTest.SolvePuzzle();

                // assert
                Assert.AreEqual(0, classUnderTest.Cells.Count(cell => !cell.IsSolved),
                    "Should have solved all the squares.");
                TestContext.WriteLine(classUnderTest.PrintResult());
            }

            [TestMethod]
            public void SolvesIntermediateExample()
            {
                /*
                 *   1 2 3 4 5 6 7 8 9
                 * 1 - 2 - 6 - 8 - - -
                 * 2 5 8 - - - 9 7 - -
                 * 3 - - - - 4 - - - -
                 * 4 3 7 - - - - 5 - -
                 * 5 6 - - - - - - - 4
                 * 6 - - 8 - - - - 1 3
                 * 7 - - - - 2 - - - -
                 * 8 - - 9 8 - - - 3 6
                 * 9 - - - 3 - 6 - 9 -
                 */
                // arrange
                var classUnderTest = new Puzzle();
                classUnderTest.SetCellValue(1, 2, 2);
                classUnderTest.SetCellValue(1, 4, 6);
                classUnderTest.SetCellValue(1, 6, 8);

                classUnderTest.SetCellValue(2, 1, 5);
                classUnderTest.SetCellValue(2, 2, 8);
                classUnderTest.SetCellValue(2, 6, 9);
                classUnderTest.SetCellValue(2, 7, 7);

                classUnderTest.SetCellValue(3, 5, 4);

                classUnderTest.SetCellValue(4, 1, 3);
                classUnderTest.SetCellValue(4, 2, 7);
                classUnderTest.SetCellValue(4, 7, 5);

                classUnderTest.SetCellValue(5, 1, 6);
                classUnderTest.SetCellValue(5, 9, 4);

                classUnderTest.SetCellValue(6, 3, 8);
                classUnderTest.SetCellValue(6, 8, 1);
                classUnderTest.SetCellValue(6, 9, 3);

                classUnderTest.SetCellValue(7, 5, 2);

                classUnderTest.SetCellValue(8, 3, 9);
                classUnderTest.SetCellValue(8, 4, 8);
                classUnderTest.SetCellValue(8, 8, 3);
                classUnderTest.SetCellValue(8, 9, 6);

                classUnderTest.SetCellValue(9, 4, 3);
                classUnderTest.SetCellValue(9, 6, 6);
                classUnderTest.SetCellValue(9, 8, 9);

                classUnderTest.BruteForceSolve = true;

                // act
                classUnderTest.SolvePuzzle();

                // assert
                Assert.AreEqual(0, classUnderTest.Cells.Count(cell => !cell.IsSolved),
                    "Should have solved all the squares.");
                TestContext.WriteLine(classUnderTest.PrintResult());
            }

            [TestMethod]
            public void SolvesDifficultExample()
            {
                /*
                 *   1 2 3 4 5 6 7 8 9
                 * 1 - - - 6 - - 4 - -
                 * 2 7 - - - - 3 6 - -
                 * 3 - - - - 9 1 - 8 -
                 * 4 - - - - - - - - -
                 * 5 - 5 - 1 8 - - - 3
                 * 6 - - - 3 - 6 - 4 5
                 * 7 - 4 - 2 - - - 6 -
                 * 8 9 - 3 - - - - - -
                 * 9 - 2 - - - - 1 - -
                 */
                // arrange
                var classUnderTest = new Puzzle();

                classUnderTest.SetCellValue(1, 4, 6);
                classUnderTest.SetCellValue(1, 7, 4);

                classUnderTest.SetCellValue(2, 1, 7);
                classUnderTest.SetCellValue(2, 6, 3);
                classUnderTest.SetCellValue(2, 7, 6);

                classUnderTest.SetCellValue(3, 5, 9);
                classUnderTest.SetCellValue(3, 6, 1);
                classUnderTest.SetCellValue(3, 8, 8);

                classUnderTest.SetCellValue(5, 2, 5);
                classUnderTest.SetCellValue(5, 4, 1);
                classUnderTest.SetCellValue(5, 5, 8);
                classUnderTest.SetCellValue(5, 9, 3);

                classUnderTest.SetCellValue(6, 4, 3);
                classUnderTest.SetCellValue(6, 6, 6);
                classUnderTest.SetCellValue(6, 8, 4);
                classUnderTest.SetCellValue(6, 9, 5);

                classUnderTest.SetCellValue(7, 2, 4);
                classUnderTest.SetCellValue(7, 4, 2);
                classUnderTest.SetCellValue(7, 8, 6);

                classUnderTest.SetCellValue(8, 1, 9);
                classUnderTest.SetCellValue(8, 3, 3);

                classUnderTest.SetCellValue(9, 2, 2);
                classUnderTest.SetCellValue(9, 7, 1);

                classUnderTest.BruteForceSolve = true;

                // act
                classUnderTest.SolvePuzzle();

                // assert
                Assert.AreEqual(0, classUnderTest.Cells.Count(cell => !cell.IsSolved),
                    "Should have solved all the squares.");
                TestContext.WriteLine(classUnderTest.PrintResult());

            }
        }
    }
}