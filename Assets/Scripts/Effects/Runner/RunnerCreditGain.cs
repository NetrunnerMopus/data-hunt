namespace effects.runner
{
    public class Gain : IEffect
    {
        private int credits;

        public Gain(int credits)
        {
            this.credits = credits;
        }

        void IEffect.Resolve(Game game)
        {
            game.runner.creditPool.Gain(credits);
        }
    }
}