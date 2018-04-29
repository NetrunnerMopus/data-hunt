using model.costs;
using model.effects.runner;
using view;

namespace model.play.runner
{
    public class ActionCard
    {
        public readonly Ability draw;
        public readonly Ability credit;

        public ActionCard()
        {
            draw = new Ability(new RunnerClickCost(1), new Draw(1));
            credit = new Ability(new RunnerClickCost(1), new Gain(1));
        }

        public void AttachView(ActionCardView view, Game game)
        {
            draw.Observe(view.draw, game);
            credit.Observe(view.credit, game);
        }
    }
}