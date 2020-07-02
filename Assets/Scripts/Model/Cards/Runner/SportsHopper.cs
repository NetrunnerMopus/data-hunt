using model.costs;
using model.cards.types;
using model.play;
using model.effects.runner;
using model.zones.runner;
using model.zones;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        private class SportsHopperActivation : IEffect, IZoneRemovalObserver
        {
            private Card card;
            private Ability pop;
            private Game game;
            IEnumerable<string> IEffect.Graphics => new string[] {};
            public SportsHopperActivation(Card card)
            {
                this.card = card;
            }

            async Task IEffect.Resolve(Game game)
            {
                var paidWindow = game.runner.paidWindow;
                this.game = game;
                var heap = game.runner.zones.heap.zone;
                if (pop == null)
                {
                    pop = new Ability(new Conjunction(paidWindow.Permission(), new Trash(card, heap)), new Draw(3));
                }
                paidWindow.Add(pop, card);
                game.runner.zones.rig.zone.ObserveRemovals(this);
                await Task.CompletedTask;
            }

            void IZoneRemovalObserver.NotifyCardRemoved(Card card)
            {
                if (card == this.card)
                {
                    game.runner.paidWindow.Remove(pop);
                }
            }

            void IEffect.Observe(IImpactObserver observer, Game game)
            {
            }
        }
    }
}
