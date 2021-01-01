using model.cards.types;
using model.costs;

namespace model.cards.corp
{
    public class CorporateSalesTeam : Card
    {
        override public string FaceupArt => "corporate-sales-team";
        override public string Name => "Corporate Sales Team";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost { get { throw new System.Exception("Agendas don't have play costs"); } }
        override public IEffect Activation { get { throw new System.Exception("Agendas don't have activations"); } }
        override public IType Type => new Agenda();
        public new Stealable Stealable => Stealable.CAN_STEAL;


    }
}