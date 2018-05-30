using model.costs;
using model.effects.corp;
using System.Threading.Tasks;

namespace model.play.corp
{
    public class ActionCard : IResolutionObserver
    {
        public readonly Ability credit;
        private TaskCompletionSource<bool> actionTaking;
        private ActionPermission permission = new ActionPermission();

        public ActionCard()
        {
            credit = new Ability(new Conjunction(new CorpClickCost(1), permission), new Gain(1));
            credit.ObserveResolution(this);
        }

        async public Task TakeAction()
        {
            permission.Grant();
            actionTaking = new TaskCompletionSource<bool>();
            await actionTaking.Task;
            permission.Revoke();
        }

        void IResolutionObserver.NotifyResolved()
        {
            actionTaking.SetResult(true);
        }
    }
}