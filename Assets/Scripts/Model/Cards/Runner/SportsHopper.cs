using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.costs;
using model.effects.runner;
using model.play;
using model.zones;

namespace model.cards.runner
{
    public class SportsHopper : Card
    {
        public SportsHopper(Game game) : base(game) { }
        override public string FaceupArt => "sports-hopper";
        override public string Name => "Sports Hopper";
        override public Faction Faction => Factions.MASQUE;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => game.Costs.InstallHardware(this, 3);
        override public IEffect Activation => new SportsHopperActivation(this);
        override public IType Type => new Hardware();

        private class SportsHopperActivation : IEffect
        {
            private Card card;
            private Ability pop;
            private Game game;
            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };
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
                    pop = new Ability(new Conjunction(paidWindow.Permission(), new Trash(card, heap)), game.runner.zones.Drawing(3));
                }
                paidWindow.Add(pop, card);
                game.runner.zones.rig.zone.Removed += CheckIfUninstalled;
                await Task.CompletedTask;
            }

            private void CheckIfUninstalled(Zone zone, Card card)
            {
                if (card == this.card)
                {
                    game.runner.paidWindow.Remove(pop);
                }
            }
        }
    }
}
