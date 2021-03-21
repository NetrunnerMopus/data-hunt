using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace model.timing.corp
{
    public class CorpTurn : ITurn
    {
        private Game game;
        public bool Active { get; private set; } = false;
        ClickPool ITurn.Clicks => game.corp.clicks;
        Side ITurn.Side => Side.CORP;
        private IList<IEffect> turnBeginningTriggers = new List<IEffect>();
        public event AsyncAction<ITurn> Started;
        public event AsyncAction<ITurn> TakingAction;
        public event AsyncAction<ITurn, Ability> ActionTaken;

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
            game.corp.clicks.Replenish(); // CR: 5.6.1.a
            var rez = OpenRezWindow(); // CR: 5.6.1.b
            var score = OpenScoreWindow(); // CR: 5.6.1.b
            var paid = OpenPaidWindow(); // CR: 5.6.1.b
            await rez;
            await score;
            await paid;
            RefillRecurringCredits(); // CR: 5.6.1.c
            await TriggerTurnBeginning(); // CR: 5.6.1.d
            await game.Checkpoint();// CR: 5.6.1.e
            await MandatoryDraw(); // CR: 5.6.1.f
            await game.Checkpoint(); // CR: 5.6.1.g
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
            await game.corp.Rezzing.Window.Open();
        }

        async private Task OpenScoreWindow()
        {
            await Task.FromResult("TODO let corp score");
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
            var rez = OpenRezWindow(); // CR: 5.6.2.a
            var score = OpenScoreWindow(); // CR: 5.6.2.a
            var paid = OpenPaidWindow(); // CR: 5.6.2.a
            await rez;
            await score;
            await paid;
            while (game.corp.clicks.Remaining > 0) // CR: 5.6.2.c
            {
                await TakeAction(); // CR: 5.6.2.b
            }
            await game.Checkpoint(); // CR: 5.6.2.d
        }

        async private Task TakeAction()
        {
            var actionTaking = game.corp.Acting.TakeAction();
            TakingAction?.Invoke(this);
            var action = await actionTaking;
            ActionTaken?.Invoke(this, action);
            var rez = OpenRezWindow();
            var score = OpenScoreWindow();
            var paid = OpenPaidWindow();
            await rez;
            await score;
            await paid;
        }

        async private Task DiscardPhase()
        {
            await Discard(); // CR: 5.6.3.a
            var rez = OpenRezWindow();  // CR: 5.6.3.b
            var paid = OpenPaidWindow();  // CR: 5.6.3.b
            await rez;
            await paid;
            game.corp.clicks.Reset(); // CR: 5.6.3.c
            TriggerTurnEnding(); // CR: 5.6.3.d
            await game.Checkpoint(); // CR: 5.6.3.e
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

        public void WhenBegins(IEffect effect)
        {
            turnBeginningTriggers.Add(effect);
        }
    }
}
