using model.effects.corp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing.corp
{
    public class Turn
    {
        private Game game;

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
            await OpenPaidWindow();
            OpenRezWindow();
            OpenScoreWindow();
            Step(1, 3);
            RefillRecurringCredits();
            Step(1, 4);
            TriggerTurnBeginning();
            Step(1, 5);
            MandatoryDraw();
        }

        async private Task OpenPaidWindow()
        {
            await game.flow.paidWindow.Open();
        }

        private void OpenRezWindow()
        {
        }

        private void OpenScoreWindow()
        {
        }

        private void RefillRecurringCredits()
        {
        }

        private void TriggerTurnBeginning()
        {
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
            await OpenPaidWindow();
            OpenRezWindow();
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
            }
        }

        async private Task DiscardPhase()
        {
            Step(3, 1);
            await Discard();
            Step(3, 2);
            await OpenPaidWindow();
            OpenRezWindow();
            Step(3, 3);
            LoseUnspentClicks();
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

        private void LoseUnspentClicks()
        {
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

        internal void ObserveSteps(IStepObserver observer)
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