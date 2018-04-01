namespace model.effects
{
    public class Sequence : IEffect
    {
        private IEffect[] effects;

        public Sequence(params IEffect[] effects)
        {
            this.effects = effects;
        }

        void IEffect.Resolve(Game game)
        {
            foreach (var effect in effects)
            {
                effect.Resolve(game);
            }
        }
    }
}