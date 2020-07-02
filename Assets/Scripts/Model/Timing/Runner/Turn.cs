using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace model.timing.runner
{
    public class Turn
    {
        private Game game;
        private HashSet<IStepObserver> steps = new HashSet<IStepObserver>();
        private HashSet<IRunnerTurnStartObserver> starts = new HashSet<IRunnerTurnStartObserver>();
        private HashSet<IRunnerActionObserver> actions = new HashSet<IRunnerActionObserver>();

        public Turn(Game game)
        {
            this.game = game;
        }

        async public Task Start()
        {
            await ActionPhase();
            await DiscardPhase();
        }

        async private Task ActionPhase()
        {
            Step(1, 1);
            game.runner.clicks.Gain(4);
            Step(1, 2);
            await OpenPaidWindow();
            OpenRezWindow();
            Step(1, 3);
            RefillRecurringCredits();
            Step(1, 4);
            TriggerTurnBeginning();
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

        private void TriggerTurnBeginning()
        {
            foreach (var observer in starts)
            {
                observer.NotifyTurnStarted(game);
            }
        }

        async private Task TakeActions()
        {
            while (game.runner.clicks.Remaining() > 0)
            {
                Task<Ability> actionTaking = game.runner.actionCard.TakeAction();
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

        public void ObserveSteps(IStepObserver observer)
        {
            steps.Add(observer);
        }

        public void ObserveStart(IRunnerTurnStartObserver observer)
        {
            starts.Add(observer);
        }

        public void UnobserveStart(IRunnerTurnStartObserver observer)
        {
            starts.Remove(observer);
        }

        public void ObserveActions(IRunnerActionObserver observer)
        {
            actions.Add(observer);
        }
    }

    public interface IRunnerTurnStartObserver
    {
        Task NotifyTurnStarted(Game game);
    }

    public interface IRunnerActionObserver
    {
        void NotifyActionTaking();
        void NotifyActionTaken(Ability ability);
    }
}
