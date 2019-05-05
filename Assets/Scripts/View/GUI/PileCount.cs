using model.zones;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui {
	public class PileCount : MonoBehaviour, IZoneCountObserver {
		private Text text;

		void Awake() {
			text = gameObject.AddComponent<Text>();
			text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			var rectangle = text.rectTransform;
			rectangle.anchorMin = new Vector2(0.2f, 0.2f);
			rectangle.anchorMax = new Vector2(0.8f, 0.8f);
			rectangle.offsetMin = Vector2.zero;
			rectangle.offsetMax = Vector2.zero;
			text.raycastTarget = false;
			text.resizeTextForBestFit = true;
			text.resizeTextMinSize = 1;
			text.resizeTextMaxSize = 100;
			text.alignment = TextAnchor.MiddleCenter;
			var outline = gameObject.AddComponent<Outline>();
			outline.effectDistance = new Vector2(2, 2);
		}

		void IZoneCountObserver.NotifyCount(int count) {
			text.text = count.ToString();
		}
	}
}
