using model.play;
using System;
using view.gui;

namespace view
{
    public class AbilityHighlight : IUsabilityObserver
    {
        public Highlight[] highlights;

        public AbilityHighlight(params Highlight[] highlights)
        {
            this.highlights = highlights;
        }

        void IUsabilityObserver.NotifyUsable(bool usable)
        {
            if (usable)
            {
                Array.ForEach(highlights, (highlight) => highlight.TurnOn());
            }
            else
            {
                Array.ForEach(highlights, (highlight) => highlight.TurnOff());
            }
        }
    }
}