using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;
using model.player;

namespace model.timing.runner {
    public class RunnerTurn : ITurn {
        private Runner runner;
        private Timing timing;
        override public ClickPool Clicks => runner.clicks;
        override public Side Side => Side.RUNNER;
        override public IPilot Owner => runner.pilot;
        private List<IEffect> turnBeginningTriggers = new List<IEffect>();
        public event AsyncAction<ITurn> TakingAction;
        public event AsyncAction<ITurn, Ability> ActionTaken;

        public RunnerTurn(Runner runner, Timing timing, int turnNumber): base("Runner turn " + turnNumber) {

        }

        override async protected Task Proceed() {
            await ActionPhase();
            await DiscardPhase();
        }

        async private Task ActionPhase() {
            runner.clicks.Replenish(); // CR: 5.7.1.a
            await timing.DefinePaidWindow(rezzing: true, scoring: false).Open(); // CR: 5.7.1.b
            RefillRecurringCredits(); // CR: 5.7.1.c
            await Begins.Open(); // CR: 5.7.1.d
            await timing.DefinePaidWindow(rezzing: true, scoring: false).Open(); // CR: 5.7.1.e
            while (runner.clicks.Remaining > 0) // CR: 5.7.1.g
            {
                await TakeAction(); // CR: 5.7.1.f
            }
            await timing.Checkpoint(); // CR: 5.7.1.g
        }

        private void RefillRecurringCredits() {

        }

        async private Task TakeAction() {
            var actionTaking = game.runner.Acting.TakeAction();
            TakingAction?.Invoke(this);
            var action = await actionTaking;
            ActionTaken?.Invoke(this, action);
            await timing.DefinePaidWindow(rezzing: true, scoring: false).Open();
        }

        async private Task DiscardPhase() {
            await Discard(); // CR: 5.7.2.a
            await timing.DefinePaidWindow(rezzing: true, scoring: false).Open(); // CR: 5.7.2.b
            game.Runner.clicks.Reset(); // CR: 5.7.2.c
            TriggerTurnEnding(); // CR: 5.7.2.d
            await timing.Checkpoint(); // CR: 5.7.2.e
        }

        async private Task Discard() {
            var grip = game.runner.zones.grip;
            while (grip.zone.Count > 5) {
                await grip.Discard();
            }
        }

        private void TriggerTurnEnding() {
        }
    }
}
