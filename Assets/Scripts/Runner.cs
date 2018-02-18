public class Runner
{
    public int tags = 0;
    public readonly Grip grip;
    public readonly Stack stack;
    public readonly Heap heap;
    public readonly CreditPool creditPool;

    public Runner(Grip grip, Stack stack, Heap heap, CreditPool creditPool)
    {
        this.grip = grip;
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

    public void Trash(ICard2 card)
    {

    }

    public void Discard(ICard2 card)
    {

    }
}
