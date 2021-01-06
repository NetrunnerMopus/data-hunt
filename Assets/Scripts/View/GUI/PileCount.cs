using model;
using model.zones;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace view.gui {
	public class PileCount : MonoBehaviour, IZoneCountObserver, IBalanceObserver, IPointerEnterHandler, IPointerExitHandler {
		private Text text;

		void Awake() {
			var count = new GameObject("Pile Count");
			text = count.AddComponent<Text>();
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
			text.enabled = false;
			var outline = count.AddComponent<Outline>();
			outline.effectDistance = new Vector2(2, 2);
			count.AttachTo(gameObject);
		}

		void IZoneCountObserver.NotifyCount(int count) {
			text.text = count.ToString();
		}

        void IBalanceObserver.NotifyBalance(int balance)
        {
            text.text = balance.ToString();
        }

		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
			text.enabled = true;
			text.gameObject.transform.SetAsLastSibling();
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {
			text.enabled = false;
		}
	}
}
