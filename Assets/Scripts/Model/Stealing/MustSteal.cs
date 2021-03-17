using System.Threading.Tasks;
using model.cards;

namespace model.stealing
{
    public class MustSteal : IStealOption
    {
        private Card card;
        private int agendaPoints;
        string IStealOption.Art => "Images/UI/hand-click";

        public MustSteal(Card card, int agendaPoints)
        {
            this.card = card;
            this.agendaPoints = agendaPoints;
        }

        bool IStealOption.IsLegal(Game game) => true;
        Task<bool> IStealOption.Perform(Game game)
        {
            game.runner.zones.score.Add(card, agendaPoints); // CR: 1.16.3
            return Task.FromResult(true);
        }
    }
}
