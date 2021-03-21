using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model;
using model.play;
using model.timing;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui.timecross
{
    public class PastTrack : MonoBehaviour
    {
        private Sprite clickSprite;
        private HorizontalLayoutGroup horizontal;
        private List<GameObject> renderedClicks = new List<GameObject>();

        public DayNightCycle DayNight { private get; set; }

        void Awake()
        {
            clickSprite = Resources.LoadAll<Sprite>("Images/UI/symbols").Where(r => r.name == "symbols_click").First();
            horizontal = gameObject.AddComponent<HorizontalLayoutGroup>();
            horizontal.childAlignment = TextAnchor.MiddleRight;
            horizontal.childControlWidth = false;
            horizontal.childControlHeight = true;
            horizontal.childForceExpandWidth = false;
            horizontal.childForceExpandHeight = true;
        }

        internal void Wire(Game game)
        {
            game.corp.turn.ActionTaken += RenderCompleteCorpAction;
            game.runner.turn.ActionTaken += RenderCompleteRunnerAction;
        }

        async private Task RenderCompleteCorpAction(ITurn turn, Ability ability)
        {
            var envelope = new GameObject("Corp envelope");
            var background = envelope.AddComponent<Image>();
            DayNight.Paint(background, Side.CORP);
            envelope.AttachTo(gameObject);
            RenderAction(ability, envelope);
            Expand(envelope);
            await Task.CompletedTask;
        }

        private void RenderAction(Ability ability, GameObject parent)
        {
            var pastAction = new GameObject("Past action " + ability);
            var rect = pastAction.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.1f, 0.1f);
            rect.anchorMax = new Vector2(0.9f, 0.9f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            foreach (var asset in ability.effect.Graphics)
            {
                var image = pastAction.AddComponent<Image>();
                image.sprite = Resources.Load<Sprite>(asset);
                image.preserveAspect = true;
            }
            pastAction.layer = 5;
            pastAction.AttachTo(parent);
        }

        async private Task RenderCompleteRunnerAction(ITurn turn, Ability ability)
        {
            var envelope = new GameObject("Runner envelope");
            var background = envelope.AddComponent<Image>();
            DayNight.Paint(background, Side.RUNNER);
            envelope.AttachTo(gameObject);
            RenderAction(ability, envelope);
            Expand(envelope);
            await Task.CompletedTask;
        }


        private void Expand(GameObject gameObject)
        {
            var aspect = gameObject.AddComponent<AspectRatioFitter>();
            aspect.aspectRatio = 1;
            aspect.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            horizontal.CalculateLayoutInputHorizontal();
            horizontal.CalculateLayoutInputVertical();
            horizontal.SetLayoutHorizontal();
            horizontal.SetLayoutVertical();
        }
    }
}
