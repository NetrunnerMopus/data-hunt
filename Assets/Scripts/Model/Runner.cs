namespace model
{
    public class Runner
    {
        public int tags = 0;
        public readonly Stack stack;
        public readonly Heap heap;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Runner(Stack stack, Heap heap, ClickPool clicks, CreditPool credits)
        {
            this.stack = stack;
            this.heap = heap;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void StartGame()
        {
            credits.Gain(5);
            stack.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                stack.Draw();
            }
            for (int i = 0; i < 4; i++)
            {
                clicks.Gain();
            }

            clicks.Spend();
        }
    }
}