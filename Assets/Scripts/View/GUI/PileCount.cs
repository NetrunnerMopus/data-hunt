using UnityEngine;
using UnityEngine.UI;

namespace view.gui {
	public class PileCount {
		public PileCountObserver Count(GameObject embed) {
			var text = embed.AddComponent<Text>();
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
			return new PileCountObserver(text);
		}
	}

	public class PileCountObserver {
		private readonly Text text;

		public PileCountObserver(Text text) {
			this.text = text;
		}

		public void UpdateCount(int count) {
			text.text = count.ToString();
		}
	}
}
