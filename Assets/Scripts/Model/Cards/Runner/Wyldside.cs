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

        IEffect ICard.PlayEffect => new WyldsideActivation();

        IType ICard.Type => new Resource();

        private class WyldsideActivation : IEffect
        {
            void IEffect.Resolve(Game game)
            {
                game.runner.turn.ObserveStart(new WyldsideTrigger());
            }

            void IEffect.Observe(IImpactObserver observer, Game game)
            {
                observer.NotifyImpact(true, this);
            }
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