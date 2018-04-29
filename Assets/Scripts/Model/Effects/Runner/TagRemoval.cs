namespace model.effects.runner
{
    public class TagRemoval : IEffect
    {
        private int tags;

        public TagRemoval(int tags)
        {
            this.tags = tags;
        }

        void IEffect.Resolve(Game game)
        {
            game.runner.tags -= tags;
        }
    }
}