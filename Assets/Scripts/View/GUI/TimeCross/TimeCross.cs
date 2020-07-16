﻿using UnityEngine;
using model;

namespace view.gui.timecross
{
    public class TimeCross
    {
        public PresentBox PresentBox { get; private set; }

        public TimeCross(Game game, DayNightCycle dayNight)
        {
            var pastTrack = GameObject.Find("Past").AddComponent<PastTrack>();
            pastTrack.DayNight = dayNight;
            game.corp.turn.ObserveActions(pastTrack);
            game.runner.turn.ObserveActions(pastTrack);
            var futureTrack = GameObject.Find("Future").AddComponent<FutureTrack>();
            futureTrack.Wire(game, dayNight);
            PresentBox = GameObject.Find("Present").AddComponent<PresentBox>();
            PresentBox.Wire(game, dayNight, futureTrack);
        }
    }
}
