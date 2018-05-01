using model.costs;
using model.effects.runner;
using model.cards;

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

        public Ability Play(ICard card)
        {
            return new Ability(new Conjunction(new RunnerClickCost(1), card.PlayCost), new Play(card));
        }

        public Ability Install(ICard card)
        {
            return new Ability(new Conjunction(new RunnerClickCost(1), card.PlayCost), new Install(card));
        }
    }
}