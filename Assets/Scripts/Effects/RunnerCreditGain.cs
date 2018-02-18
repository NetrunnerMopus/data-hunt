namespace effects
{
    public class RunnerCreditGain : IEffect
    {
        private int gain;

        public RunnerCreditGain(int gain)
        {
            this.gain = gain;
        }

        void IEffect.Resolve(Game game)
        {
            game.runner.creditPool.Gain(gain);
        }
    }
}