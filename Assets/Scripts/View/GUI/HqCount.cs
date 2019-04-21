using model.zones.corp;
using UnityEngine;

namespace view.gui {
	public class HqCount : MonoBehaviour, IHqCountObserver {
		private PileCountObserver countObserver;

		public void Awake() {
			var embed = new GameObject("HQ card count");
			embed.AttachTo(gameObject);
			countObserver = new PileCount().Count(embed);
		}

		void IHqCountObserver.NotifyCardCount(int cards) {
			countObserver.UpdateCount(cards);
		}
	}
}
