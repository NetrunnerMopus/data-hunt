using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;
using model;
using System;

namespace model.timing.runner
{
    public class RunnerTurn : ITurn
    {
        private Game game;
        public bool Active { get; private set; } = false;
        ClickPool ITurn.Clicks => game.runner.clicks;
        Side ITurn.Side => Side.RUNNER;
        private List<IEffect> turnBeginningTriggers = new List<IEffect>();
        private HashSet<IStepObserver> steps = new HashSet<IStepObserver>();
        public event AsyncAction<ITurn> Started;
        private HashSet<IRunnerActionObserver> actions = new HashSet<IRunnerActionObserver>();

        public RunnerTurn(Game game)
        {
            this.game = game;
        }

        async Task ITurn.Start()
        {
            Active = true;
            await Started(this);
            await ActionPhase();
            await DiscardPhase();
            Active = false;
        }

        async private Task ActionPhase()
        {
            Step(1, 1);
            game.runner.clicks.Replenish();
            Step(1, 2);
            await OpenPaidWindow();
            OpenRezWindow();
            Step(1, 3);
            RefillRecurringCredits();
            Step(1, 4);
            await TriggerTurnBeginning();
            Step(1, 5);
            await OpenPaidWindow();
            OpenRezWindow();

            Step(1, 6);
            await TakeActions();
        }

        async private Task OpenPaidWindow()
        {
            await game.OpenPaidWindow(
                acting: game.runner.paidWindow,
                reacting: game.corp.paidWindow
            );
        }

        private void OpenRezWindow()
        {

        }

        private void RefillRecurringCredits()
        {

        }

        async private Task TriggerTurnBeginning()
        {
            if (turnBeginningTriggers.Count > 0)
            {
                await new SimultaneousTriggers(turnBeginningTriggers.Copy()).AllTriggered(game.runner.pilot);
            }
        }

        async private Task TakeActions()
        {
            while (game.runner.clicks.Remaining > 0)
            {
                Task<Ability> actionTaking = game.runner.Acting.TakeAction();
                foreach (var observer in actions)
                {
                    observer.NotifyActionTaking();
                }
                var action = await actionTaking;
                foreach (var observer in actions)
                {
                    observer.NotifyActionTaken(action);
                }
                await OpenPaidWindow();
                OpenRezWindow();
            }
        }

        async private Task DiscardPhase()
        {
            Step(2, 1);
            await Discard();
            Step(2, 2);
            await OpenPaidWindow();
            OpenRezWindow();
            Step(2, 3);
            game.runner.clicks.Reset();
            Step(2, 4);
            TriggerTurnEnding();
        }

        async private Task Discard()
        {
            var grip = game.runner.zones.grip;
            while (grip.zone.Count > 5)
            {
                await grip.Discard();
            }
        }

        private void TriggerTurnEnding()
        {

        }

        private void Step(int phase, int step)
        {
            foreach (var observer in steps)
            {
                observer.NotifyStep("Runner turn", phase, step);
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

        public void ObserveActions(IRunnerActionObserver observer)
        {
            actions.Add(observer);
        }
    }

    public interface IRunnerActionObserver
    {
        void NotifyActionTaking();
        void NotifyActionTaken(Ability ability);
    }
}
