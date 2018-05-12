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
        private ActionPermission permission = new ActionPermission(false);

        public ActionCard()
        {
            draw = new Ability(new Conjunction(new RunnerClickCost(1), permission), new Draw(1));
            draw.ObserveResolution(this);
            credit = new Ability(new Conjunction(new RunnerClickCost(1), permission), new Gain(1));
            credit.ObserveResolution(this);
        }

        public Ability Play(ICard card)
        {
            Ability play = new Ability(new Conjunction(new RunnerClickCost(1), card.PlayCost, permission), new Play(card));
            play.ObserveResolution(this);
            return play;
        }

        public Ability Install(ICard card)
        {
            Ability install = new Ability(new Conjunction(new RunnerClickCost(1), card.PlayCost, permission), new Install(card));
            install.ObserveResolution(this);
            return install;
        }

        async public Task TakeAction()
        {
            permission.Grant();
            completion = new TaskCompletionSource<bool>();
            await completion.Task;
        }

        void IResolutionObserver.NotifyResolved()
        {
            completion.SetResult(true);
        }
    }
}