using model.effects.runner;
using model.costs;
using model.cards.types;
using model.timing.runner;

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

            void IEffect.Resolve(Game game) => game.flow.runnerTurn.ObserveStart(trigger);
            void IEffect.Observe(IImpactObserver observer, Game game) { }
        }

        private class WyldsideTrigger : IRunnerTurnStartObserver
        {
            void IRunnerTurnStartObserver.NotifyTurnStarted(Game game)
            {
                IEffect draw = new Draw(2);
                draw.Resolve(game);
                game.runner.clicks.Lose(1);
            }
        }
    }
}