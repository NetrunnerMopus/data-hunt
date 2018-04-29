using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class Highlight : MonoBehaviour
    {
        private Image Image { get { return GetComponent<Image>(); } }
        private Color Color { set { Image.color = value; } }

        public void TurnOn()
        {
            var on = Color.cyan;
            Color = on;
        }

        public void TurnOff()
        {
            var off = Color.white;
            off.a = 1.0f;
            Color = off;
        }
    }
}