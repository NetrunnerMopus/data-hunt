using UnityEngine;
using UnityEngine.UI;
using model;
using model.play;
using model.timing.runner;
using controller;

namespace view.gui.timecross
{
    public class PresentBox : MonoBehaviour, IRunnerActionObserver
    {
        public DropZone PresentClick { get; private set; }
        public GameObject BankCredit { get; private set; }
        private Game game;
        private DayNightCycle dayNight;
        private FutureTrack future;

        internal void Wire(Game game, DayNightCycle dayNight, FutureTrack future)
        {
            this.game = game;
            this.dayNight = dayNight;
            this.future = future;
            var clickChoice = GameObject.Find("Click choice");
            var background = clickChoice.GetComponent<Image>();
            dayNight.Paint(background, Side.RUNNER);
            PresentClick = clickChoice.AddComponent<DropZone>();
            BankCredit = GameObject.Find("Bank/Credit");
            SetActions(false);
            game.runner.turn.ObserveActions(this);
        }

        private void SetActions(bool takingAction)
        {
            PresentClick.gameObject.SetActive(takingAction);
            BankCredit.SetActive(takingAction);
        }

        void IRunnerActionObserver.NotifyActionTaking()
        {
            SetActions(true);
            future.CurrentTurn.UpdateClicks(game.runner.clicks.Remaining - 1);
        }

        void IRunnerActionObserver.NotifyActionTaken(Ability ability)
        {
            SetActions(false);
        }
    }
}
