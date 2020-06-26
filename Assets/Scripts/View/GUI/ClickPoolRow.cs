using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using model;

namespace view.gui
{
    public class ClickPoolRow : MonoBehaviour, IClickObserver
    {
        private Sprite clickSprite;

        private List<GameObject> clicks = new List<GameObject>();

        void Awake()
        {
            clickSprite = Resources.LoadAll<Sprite>("Images/UI/symbols").Where(r => r.name == "symbols_click").First();
        }

        void IClickObserver.NotifyClicks(int spent, int remaining)
        {
            var total = spent + remaining;
            RenderMissing(total);
            RemoveExtra(total);
            Paint(spent);
        }

        private void RenderMissing(int total)
        {
            while (clicks.Count < total)
            {
                clicks.Add(ClickBox.RenderClickBox(transform));
            }
        }

        private void RemoveExtra(int total)
        {
            var extra = clicks.Count - total;
            if (extra > 0)
            {
                foreach (GameObject click in clicks.GetRange(0, extra))
                {
                    Destroy(click);
                    clicks.Remove(click);
                }
            }
        }

        private void Paint(int spent)
        {
            for (int i = 0; i < clicks.Count; i++)
            {
                var image = clicks[i].GetComponent<Image>();
                if (i < spent)
                {
                    image.color = Color.red;
                }
                else
                {
                    image.color = Color.yellow;
                }
            }
        }
    }
}
