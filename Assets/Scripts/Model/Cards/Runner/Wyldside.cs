using model.effects.runner;
using model.costs;
using model.cards.types;
using model.timing.runner;

namespace model.cards.runner
{
    public class Wyldside : ICard
    {
        string ICard.FaceupArt => "wyldside";

        string ICard.Name => "Wyldside";

        bool ICard.Faceup => false;

        Faction ICard.Faction => Factions.ANARCH;

        int ICard.InfluenceCost => 3;

        ICost ICard.PlayCost => new RunnerCreditCost(3);

        IEffect ICard.Activation => new WyldsideActivation();

        IType ICard.Type => new Resource();

        private class WyldsideActivation : IEffect
        {
            private WyldsideTrigger trigger = new WyldsideTrigger();

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