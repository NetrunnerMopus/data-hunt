using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class Highlight : MonoBehaviour
    {
        private Image Image { get { return GetComponent<Image>(); } }
        private Color Color { set { Image.color = value; } }
        public Color Flash = Color.yellow * 0.7f + Color.green * 0.8f;
        public Color Rest = Color.white + new Color(0f, 0f, 0f, 1f);

        void Update()
        {
            var phase = Mathf.Sin(Time.time * 4);
            Color = Color.Lerp(Flash, Rest, phase);
        }

        void OnDisable()
        {
            Color = Rest;
        }
    }
}
