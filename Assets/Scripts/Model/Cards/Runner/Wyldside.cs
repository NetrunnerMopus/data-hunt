using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.costs;
using model.effects;
using model.effects.runner;

namespace model.cards.runner
{
    public class Wyldside : Card
    {
        public Wyldside(Game game) : base(game) { }
        override public string FaceupArt => "wyldside";
        override public string Name => "Wyldside";
        override public Faction Faction => Factions.ANARCH;
        override public int InfluenceCost => 3;
        override public ICost PlayCost => game.Costs.InstallResource(this, 3);
        override public IEffect Activation => new WyldsideActivation(game);
        override public IType Type => new Resource();

        private class WyldsideActivation : IEffect
        {
            private readonly WyldsideTrigger trigger;
            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };

            public WyldsideActivation(Game game)
            {
                trigger = new WyldsideTrigger(game);
            }

            async Task IEffect.Resolve(Game game)
            {
                game.runner.turn.WhenBegins(trigger);
                await Task.CompletedTask;
            }
        }

        private class WyldsideTrigger : IEffect
        {
            private IEffect sequence;
            public bool Impactful => sequence.Impactful;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new[] { "wyldside" };

            public WyldsideTrigger(Game game)
            {
                sequence = new Sequence(game.runner.zones.Drawing(2), game.runner.clicks.Losing(1));
                sequence.ChangedImpact += ChangedImpact;
            }

            async Task IEffect.Resolve(Game game)
            {
                await sequence.Resolve(game);
            }
        }
    }
}
