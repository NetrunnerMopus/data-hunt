using UnityEngine;
using UnityEngine.UI;
using model;
using model.play;
using model.timing.runner;
using model.zones.runner;
using model.zones.corp;
using model.timing.corp;

namespace view.gui.timecross
{
    public class PresentBox : MonoBehaviour, IRunnerActionObserver, ICorpActionObserver, IGripDiscardObserver, IHqDiscardObserver
    {
        public GameObject RunnerActionPhase { get; private set; }
        private GameObject corpActionPhase;
        public GameObject BankCredit { get; private set; }
        private GameObject discardPhase;
        private Image discardBackground;
        private Game game;
        private DayNightCycle dayNight;
        private FutureTrack future;

        internal void Wire(Game game, DayNightCycle dayNight, FutureTrack future)
        {
            this.game = game;
            this.dayNight = dayNight;
            this.future = future;
            WireRunnerActionPhase(game);
            WireCorpActionPhase(game);
            WireDiscardPhase(game);
        }

        private void WireRunnerActionPhase(Game game)
        {
            RunnerActionPhase = GameObject.Find("Runner action phase");
            var background = RunnerActionPhase.GetComponent<Image>();
            dayNight.Paint(background, Side.RUNNER);
            BankCredit = GameObject.Find("Bank/Credit");
            SetRunnerActions(false);
            game.runner.turn.ObserveActions(this);
        }

        private void SetRunnerActions(bool takingAction)
        {
            RunnerActionPhase.gameObject.SetActive(takingAction);
            BankCredit.SetActive(takingAction);
        }

        private void WireCorpActionPhase(Game game)
        {
            corpActionPhase = GameObject.Find("Corp action phase");
            var background = corpActionPhase.GetComponent<Image>();
            dayNight.Paint(background, Side.CORP);
            corpActionPhase.SetActive(false);
            game.corp.turn.ObserveActions(this);
        }

        private void WireDiscardPhase(Game game)
        {
            discardPhase = GameObject.Find("Discard phase");
            discardBackground = discardPhase.GetComponent<Image>();
            discardPhase.SetActive(false);
            game.runner.zones.grip.ObserveDiscarding(this);
            game.corp.zones.hq.ObserveDiscarding(this);
        }

        void IRunnerActionObserver.NotifyActionTaking()
        {
            SetRunnerActions(true);
            future.CurrentTurn.UpdateClicks(game.runner.clicks.Remaining - 1);
        }

        void IRunnerActionObserver.NotifyActionTaken(Ability ability)
        {
            SetRunnerActions(false);
        }

        void ICorpActionObserver.NotifyActionTaking()
        {
            corpActionPhase.SetActive(true);
            future.CurrentTurn.UpdateClicks(game.corp.clicks.Remaining - 1);
        }

        void ICorpActionObserver.NotifyActionTaken(Ability ability)
        {
            corpActionPhase.SetActive(false);
        }


        void IGripDiscardObserver.NotifyDiscarding(bool discarding)
        {
            discardPhase.SetActive(discarding);
            if (discarding)
            {
                dayNight.Paint(discardBackground, Side.RUNNER);
            }
        }

        void IHqDiscardObserver.NotifyDiscarding(bool discarding)
        {
            discardPhase.SetActive(discarding);
            if (discarding)
            {
                dayNight.Paint(discardBackground, Side.CORP);
            }
        }
    }
}
