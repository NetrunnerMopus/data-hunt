using model.cards;

namespace view.memory
{
    public class CompositeGripView : IGripView
    {
        private IGripView[] views;

        public CompositeGripView(params IGripView[] views)
        {
            this.views = views;
        }

        void IGripView.Add(ICard card)
        {
            foreach (var view in views)
            {
                view.Add(card);
            }
        }
    }
}