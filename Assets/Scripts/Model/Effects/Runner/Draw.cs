namespace model.effects.runner
{
    public class Draw : IEffect
    {
        private int cards;

        public Draw(int cards)
        {
            this.cards = cards;
        }

        void IEffect.Resolve(Game game)
        {
            for (int i = 0; i < cards; i++)
            {
                var drawn = game.runner.stack.RemoveTop();
                game.runner.grip.Add(drawn);
            }
        }
    }
}