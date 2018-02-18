namespace effects
{
    public class RunnerCardDraw : IEffect
    {
        private int cards;

        public RunnerCardDraw(int cards)
        {
            this.cards = cards;
        }

        void IEffect.Resolve(Game game)
        {
            for (int i = 0; i < cards; i++)
            {
                game.runner.stack.Draw();
            }
        }
    }
}