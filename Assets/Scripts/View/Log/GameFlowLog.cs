using System.Collections.Generic;
using System.Threading.Tasks;
using model;
using model.play;
using model.timing;
using model.timing.corp;
using UnityEngine;

namespace view.log
{
    public class GameFlowLog :
        IStepObserver,
        IRezWindowObserver
    {
        private string currentStep = "";

        public void Display(Game game)
        {
            game.corp.paidWindow.Opened += LogOpened;
            game.corp.paidWindow.Closed += LogClosed;
            game.runner.paidWindow.Opened += LogOpened;
            game.runner.paidWindow.Closed += LogClosed;
            var runnerTurn = game.runner.turn;
            var corpTurn = game.corp.turn;
            corpTurn.ObserveSteps(this);
            corpTurn.Started += TurnStarted;
            corpTurn.TakingAction += (turn) => LogAsync("corp taking action");
            corpTurn.ActionTaken += (turn, action) => LogAsync("corp took action");
            corpTurn.rezWindow.ObserveWindow(this);
            game.corp.zones.hq.DiscardingOne += () => Log("discarding");
            runnerTurn.ObserveSteps(this);
            runnerTurn.Started += TurnStarted;
            runnerTurn.TakingAction += (turn) => LogAsync("runner taking action");
            runnerTurn.ActionTaken += (turn, action) => LogAsync("runner took action");
            game.runner.zones.grip.DiscardingOne += () => Log("discarding");
        }

        void IStepObserver.NotifyStep(string structure, int phase, int step)
        {
            currentStep = $"{structure}, step {phase}.{step}: ";
            Debug.Log(currentStep);
        }

        async private Task LogAsync(string message)
        {
            Debug.Log(currentStep + message);
            await Task.CompletedTask;
        }

        private void Log(string message)
        {
            Debug.Log(currentStep + message);
        }

        private void LogOpened(PaidWindow window)
        {
            Log(window + " opened");
        }

        private void LogClosed(PaidWindow window)
        {
            Log(window + " closed");
        }

        async private Task TurnStarted(ITurn turn)
        {
            Log("turn " + turn + " beginning");
            await Task.CompletedTask;
        }

        void IRezWindowObserver.NotifyRezWindowOpened(List<Rezzable> rezzables)
        {
            Log("rez window opened, up to " + rezzables.Count + " could be rezzed");
        }

        void IRezWindowObserver.NotifyRezWindowClosed()
        {
            Log("rez window closed");
        }
    }
}
