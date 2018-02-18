namespace effects.runner
{
    public class Trash : IEffect
    {
        private ICard2 card;

        public Trash(ICard2 card)
        {
            this.card = card;
        }

        void IEffect.Resolve(Game game)
        {
            game.runner.Trash(card);
        }
    }
}