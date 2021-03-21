using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;
using model.costs;
using model.zones;

namespace model.play
{
    public class CorpActing
    {
        private Corp corp;
        public readonly Ability credit;
        public readonly Ability draw;
        private TaskCompletionSource<Ability> actionTaking;
        private ActionPermission permission = new ActionPermission();
        private List<Ability> potentialActions = new List<Ability>();
        private IList<IActionPotentialObserver> actionPotentialObservers = new List<IActionPotentialObserver>();

        public CorpActing(Corp corp)
        {
            this.corp = corp;
            draw = new Ability(new Conjunction(corp.clicks.Spending(1), permission), corp.zones.Drawing(1));
            draw.Resolved += CompleteAction;
            credit = new Ability(new Conjunction(corp.clicks.Spending(1), permission), corp.credits.Gaining(1));
            credit.Resolved += CompleteAction;
            potentialActions.Add(draw);
            potentialActions.Add(credit);
            MarkPotential(draw);
            MarkPotential(credit);
            corp.zones.hq.Zone.Added += MarkPotential;
        }

        public Ability Play(Card card)
        {
            Ability play = new Ability(
                new Conjunction(
                    corp.clicks.Spending(1),
                    card.PlayCost,
                    permission,
                    new InZone(card, corp.zones.hq.Zone)
                ),
                corp.zones.Playing(card)
            );
            play.Resolved += CompleteAction;
            return play;
        }

        public Ability Install(Card card)
        {
            Ability install = new Ability(
                new Conjunction(
                    corp.clicks.Spending(1),
                    permission,
                    new InZone(card, corp.zones.hq.Zone)
                ),
                corp.Installing.InstallingCard(card)
            );
            install.Resolved += CompleteAction;
            return install;
        }

        async public Task<Ability> TakeAction()
        {
            permission.Grant();
            actionTaking = new TaskCompletionSource<Ability>();
            return await actionTaking.Task;
        }

        private void CompleteAction(Ability ability) => actionTaking.SetResult(ability);

        public void ObservePotentialActions(IActionPotentialObserver observer)
        {
            actionPotentialObservers.Add(observer);
            foreach (var action in potentialActions)
            {
                observer.NotifyPotentialAction(action);
            }
        }

        private void MarkPotential(Ability action)
        {
            potentialActions.Add(action);
            foreach (var observer in actionPotentialObservers)
            {
                observer.NotifyPotentialAction(action);
            }
        }

        private void MarkPotential(Zone zone, Card card)
        {
            foreach (var action in InferActions(card))
            {
                MarkPotential(action);
            }
        }

        private List<Ability> InferActions(Card card)
        {
            if (card.Type.Playable)
            {
                return new List<Ability> { Play(card) };
            }
            if (card.Type.Installable)
            {
                return new List<Ability> { Install(card) };
            }
            return new List<Ability>();
        }
    }

    public interface IActionPotentialObserver
    {
        void NotifyPotentialAction(Ability action);
    }
}
