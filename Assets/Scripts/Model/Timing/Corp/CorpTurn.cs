using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace model.timing.corp
{
    public class CorpTurn : ITurn
    {
        private Game game;
        public readonly RezWindow rezWindow = new RezWindow();
        public bool Active { get; private set; } = false;
        ClickPool ITurn.Clicks => game.corp.clicks;
        Side ITurn.Side => Side.CORP;
        private IList<IEffect> turnBeginningTriggers = new List<IEffect>();
        private IList<IStepObserver> steps = new List<IStepObserver>();
        public event AsyncAction<ITurn> Started;
        private IList<ICorpActionObserver> actions = new List<ICorpActionObserver>();

        public CorpTurn(Game game)
        {
            this.game = game;
        }

        async Task ITurn.Start()
        {
            Active = true;
            await Started?.Invoke(this);
            await DrawPhase();
            await ActionPhase();
            await DiscardPhase();
            Active = false;
        }

        async private Task DrawPhase()
        {
            Step(1, 1);
            game.corp.clicks.Replenish();
            Step(1, 2);
            Task paid = OpenPaidWindow();
            Task rez = OpenRezWindow();
            await paid;
            await rez;
            OpenScoreWindow();
            Step(1, 3);
            RefillRecurringCredits();
            Step(1, 4);
            await TriggerTurnBeginning();
            Step(1, 5);
            await MandatoryDraw();
        }

        async private Task OpenPaidWindow()
        {
            await game.OpenPaidWindow(
                acting: game.corp.paidWindow,
                reacting: game.runner.paidWindow
            );
        }

        async private Task OpenRezWindow()
        {
            await rezWindow.Open();
        }

        private void OpenScoreWindow()
        {
        }

        private void RefillRecurringCredits()
        {
        }

        async private Task TriggerTurnBeginning()
        {
            if (turnBeginningTriggers.Count > 0)
            {
                await new SimultaneousTriggers(turnBeginningTriggers.Copy()).AllTriggered(game.corp.pilot);
            }
        }

        async private Task MandatoryDraw()
        {
            await game.corp.zones.Drawing(1).Resolve();
        }

        async private Task ActionPhase()
        {
            Step(2, 1);
            Task paid = OpenPaidWindow();
            Task rez = OpenRezWindow();
            await paid;
            await rez;
            OpenScoreWindow();
            Step(2, 2);
            await TakeActions();
        }

        async private Task TakeActions()
        {
            while (game.corp.clicks.Remaining > 0)
            {
                await TakeAction();
            }
        }

        async private Task TakeAction()
        {
            Task<Ability> actionTaking = game.corp.Acting.TakeAction();
            foreach (var observer in actions)
            {
                observer.NotifyActionTaking();
            }
            var action = await actionTaking;
            foreach (var observer in actions)
            {
                observer.NotifyActionTaken(action);
            }
            Task paid = OpenPaidWindow();
            Task rez = OpenRezWindow();
            OpenScoreWindow();
            await paid;
            await rez;
        }

        async private Task DiscardPhase()
        {
            Step(3, 1);
            await Discard();
            Step(3, 2);
            Task paid = OpenPaidWindow();
            Task rez = OpenRezWindow();
            await paid;
            await rez;
            Step(3, 3);
            game.corp.clicks.Reset();
            Step(3, 4);
            TriggerTurnEnding();
        }

        async private Task Discard()
        {
            var hq = game.corp.zones.hq;
            while (hq.Zone.Count > 5)
            {
                await hq.Discard();
            }
        }

        private void TriggerTurnEnding()
        {
        }

        private void Step(int phase, int step)
        {
            foreach (var observer in steps)
            {
                observer.NotifyStep("Corp turn", phase, step);
            }
        }

        public void WhenBegins(IEffect effect)
        {
            turnBeginningTriggers.Add(effect);
        }

        public void ObserveSteps(IStepObserver observer)
        {
            steps.Add(observer);
        }

        public void ObserveActions(ICorpActionObserver observer)
        {
            actions.Add(observer);
        }
    }

    public interface ICorpActionObserver
    {
        void NotifyActionTaking();
        void NotifyActionTaken(Ability ability);
    }
}
