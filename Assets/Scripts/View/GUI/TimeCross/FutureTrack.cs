using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using model;
using model.timing;
using System;

namespace view.gui.timecross
{
    public class FutureTrack : MonoBehaviour
    {
        void Awake()
        {
            var horizontal = gameObject.AddComponent<HorizontalLayoutGroup>();
            horizontal.childAlignment = TextAnchor.MiddleLeft;
            horizontal.childControlWidth = true;
            horizontal.childControlHeight = true;
            horizontal.childForceExpandWidth = false;
            horizontal.childForceExpandHeight = false;
        }

        public void Wire(Game game, DayNightCycle dayNight)
        {
            var currentTurn = new GameObject("Current turn").AddComponent<FutureTurn>();
            currentTurn.gameObject.AttachTo(gameObject);
            game.CurrentTurn += currentTurn.DisplayCurrent;
            currentTurn.dayNight = dayNight;
            var nextTurn = new GameObject("Next turn").AddComponent<FutureTurn>();
            nextTurn.gameObject.AttachTo(gameObject);
            game.NextTurn += nextTurn.DisplayNext;
            nextTurn.dayNight = dayNight;
        }
    }

    class FutureTurn : MonoBehaviour
    {
        private HorizontalLayoutGroup horizontal;
        private List<GameObject> renderedClicks = new List<GameObject>();
        private Image background;
        public DayNightCycle dayNight { private get; set; }
        private ClickPool monitoredClicks;

        void Awake()
        {
            horizontal = gameObject.AddComponent<HorizontalLayoutGroup>();
            horizontal.childAlignment = TextAnchor.MiddleLeft;
            horizontal.childControlWidth = true;
            horizontal.childControlHeight = true;
            horizontal.childForceExpandWidth = false;
            horizontal.childForceExpandHeight = true;
            background = gameObject.AddComponent<Image>();
        }

        internal void DisplayCurrent(object sender, ITurn turn)
        {
            dayNight.Paint(background, turn.Side);
            TrackClicks(turn, UpdateRemainingClicks);
        }

        internal void DisplayNext(object sender, ITurn turn)
        {
            dayNight.Paint(background, turn.Side);
            TrackClicks(turn, UpdateNextClicks);
        }

        private void TrackClicks(ITurn turn, EventHandler<ClickPool> update)
        {
            if (monitoredClicks != null)
            {
                monitoredClicks.Changed -= update;
            }
            monitoredClicks = turn.Clicks;
            monitoredClicks.Changed += update;
            update.Invoke(monitoredClicks, monitoredClicks);
        }

        void UpdateRemainingClicks(object sender, ClickPool clicks)
        {
            var desired = clicks.Remaining;
            AddMissing(desired);
            RemoveExtra(desired);
        }


        void UpdateNextClicks(object sender, ClickPool clicks)
        {
            var desired = clicks.NextReplenishment;
            AddMissing(desired);
            RemoveExtra(desired);
        }

        private void AddMissing(int desired)
        {
            while (renderedClicks.Count < desired)
            {
                Render();
            }
        }

        private void RemoveExtra(int desired)
        {
            var extra = renderedClicks.Count - desired;
            if (extra > 0)
            {
                foreach (var click in renderedClicks.GetRange(0, extra))
                {
                    Destroy(click);
                    renderedClicks.Remove(click);
                }
            }
        }

        private void Render()
        {
            var click = ClickBox.RenderClickBox(gameObject);
            // var aspect = click.AddComponent<AspectRatioFitter>();
            // aspect.aspectRatio = 1;
            // aspect.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            horizontal.CalculateLayoutInputHorizontal();
            horizontal.CalculateLayoutInputVertical();
            horizontal.SetLayoutHorizontal();
            horizontal.SetLayoutVertical();
            renderedClicks.Add(click);
        }
    }
}
