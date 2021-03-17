using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.steal;
using model.costs;

namespace model.cards.corp
{
    public class Bellona : Card
    {
        public Bellona(Game game) : base(game) { }
        override public string FaceupArt => "bellona";
        override public string Name => "Bellona";
        override public Faction Faction => Factions.NBN;
        override public int InfluenceCost => 9999999;
        override public ICost PlayCost { get { throw new System.Exception("Agendas don't have play costs"); } }
        override public IEffect Activation { get { throw new System.Exception("Agendas don't have activations"); } }
        override public IType Type => new Agenda(printedAgendaPoints: 3);
        override public IList<IStealOption> StealOptions(Game game) => new List<IStealOption> { new BellonaStealing(this, game), new DeclineSteal() };

        private class BellonaStealing : IStealOption
        {
            private ICost costToSteal;
            string IStealOption.Art => "Images/UI/credit";
            private Card card;

            public BellonaStealing(Card card, Game game)
            {
                this.card = card;
                costToSteal = game.Costs.Steal(card, 5);
            }

            bool IStealOption.IsLegal(Game game)
            {
                return costToSteal.Payable(game) && game.MustSteal(card, 3).IsLegal(game);
            }

            async Task<bool> IStealOption.Perform(Game game)
            {
                await costToSteal.Pay(game);
                IStealOption mustSteal = game.MustSteal(card, 3);
                return await mustSteal.Perform(game);
            }
        }
    }
}
