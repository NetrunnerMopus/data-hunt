using model.cards.types;

namespace model.cards.corp
{
    public class HedgeFund : Card
    {
        public HedgeFund(Game game) : base(game)
        {
        }

        override public string FaceupArt => "hedge-fund";
        override public string Name => "Hedge Fund";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => game.corp.credits.PayingForPlaying(this, 5);
        override public IEffect Activation => game.corp.credits.Gaining(9);
        override public IType Type => new Operation();
    }
}
