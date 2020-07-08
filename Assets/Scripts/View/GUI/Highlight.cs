using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class Highlight : MonoBehaviour
    {
        private Image Image { get { return GetComponent<Image>(); } }
        private Color Color { set { Image.color = value; } }
        private bool on = false;
        private Color highlight = Color.yellow * 0.7f + Color.green * 0.8f;
        private Color reset = Color.white + new Color(0f, 0f, 0f, 1f);

        public void TurnOn()
        {
            on = true;
        }

        void Update()
        {
            if (on)
            {
                var phase = Mathf.Sin(Time.time * 4);
                Color = Color.Lerp(highlight, reset, phase);
            }
        }

        public void TurnOff()
        {
            on = false;
            Color = reset;
        }
    }
}
