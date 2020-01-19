using UnityEngine;
using model.timing.corp;
using model;
using model.zones.corp;
using model.timing.runner;
using model.zones.runner;
using model.timing;
using System.Collections.Generic;
using model.play.corp;
using System.Threading.Tasks;

namespace view.log
{
    public class GameFlowLog :
        IStepObserver,
        IPaidWindowObserver,
        ICorpActionObserver,
        IRezWindowObserver,
        IHqDiscardObserver,
        IRunnerTurnStartObserver,
        IRunnerActionObserver,
        IGripDiscardObserver
    {
        private string currentStep = "";

        public void Display(Game game)
        {
            game.corp.paidWindow.ObserveWindow(this);
            game.runner.paidWindow.ObserveWindow(this);
            var runnerTurn = game.runner.turn;
            var corpTurn = game.corp.turn;
            corpTurn.ObserveSteps(this);
            corpTurn.ObserveActions(this);
            corpTurn.rezWindow.ObserveWindow(this);
            game.corp.zones.hq.ObserveDiscarding(this);
            runnerTurn.ObserveSteps(this);
            runnerTurn.ObserveStart(this);
            runnerTurn.ObserveActions(this);
            game.runner.zones.grip.ObserveDiscarding(this);
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

        void IPaidWindowObserver.NotifyPaidWindowOpened(PaidWindow window)
        {
            Log(window + " opened");
        }

        void IPaidWindowObserver.NotifyPaidWindowClosed(PaidWindow window)
        {
            Log(window + " closed");
        }

        void ICorpActionObserver.NotifyActionTaking()
        {
            Log("taking action");
        }

        void IHqDiscardObserver.NotifyDiscarding(bool discarding)
        {
            if (discarding)
            {
                Log("discarding");
            }
        }

        async Task IRunnerTurnStartObserver.NotifyTurnStarted(Game game)
        {
            Log("turn beginning");
            await Task.CompletedTask;
        }

        void IRunnerActionObserver.NotifyActionTaking()
        {
            Log("taking action");
        }

        void IGripDiscardObserver.NotifyDiscarding(bool discarding)
        {
            if (discarding)
            {
                Log("discarding");
            }
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