using PolyformExplorer.Data;
using System.Collections.Generic;

namespace PolyformExplorer
{
    internal class PolyominoGenerator
    {
        public IEnumerable<Polyomino> GetPolyominoesOfOrder(int order)
        {
            if (order == 1)
            {
                yield return new Polyomino();
                yield break;
            }

            HashSet<Polyomino> foundPolyominoes = new();

            foreach (Polyomino subPolyomino in GetPolyominoesOfOrder(order - 1))
            {
                foreach (IntVector2 cell in subPolyomino)
                {
                    foreach (IntVector2 neighbor in cell.GetNeighbors())
                    {
                        if (subPolyomino.Contains(neighbor))
                            continue;

                        Polyomino candidatePolyomino = subPolyomino.GrowBy(neighbor);

                        if (foundPolyominoes.Contains(candidatePolyomino) ||
                            foundPolyominoes.Contains(candidatePolyomino.RotateCW()) ||
                            foundPolyominoes.Contains(candidatePolyomino.RotateCCW()) ||
                            foundPolyominoes.Contains(candidatePolyomino.Rotate180()) ||
                            foundPolyominoes.Contains(candidatePolyomino.ReflectAcrossHorizontalAxis()) ||
                            foundPolyominoes.Contains(candidatePolyomino.ReflectAcrossVerticalAxis()) ||
                            foundPolyominoes.Contains(candidatePolyomino.ReflectAcrossMainDiagonal()) ||
                            foundPolyominoes.Contains(candidatePolyomino.ReflectAcrossAntiDiagonal()))
                            continue;

                        foundPolyominoes.Add(candidatePolyomino);
                        yield return candidatePolyomino;
                    }
                }
            }
        }
    }
}