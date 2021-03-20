using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.steal;

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
        override public IType Type => new Agenda(printedAgendaPoints: 3, game: game);
        override public IList<IStealOption> StealOptions() => new List<IStealOption> { new BellonaStealing(this, game), new DeclineSteal() };

        private class BellonaStealing : IStealOption
        {
            private ICost costToSteal;
            string IStealOption.Art => "Images/UI/credit";
            private Card card;
            private IStealOption stealing;

            public BellonaStealing(Card card, Game game)
            {
                this.card = card;
                this.stealing = game.runner.Stealing.MustSteal(card, 3);
                costToSteal = game.runner.credits.Paying(5);
            }

            bool IStealOption.IsLegal()
            {
                return costToSteal.Payable && stealing.IsLegal();
            }

            async Task<bool> IStealOption.Perform()
            {
                await costToSteal.Pay();
                return await stealing.Perform();
            }
        }
    }
}
