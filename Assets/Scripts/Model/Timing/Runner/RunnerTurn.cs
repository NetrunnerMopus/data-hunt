using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace model.timing.runner
{
    public class RunnerTurn : ITurn
    {
        private Runner runner;
        private Timing timing;
        ClickPool ITurn.Clicks => runner.clicks;
        Side ITurn.Side => Side.RUNNER;
        private List<IEffect> turnBeginningTriggers = new List<IEffect>();
        public int Number { get; private set; } = 0;
        public event AsyncAction<ITurn> Opened;
        public event AsyncAction<ITurn> Closed;
        public event AsyncAction<ITurn> TakingAction;
        public event AsyncAction<ITurn, Ability> ActionTaken;

        public RunnerTurn(Runner runner, Timing timing)
        {
            this.runner = runner;
        }

        async Task ITimingStructure.Open()
        {
            Number++;
            await Opened?.Invoke(this);
            await ActionPhase();
            await DiscardPhase();
            await Closed?.Invoke(this);
        }

        async private Task ActionPhase()
        {
            runner.clicks.Replenish(); // CR: 5.7.1.a
            var rez = OpenRezWindow(); // CR: 5.7.1.b
            var paid = OpenPaidWindow(); // CR: 5.7.1.b
            await rez;
            await paid;
            RefillRecurringCredits(); // CR: 5.7.1.c
            await TriggerTurnBeginning(); // CR: 5.7.1.d
            var rez2 = OpenRezWindow(); // CR: 5.7.1.e
            var paid2 = OpenPaidWindow(); // CR: 5.7.1.e
            await rez2;
            await paid2;
            while (runner.clicks.Remaining > 0) // CR: 5.7.1.g
            {
                await TakeAction(); // CR: 5.7.1.f
            }
            await timing.Checkpoint(); // CR: 5.7.1.g
        }

        async private Task OpenPaidWindow()
        {
            await timing.OpenPaidWindow(
                acting: game.runner.paidWindow,
                reacting: game.corp.paidWindow
            );
        }

        async private Task OpenRezWindow()
        {
            await game.corp.Rezzing.Window.Open();
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

        async private Task TakeAction()
        {
            var actionTaking = game.runner.Acting.TakeAction();
            TakingAction?.Invoke(this);
            var action = await actionTaking;
            ActionTaken?.Invoke(this, action);
            var rez = OpenRezWindow();
            var paid = OpenPaidWindow();
            await rez;
            await paid;
        }

        async private Task DiscardPhase()
        {
            await Discard(); // CR: 5.7.2.a
            var rez = OpenRezWindow(); // CR: 5.7.2.b
            var paid = OpenPaidWindow(); // CR: 5.7.2.b
            await rez;
            await paid;
            game.runner.clicks.Reset(); // CR: 5.7.2.c
            TriggerTurnEnding(); // CR: 5.7.2.d
            await game.Checkpoint(); // CR: 5.7.2.e
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

        public void WhenBegins(IEffect effect)
        {
            turnBeginningTriggers.Add(effect);
        }
    }
}
