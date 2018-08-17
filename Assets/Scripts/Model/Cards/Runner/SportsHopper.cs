using model.costs;
using model.cards.types;
using model.play;
using model.effects.runner;
using model.zones.runner;

namespace model.cards.runner
{
    public class SportsHopper : Card
    {
        override public string FaceupArt => "sports-hopper";
        override public string Name => "Sports Hopper";
        override public Faction Faction => Factions.MASQUE;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => new RunnerCreditCost(3);
        override public IEffect Activation => new SportsHopperActivation(this);
        override public IType Type => new Hardware();

        private class SportsHopperActivation : IEffect, IUninstallationObserver
        {
            private Card card;
            private Ability pop;
            private Game game;

            public SportsHopperActivation(Card card)
            {
                this.card = card;
            }

            void IEffect.Resolve(Game game)
            {
                var paidWindow = game.flow.paidWindow;
                this.game = game;
                if (pop == null)
                {
                    pop = new Ability(new Conjunction(paidWindow.Permission(), new SelfTrash(card)), new Draw(3));
                }
                paidWindow.Add(pop, card);
                game.runner.zones.rig.ObserveUninstallations(this);
            }

            void IUninstallationObserver.NotifyUninstalled(Card card)
            {
                if (card == this.card)
                {
                    game.flow.paidWindow.Remove(pop);
                }
            }

            void IEffect.Observe(IImpactObserver observer, Game game)
            {
            }
        }
    }
}