using model.cards.types;

namespace model.cards.corp
{
    public class CorporateSalesTeam : Card
    {
        public CorporateSalesTeam(Game game) : base(game) { }
        override public string FaceupArt => "corporate-sales-team";
        override public string Name => "Corporate Sales Team";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost { get { throw new System.Exception("Agendas don't have play costs"); } }
        override public IEffect Activation { get { throw new System.Exception("Agendas don't have activations"); } }
        override public IType Type => new Agenda(printedAgendaPoints: 2);
    }
}
