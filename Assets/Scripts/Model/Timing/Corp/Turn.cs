using model.effects.corp;
using model.play;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing.corp
{
    public class Turn
    {
        private Game game;
        public readonly RezWindow rezWindow = new RezWindow();
        public List<IEffect> turnBeginningTriggers = new List<IEffect>();
        private HashSet<IStepObserver> steps = new HashSet<IStepObserver>();
        private HashSet<ICorpActionObserver> actions = new HashSet<ICorpActionObserver>();

        public Turn(Game game)
        {
            this.game = game;
        }

        async public Task Start()
        {
            await DrawPhase();
            await ActionPhase();
            await DiscardPhase();
        }

        async private Task DrawPhase()
        {
            Step(1, 1);
            game.corp.clicks.Gain(3);
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
            MandatoryDraw();
        }

        async private Task OpenPaidWindow()
        {
            await game.flow.paidWindow.Open();
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
                await new SimultaneousTriggers(turnBeginningTriggers).AllTriggered(game.corp.pilot, game);
            }
        }

        private void MandatoryDraw()
        {
            var rd = game.corp.zones.rd;
            if (rd.HasCards())
            {
                IEffect draw = new Draw(1);
                draw.Resolve(game);
            }
            else
            {
                game.flow.DeckCorp();
            }
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
            while (game.corp.clicks.Remaining() > 0)
            {
                Task actionTaking = game.corp.actionCard.TakeAction();
                foreach (var observer in actions)
                {
                    observer.NotifyActionTaking();
                }
                await actionTaking;
                Task paid = OpenPaidWindow();
                Task rez = OpenRezWindow();
                await paid;
                await rez;
                OpenScoreWindow();
            }
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
            while (hq.Count > 5)
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
    }
}