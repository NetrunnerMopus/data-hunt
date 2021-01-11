using System.Threading.Tasks;
using model.cards;

namespace model.choices.steal
{
    public class MustSteal : IStealOption
    {
        private Card card;
        private int agendaPoints;

        public MustSteal(Card card, int agendaPoints)
        {
            this.card = card;
            this.agendaPoints = agendaPoints;
        }

        Task IStealOption.Perform(Game game)
        {
            game.runner.zones.score.Add(card, agendaPoints); // CR: 1.16.3
            return Task.CompletedTask;
        }
    }
}
