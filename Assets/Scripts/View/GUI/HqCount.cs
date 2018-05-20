using model.zones.corp;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class HqCount : MonoBehaviour, IHqCountObserver
    {
        private Text text;

        public void Awake()
        {
            var embed = new GameObject("HQ card count");
            embed.transform.parent = transform;
            text = embed.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            var rectangle = text.rectTransform;
            rectangle.anchorMin = new Vector2(0.2f, 0.2f);
            rectangle.anchorMax = new Vector2(0.8f, 0.8f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 1;
            text.resizeTextMaxSize = 100;
            text.alignment = TextAnchor.MiddleCenter;
            var outline = embed.AddComponent<Outline>();
            outline.effectDistance = new Vector2(2, 2);
        }

        void IHqCountObserver.NotifyCardCount(int cards)
        {
            text.text = cards.ToString();
        }
    }
}