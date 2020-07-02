using model.cards;
using model.costs;
using model.effects;
using model.effects.corp;
using model.player;
using model.zones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.play.corp
{
    public class ActionCard : IResolutionObserver, IZoneAdditionObserver
    {
        private zones.corp.Zones zones;
        private IPilot pilot;
        public readonly Ability credit;
        public readonly Ability draw;
        private TaskCompletionSource<Ability> actionTaking;
        private ActionPermission permission = new ActionPermission();
        private List<Ability> potentialActions = new List<Ability>();
        private HashSet<IActionPotentialObserver> actionPotentialObservers = new HashSet<IActionPotentialObserver>();

        public ActionCard(zones.corp.Zones zones, IPilot pilot)
        {
            this.zones = zones;
            this.pilot = pilot;
            zones.hq.Zone.ObserveAdditions(this);
            credit = new Ability(new Conjunction(new CorpClickCost(1), permission), new Gain(1));
            credit.ObserveResolution(this);
            MarkPotential(credit);
            draw = new Ability(new Conjunction(new CorpClickCost(1), permission), new Draw(1));
            draw.ObserveResolution(this);
            MarkPotential(draw);
        }

        public Ability Play(Card card)
        {
            Ability play = new Ability(
                new Conjunction(
                    new CorpClickCost(1),
                    card.PlayCost,
                    permission,
                    new InZone(card, zones.hq.Zone)
                ),
                new Play(card)
            );
            play.ObserveResolution(this);
            return play;
        }

        public Ability Install(Card card)
        {
            Ability install = new Ability(
                new Conjunction(
                    new CorpClickCost(1),
                    permission,
                    new InZone(card, zones.hq.Zone)
                ),
                new GenericInstall(card, pilot)
            );
            install.ObserveResolution(this);
            return install;
        }

        async public Task<Ability> TakeAction()
        {
            permission.Grant();
            actionTaking = new TaskCompletionSource<Ability>();
            return await actionTaking.Task;
        }

        void IResolutionObserver.NotifyResolved(Ability ability)
        {
            actionTaking.SetResult(ability);
        }

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

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
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
