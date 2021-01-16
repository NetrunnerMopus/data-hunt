using model.effects.runner;
using model.costs;
using model.cards.types;
using System.Threading.Tasks;
using System.Collections.Generic;
using model.effects;

namespace model.cards.runner
{
    public class Wyldside : Card
    {
        override public string FaceupArt => "wyldside";
        override public string Name => "Wyldside";
        override public Faction Faction => Factions.ANARCH;
        override public int InfluenceCost => 3;
        override public ICost PlayCost => new RunnerCreditCost(3);
        override public IEffect Activation => new WyldsideActivation();
        override public IType Type => new Resource();

        private class WyldsideActivation : IEffect
        {
            private readonly WyldsideTrigger trigger = new WyldsideTrigger();

            IEnumerable<string> IEffect.Graphics => new string[] { };

            async Task IEffect.Resolve(Game game)
            {
                game.runner.turn.WhenBegins(trigger);
                await Task.CompletedTask;
            }
            void IEffect.Observe(IImpactObserver observer, Game game) { }
        }

        private class WyldsideTrigger : IEffect
        {

            private IEffect sequence = new Sequence(new Draw(2), new LoseClicks(1));
            IEnumerable<string> IEffect.Graphics => new[] { "wyldside" };

            void IEffect.Observe(IImpactObserver observer, Game game)
            {
                sequence.Observe(observer, game);
            }

            async Task IEffect.Resolve(Game game)
            {
                await sequence.Resolve(game);
            }
        }
    }
}
