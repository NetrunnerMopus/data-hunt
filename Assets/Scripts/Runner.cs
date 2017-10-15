public class Runner
{
    private int tags = 0;
    private Grip grip;
    private Stack stack;
    private CreditPool creditPool;

    public Runner(Grip grip, Stack stack, CreditPool creditPool)
    {
        this.grip = grip;
        this.stack = stack;
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
