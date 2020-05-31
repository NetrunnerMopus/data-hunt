using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace view.gui
{
    public class Spiral
    {
        private static float HORIZONTAL = 1f / 2f;
        private static float VERTICAL = 4f / 3f;
        public static Vector2 R = Vector2.right;
        public static Vector2 RD = Vector2.right * HORIZONTAL + Vector2.down * VERTICAL;
        public static Vector2 LD = Vector2.left * HORIZONTAL + Vector2.down * VERTICAL;
        public static Vector2 L = Vector2.left;
        public static Vector2 LU = Vector2.left * HORIZONTAL + Vector2.up * VERTICAL;
        public static Vector2 RU = Vector2.right * HORIZONTAL + Vector2.up * VERTICAL;

        public Vector2 PickVector(int hexIndex)
        {
            if (hexIndex == 1)
            {
                return Vector2.zero;
            }
            var cycle = 1;
            while (hexIndex > CycleSize(cycle))
            {
                hexIndex -= CycleSize(cycle);
                cycle++;
            }
            var cycleStart = LU * (cycle - 1);
            var withinCycle = YieldVector(cycle).Take(hexIndex - 1).Aggregate(Vector2.zero, (a, b) => a + b);
            return cycleStart + withinCycle;
        }

        private IEnumerable<Vector2> YieldVector(int cycle)
        {
            for (int i = 0; i < cycle; i++)
            {
                yield return R;
            }
            for (int i = 0; i < cycle - 1; i++) // the -1 is intentional
            {
                yield return RD;
            }
            for (int i = 0; i < cycle; i++)
            {
                yield return LD;
            }
            for (int i = 0; i < cycle; i++)
            {
                yield return L;
            }
            for (int i = 0; i < cycle; i++)
            {
                yield return LU;
            }
            for (int i = 0; i < cycle; i++)
            {
                yield return RU;
            }
        }

        private int CountCycles(int hexIndex)
        {
            var cycle = 1;
            while (hexIndex > CycleSize(cycle))
            {
                hexIndex -= CycleSize(cycle);
                cycle++;
            }
            return cycle;
        }

        private int CycleSize(int cycle) => cycle * 6 - 1;
    }

}