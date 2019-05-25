using model.costs;
using model.effects.runner;
using model.cards;
using System.Threading.Tasks;
using model.zones.runner;
using System.Collections.Generic;
using model.zones.corp;
using model.zones;

namespace model.play.runner
{
    public class ActionCard : IResolutionObserver, IZoneAdditionObserver
    {
        public readonly Ability draw;
        public readonly Ability credit;
        private TaskCompletionSource<bool> actionTaking;
        private ActionPermission permission = new ActionPermission();
        public List<Ability> potentialActions = new List<Ability>();

        public ActionCard()
        {
            draw = new Ability(new Conjunction(new RunnerClickCost(1), permission), new Draw(1));
            draw.ObserveResolution(this);
            credit = new Ability(new Conjunction(new RunnerClickCost(1), permission), new Gain(1));
            credit.ObserveResolution(this);
            potentialActions.Add(draw);
            potentialActions.Add(credit);
        }

        public Ability Play(Card card)
        {
            Ability play = new Ability(new Conjunction(new RunnerClickCost(1), card.PlayCost, permission), new Play(card));
            play.ObserveResolution(this);
            return play;
        }

        public Ability Install(Card card)
        {
            Ability install = new Ability(new Conjunction(new RunnerClickCost(1), card.PlayCost, permission), new Install(card));
            install.ObserveResolution(this);
            return install;
        }

        public Ability Run(IServer server)
        {
            Ability run = new Ability(new Conjunction(new RunnerClickCost(1), permission), new Run(server));
            run.ObserveResolution(this);
            return run;
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

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            if (card.Type.Playable)
            {
                potentialActions.Add(Play(card));
            }
            if (card.Type.Installable)
            {
                potentialActions.Add(Install(card));
            }
        }
    }
}