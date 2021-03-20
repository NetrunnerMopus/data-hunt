using System.Threading.Tasks;
using model.cards;
using model.costs;
using model.zones.corp;

namespace model.play
{
    public class RunnerActing
    {
        private Runner runner;
        public readonly Ability draw;
        public readonly Ability credit;
        private TaskCompletionSource<Ability> actionTaking;
        private ActionPermission permission = new ActionPermission();

        public RunnerActing(Runner runner)
        {
            this.runner = runner;
            draw = new Ability(new Conjunction(runner.clicks.Spending(1), permission), runner.zones.Drawing(1));
            draw.Resolved += CompleteAction;
            credit = new Ability(new Conjunction(runner.clicks.Spending(1), permission), runner.credits.Gaining(1));
            credit.Resolved += CompleteAction;
        }

        private void CompleteAction(Ability ability) => actionTaking.SetResult(ability);

        public Ability Play(Card card)
        {
            Ability play = new Ability(new Conjunction(runner.clicks.Spending(1), card.PlayCost, permission), runner.zones.Playing(card));
            play.Resolved += CompleteAction;
            return play;
        }

        public Ability Install(Card card)
        {
            Ability install = new Ability(new Conjunction(runner.clicks.Spending(1), permission), runner.Installing.InstallingCard(card));
            install.Resolved += CompleteAction;
            return install;
        }

        public Ability Run(IServer server)
        {
            Ability run = new Ability(new Conjunction(runner.clicks.Spending(1), permission), runner.Running.RunningOn(server));
            run.Resolved += CompleteAction;
            return run;
        }

        async public Task<Ability> TakeAction()
        {
            permission.Grant();
            actionTaking = new TaskCompletionSource<Ability>();
            return await actionTaking.Task;
        }
    }
}
