using model.costs;
using model.effects.runner;
using model.cards;
using System.Threading.Tasks;

namespace model.play.runner
{
    public class ActionCard : IResolutionObserver
    {
        public readonly Ability draw;
        public readonly Ability credit;
        private TaskCompletionSource<bool> completion;

        public ActionCard()
        {
            draw = new Ability(new RunnerClickCost(1), new Draw(1));
            draw.ObserveResolution(this);
            credit = new Ability(new RunnerClickCost(1), new Gain(1));
            credit.ObserveResolution(this);
        }

        public Ability Play(ICard card)
        {
            Ability play = new Ability(new Conjunction(new RunnerClickCost(1), card.PlayCost), new Play(card));
            play.ObserveResolution(this);
            return play;
        }

        public Ability Install(ICard card)
        {
            Ability install = new Ability(new Conjunction(new RunnerClickCost(1), card.PlayCost), new Install(card));
            install.ObserveResolution(this);
            return install;
        }

        async public Task TakeAction()
        {
            completion = new TaskCompletionSource<bool>();
            await completion.Task;
        }

        void IResolutionObserver.NotifyResolved()
        {
            completion.SetResult(true);
        }
    }
}