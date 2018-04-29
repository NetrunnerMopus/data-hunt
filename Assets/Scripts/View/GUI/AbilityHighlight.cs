using model.play;
using System;
using view.gui;

namespace view
{
    public class AbilityHighlight : IAvailabilityObserver<Ability>
    {
        public Highlight[] highlights;

        public AbilityHighlight(params Highlight[] highlights)
        {
            this.highlights = highlights;
        }

        void IAvailabilityObserver<Ability>.Notify(bool available, Ability ability)
        {
            if (available)
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