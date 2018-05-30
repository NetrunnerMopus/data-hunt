using model.effects.corp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing.corp
{
    public class Turn
    {
        private Game game;
        private HashSet<IActionStepObserver> actionSteps = new HashSet<IActionStepObserver>();

        public Turn(Game game)
        {
            this.game = game;
        }

        async public Task Start()
        {
            // 1
            DrawPhase();
            // 2
            await ActionPhase();
            // 3
            await DiscardPhase();
        }

        private void DrawPhase()
        {
            // 1.1
            game.corp.clicks.Gain(3);
            // 1.2
            OpenPaidWindow();
            OpenRezWindow();
            OpenScoreWindow();
            // 1.3
            RefillRecurringCredits();
            // 1.4
            TriggerTurnBeginning();
            // 1.5
            MandatoryDraw();
        }

        private void OpenPaidWindow()
        {
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
            // 2.1
            OpenPaidWindow();
            OpenRezWindow();
            OpenScoreWindow();
            // 2.2
            await TakeActions();
        }

        async private Task TakeActions()
        {
            while (game.corp.clicks.Remaining() > 0)
            {
                UnityEngine.Debug.Log("Corp taking action");
                Task actionTaking = game.corp.actionCard.TakeAction();
                foreach (var observer in actionSteps)
                {
                    await observer.NotifyActionStep();
                }
                await actionTaking;
            }
        }

        async private Task DiscardPhase()
        {
            // 3.1
            await Discard();
            // 3.2
            OpenPaidWindow();
            OpenRezWindow();
            // 3.3
            LoseUnspentClicks();
            // 3.4
            TriggerTurnEnding();
        }

        async private Task Discard()
        {
            var hq = game.corp.zones.hq;
            while (hq.Count > 5)
            {
                UnityEngine.Debug.Log("Corp discarding");
                await hq.Discard();
            }
        }

        private void LoseUnspentClicks()
        {
        }

        private void TriggerTurnEnding()
        {
        }

        public void ObserveActionStep(IActionStepObserver observer)
        {
            actionSteps.Add(observer);
        }
    }

    public interface IActionStepObserver
    {
        Task NotifyActionStep();
    }
}