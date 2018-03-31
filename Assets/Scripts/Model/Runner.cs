using model;
using view;

namespace model
{
    public class Runner
    {
        public int tags = 0;
        public int clicks = 4;
        public readonly Stack stack;
        public readonly Heap heap;
        public readonly CreditPool creditPool;

        public Runner(Stack stack, Heap heap, CreditPool creditPool)
        {
            this.stack = stack;
            this.heap = heap;
            this.creditPool = creditPool;
        }

        public void StartGame()
        {
            creditPool.Gain(5);
            stack.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                stack.Draw();
            }
        }
    }
}