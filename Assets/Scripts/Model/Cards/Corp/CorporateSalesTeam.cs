﻿using model.cards.types;
using model.costs;

namespace model.cards.corp
{
    public class CorporateSalesTeam : Card
    {
        override public string FaceupArt => "corporate-sales-team";
        override public string Name => "Corporate Sales Team";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        public override ICost PlayCost { get { throw new System.Exception("Agendas don't have play costs"); } }
        public override IEffect Activation{ get { throw new System.Exception("Agendas don't have activations"); } }
        override public IType Type => new Agenda();
    }
}