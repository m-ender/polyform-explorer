using NUnit.Framework;
using PolyformExplorer.Utility;
using System;

namespace PolyformExplorer.Data.Tests
{
    internal class D4SubgroupTests
    {
        [Test]
        public void Test_subgroup_relation()
        {
            // Row gives subgroup, column gives supergroup,
            // # indicates correct relation. Ordering of groups:
            // 
            // Identity
            // D1AcrossHorizontal
            // D1AcrossVertical
            // D1AcrossMainDiagonal
            // D1AcrossAntiDiagonal
            // C2
            // D2Orthogonal
            // D2Diagonal
            // C4
            // D4
            string[] subgroupMatrix = @"
                ##########
                .#....#..#
                ..#...#..#
                ...#...#.#
                ....#..#.#
                .....#####
                ......#..#
                .......#.#
                ........##
                .........#
            ".TrimCommonIndentation(true).Split(Environment.NewLine);

            Assert.Multiple(() =>
            {
                for (int i = 0; i < 10; ++i)
                    for (int j = 0; j < 10; ++j)
                    {
                        D4Subgroup groupI = (D4Subgroup)i;
                        D4Subgroup groupJ = (D4Subgroup)j;
                        bool isSubgroup = subgroupMatrix[i][j] == '#';

                        Assert.That(
                            groupI.IsSubgroupOf(groupJ),
                            Is.EqualTo(isSubgroup),
                            $"Expected {Enum.GetName(typeof(D4Subgroup), groupI)} to {(isSubgroup ? "" : "not ")}be a subgroup of {Enum.GetName(typeof(D4Subgroup), groupJ)}.");
                    }
            });
        }
    }
}