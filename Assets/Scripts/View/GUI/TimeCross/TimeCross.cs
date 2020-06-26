using UnityEngine;
using model;
using model.timing.runner;

namespace view.gui.timecross
{
    public class TimeCross: IRunnerActionObserver
    {
        private PastTrack pastTrack;
        private FutureTrack futureTrack;

        public TimeCross(Game game)
        {
            pastTrack = GameObject.Find("Past").AddComponent<PastTrack>();
            game.corp.clicks.Observe(pastTrack);
            game.runner.clicks.Observe(pastTrack);
            futureTrack = GameObject.Find("Future").AddComponent<FutureTrack>();
            game.corp.clicks.Observe(futureTrack);
            game.runner.clicks.Observe(futureTrack);
        }

        void IRunnerActionObserver.NotifyActionTaking()
        {
            throw new System.NotImplementedException();
        }
    }
}
