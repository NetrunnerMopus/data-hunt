using UnityEngine;
using UnityEngine.UI;
using model;

namespace view.gui.timecross
{
    public class DayNightCycle
    {
        private Color daylight = new Color(38, 195, 219, 255) / 255;
        private Color midnight = new Color(23, 17, 44, 255) / 255;

        public void Paint(Image background, Side side)
        {
            switch (side)
            {
                case Side.CORP: background.color = daylight; break;
                case Side.RUNNER: background.color = midnight; break;
            }
        }
    }
}
