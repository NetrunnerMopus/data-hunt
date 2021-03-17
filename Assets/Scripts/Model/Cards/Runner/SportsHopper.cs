using System;
using System.Collections.Generic;
using System.Globalization;
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
        override public IEffect Activation => new SportsHopperActivation(this, game.runner);
        override public IType Type => new Hardware(game);

        private class SportsHopperActivation : IEffect
        {
            private Card hopper;
            private Runner runner;
            private Ability pop;
            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };

            public SportsHopperActivation(Card hopper, Runner runner)
            {
                this.hopper = hopper;
                this.runner = runner;
            }

            async Task IEffect.Resolve()
            {
                var paidWindow = runner.paidWindow;
                var heap = runner.zones.heap.zone;
                if (pop == null)
                {
                    pop = new Ability(new Conjunction(paidWindow.Permission(), new Trash(hopper, heap)), runner.zones.Drawing(3));
                }
                paidWindow.Add(pop, hopper);
                runner.zones.rig.zone.Removed += CheckIfUninstalled;
                await Task.CompletedTask;
            }

            private void CheckIfUninstalled(Zone zone, Card card)
            {
                if (card == this.hopper)
                {
                    runner.paidWindow.Remove(pop);
                }
            }
        }
    }
}
