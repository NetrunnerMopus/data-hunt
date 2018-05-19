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
            game.corp.clicks.Gain(3);
            await TakeActions();
        }

        async private Task TakeActions()
        {
            UnityEngine.Debug.Log("Corp taking actions");
            while (game.corp.clicks.Remaining() > 0)
            {
                Task actionTaking = game.corp.actionCard.TakeAction();
                foreach (var observer in actionSteps)
                {
                    await observer.NotifyActionStep();
                }
                await actionTaking;
                UnityEngine.Debug.Log("Corp action taken");
            }
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