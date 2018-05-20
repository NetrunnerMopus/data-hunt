using System.Threading.Tasks;

namespace model.timing.runner
{
    public class Turn
    {
        private Game game;

        public Turn(Game game)
        {
            this.game = game;
        }

        async public Task Start()
        {
            // 1
            await ActionPhase();
            // 2
            await DiscardPhase();
        }

        async private Task ActionPhase()
        {
            // 1.1
            game.runner.clicks.Gain(4);
            // 1.2
            OpenPaidWindow();
            OpenRezWindow();
            ClosePaidWindow();
            CloseRezWindow();
            // 1.3
            RefillRecurringCredits();
            // 1.4
            TriggerTurnBeginning();
            // 1.5
            OpenPaidWindow();
            OpenRezWindow();
            ClosePaidWindow();
            CloseRezWindow();
            // 1.6
            await TakeActions();
        }

        private void OpenPaidWindow()
        {

        }

        private void ClosePaidWindow()
        {

        }

        private void OpenRezWindow()
        {

        }

        private void CloseRezWindow()
        {

        }

        private void RefillRecurringCredits()
        {

        }

        private void TriggerTurnBeginning()
        {

        }

        async private Task TakeActions()
        {
            UnityEngine.Debug.Log("Runner taking actions");
            while (game.runner.clicks.Remaining() > 0)
            {
                await game.runner.actionCard.TakeAction();
                UnityEngine.Debug.Log("Runner action taken");
                OpenPaidWindow();
                OpenRezWindow();
                ClosePaidWindow();
                CloseRezWindow();
            }
        }

        async private Task DiscardPhase()
        {
            // 2.1
            await Discard();
            // 2.2
            OpenPaidWindow();
            OpenRezWindow();
            ClosePaidWindow();
            CloseRezWindow();
            // 2.3
            game.runner.clicks.Reset();
            // 2.4
            TriggerTurnEnding();
        }

        async private Task Discard()
        {
            while (game.runner.zones.grip.Count > 5)
            {
                UnityEngine.Debug.Log("Discarding");
                await game.runner.zones.grip.Discard();
            }
        }

        private void TriggerTurnEnding()
        {

        }
    }
}