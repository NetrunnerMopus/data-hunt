using model.costs;
using model.effects.corp;
using System.Threading.Tasks;

namespace model.play.corp
{
    public class ActionCard : IResolutionObserver
    {
        public readonly Ability credit;
        private TaskCompletionSource<bool> completion;
        private ActionPermission permission = new ActionPermission(false);

        public ActionCard()
        {
            credit = new Ability(new Conjunction(new CorpClickCost(1), permission), new Gain(1));
            credit.ObserveResolution(this);
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