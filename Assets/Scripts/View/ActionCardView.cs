using model.play;

namespace view
{
    public class ActionCardView
    {
        public readonly IUsabilityObserver draw;
        public readonly IUsabilityObserver credit;

        public ActionCardView(IUsabilityObserver draw, IUsabilityObserver credit)
        {
            this.draw = draw;
            this.credit = credit;
        }
    }
}