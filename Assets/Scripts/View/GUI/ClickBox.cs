using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace view.gui
{
    public static class ClickBox
    {
        private static Sprite CLICK_SPRITE = Resources.LoadAll<Sprite>("Images/UI/symbols").Where(r => r.name == "symbols_click").First();

        public static GameObject RenderClickBox(Transform parent)
        {
            var click = new GameObject("Click");
            var image = click.AddComponent<Image>();
            image.sprite = CLICK_SPRITE;
            image.preserveAspect = true;
            click.layer = 5;
            click.transform.SetParent(parent);
            return click;
        }
    }
}
