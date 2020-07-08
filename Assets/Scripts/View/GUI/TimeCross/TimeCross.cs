using UnityEngine;
using model;
using model.timing.runner;
using model.play;

namespace view.gui.timecross
{
    public class TimeCross : IRunnerActionObserver
    {
        private PastTrack pastTrack;
        private FutureTrack futureTrack;

        public TimeCross(Game game, DayNightCycle dayNight)
        {
            pastTrack = GameObject.Find("Past").AddComponent<PastTrack>();
            pastTrack.DayNight = dayNight;
            game.corp.turn.ObserveActions(pastTrack);
            game.runner.turn.ObserveActions(pastTrack);
            futureTrack = GameObject.Find("Future").AddComponent<FutureTrack>();
            futureTrack.Wire(game, dayNight);
        }

        void IRunnerActionObserver.NotifyActionTaking()
        {
            throw new System.NotImplementedException();
        }


        void IRunnerActionObserver.NotifyActionTaken(Ability ability)
        {
            throw new System.NotImplementedException();
        }

    }
}
