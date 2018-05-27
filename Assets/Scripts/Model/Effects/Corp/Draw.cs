namespace model.effects.corp
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
            var rd = game.corp.zones.rd;
            var hq = game.corp.zones.hq;
            rd.Draw(cards, hq);
        }

        void IEffect.Perish(Game game) { }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}