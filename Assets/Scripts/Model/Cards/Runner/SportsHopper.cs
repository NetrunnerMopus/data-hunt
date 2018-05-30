using model.costs;
using model.cards.types;
using model.play;
using model.effects.runner;
using model.zones.runner;

namespace model.cards.runner
{
    public class SportsHopper : ICard
    {
        string ICard.FaceupArt => "sports-hopper";

        string ICard.Name => "Sports Hopper";

        bool ICard.Faceup => false;

        Faction ICard.Faction => Factions.MASK;

        int ICard.InfluenceCost => 0;

        ICost ICard.PlayCost => new RunnerCreditCost(3);

        IEffect ICard.Activation => new SportsHopperActivation(this);

        IType ICard.Type => new Hardware();

        private class SportsHopperActivation : IEffect, IUninstallationObserver
        {
            private ICard card;
            private Ability pop;
            private Game game;

            public SportsHopperActivation(ICard card)
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

            void IUninstallationObserver.NotifyUninstalled(ICard card)
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