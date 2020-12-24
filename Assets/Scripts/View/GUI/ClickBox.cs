using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public static class ClickBox
    {
        private static Sprite CLICK_SPRITE = Resources.Load<Sprite>("Images/UI/click");

        public static GameObject RenderClickBox(GameObject parent)
        {
            var click = new GameObject("Click");
            var image = click.AddComponent<Image>();
            image.sprite = CLICK_SPRITE;
            image.preserveAspect = true;
            image.color = Color.white;
            click.layer = 5;
            click.AttachTo(parent);
            return click;
        }
    }
}
