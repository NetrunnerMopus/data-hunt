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
            for (int i = 0; i < cards; i++)
            {
                if (rd.HasCards())
                {
                    var drawn = rd.RemoveTop();
                    hq.Add(drawn);
                }
                else
                {
                    game.ended = true;
                }
            }
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}