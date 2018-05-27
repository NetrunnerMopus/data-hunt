using model.costs;
using model.cards.types;
using model.play;
using model.effects.runner;

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

        private class SportsHopperActivation : IEffect
        {
            private Ability pop;

            public SportsHopperActivation(ICard card)
            {
                pop = new Ability(new SelfTrash(card), new Draw(3));
            }

            void IEffect.Resolve(Game game)
            {
                game.runner.turn.paidWindow.Add(pop);
            }

            void IEffect.Perish(Game game)
            {
                game.runner.turn.paidWindow.Remove(pop);
            }

            void IEffect.Observe(IImpactObserver observer, Game game)
            {
            }
        }
    }
}