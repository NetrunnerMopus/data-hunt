using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.steal;
using model.costs;

namespace model.cards.corp
{
    public class Bellona : Card
    {
        override public string FaceupArt => "bellona";
        override public string Name => "Bellona";
        override public Faction Faction => Factions.NBN;
        override public int InfluenceCost => 9999999;
        override public ICost PlayCost { get { throw new System.Exception("Agendas don't have play costs"); } }
        override public IEffect Activation { get { throw new System.Exception("Agendas don't have activations"); } }
        override public IType Type => new Agenda(printedAgendaPoints: 3);
        override public IEnumerable<IStealOption> StealOptions(Game game)
        {
            return new IStealOption[] { new BellonaStealing(this), new DeclineSteal() };
        }

        private class BellonaStealing : IStealOption
        {
            private ICost costToSteal = new RunnerCreditCost(5);
            string IStealOption.Art => "Images/UI/credit";
            private Card card;

            public BellonaStealing(Card card)
            {
                this.card = card;
            }

            bool IStealOption.IsLegal(Game game)
            {
                return costToSteal.Payable(game);
            }

            async Task IStealOption.Perform(Game game)
            {
                await costToSteal.Pay(game);
                IStealOption mustSteal = new MustSteal(card, 3);
                await mustSteal.Perform(game);
            }
        }
    }
}
