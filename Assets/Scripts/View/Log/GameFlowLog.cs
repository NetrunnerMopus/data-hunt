using System.Collections.Generic;
using System.Threading.Tasks;
using model;
using model.play;
using model.timing;
using model.timing.corp;
using model.timing.runner;
using UnityEngine;

namespace view.log
{
    public class GameFlowLog :
        IStepObserver,
        ICorpActionObserver,
        IRezWindowObserver,
        IRunnerActionObserver
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
            corpTurn.ObserveActions(this);
            corpTurn.rezWindow.ObserveWindow(this);
            game.corp.zones.hq.DiscardingOne += () => Log("discarding");
            runnerTurn.ObserveSteps(this);
            runnerTurn.Started += TurnStarted;
            runnerTurn.ObserveActions(this);
            game.runner.zones.grip.DiscardingOne += () => Log("discarding");
        }

        void IStepObserver.NotifyStep(string structure, int phase, int step)
        {
            currentStep = $"{structure}, step {phase}.{step}: ";
            Debug.Log(currentStep);
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

        void ICorpActionObserver.NotifyActionTaking()
        {
            Log("taking action");
        }

        void ICorpActionObserver.NotifyActionTaken(Ability ability)
        {
            Log("corp action taken");
        }

        async private Task TurnStarted(ITurn turn)
        {
            Log("turn " + turn + " beginning");
            await Task.CompletedTask;
        }

        void IRunnerActionObserver.NotifyActionTaking()
        {
            Log("taking action");
        }

        void IRunnerActionObserver.NotifyActionTaken(Ability ability)
        {
            Log("runner action taken");
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
