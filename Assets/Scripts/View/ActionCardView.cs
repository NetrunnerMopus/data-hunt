using model.play;

namespace view
{
    public class ActionCardView
    {
        public readonly IAvailabilityObserver<Ability> draw;
        public readonly IAvailabilityObserver<Ability> credit;

        public ActionCardView(IAvailabilityObserver<Ability> draw, IAvailabilityObserver<Ability> credit)
        {
            this.draw = draw;
            this.credit = credit;
        }
    }
}