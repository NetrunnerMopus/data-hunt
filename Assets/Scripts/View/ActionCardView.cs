using model.play;

namespace view
{
    public class ActionCardView
    {
        public readonly IUsabilityObserver draw;

        public ActionCardView(IUsabilityObserver draw)
        {
            this.draw = draw;
        }
    }
}