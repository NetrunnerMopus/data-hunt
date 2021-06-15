using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.effects;

namespace model.cards.runner {
    public class Wyldside : Card {
        private readonly WyldsideTrigger trigger;

        public Wyldside(Game game) : base(game) {
            trigger = new WyldsideTrigger(game.runner);
        }

        override public string FaceupArt => "wyldside";
        override public string Name => "Wyldside";
        override public Faction Faction => Factions.ANARCH;
        override public int InfluenceCost => 3;
        override public ICost PlayCost => game.runner.credits.PayingForPlaying(this, 3);
        override public IType Type => new Resource(game);

        protected override Task Activate() {
            game.runner.turn.WhenBegins(trigger);
        }

        private class WyldsideActivation : IEffect {
            private Runner runner;

            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };

            public WyldsideActivation(Runner runner) {
                this.runner = runner;
                trigger = new WyldsideTrigger(runner);
            }

            async Task IEffect.Resolve() {
                runner.turn.WhenBegins(trigger);
                await Task.CompletedTask;
            }
        }

        private class WyldsideTrigger : IEffect {
            private IEffect sequence;
            public bool Impactful => sequence.Impactful;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new[] { "wyldside" };

            public WyldsideTrigger(Runner runner) {
                sequence = new Sequence(runner.zones.Drawing(2), runner.clicks.Losing(1));
                sequence.ChangedImpact += ChangedImpact;
            }

            async Task IEffect.Resolve() {
                await sequence.Resolve();
            }
        }
    }
}
