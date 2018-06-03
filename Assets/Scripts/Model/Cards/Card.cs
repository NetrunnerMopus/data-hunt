namespace model.cards
{
    public abstract class Card
    {
        public abstract string Name { get; }
        public abstract IType Type { get; }
        public abstract ICost PlayCost { get; }
        public abstract IEffect Activation { get; }
        public abstract Faction Faction { get; }
        public abstract int InfluenceCost { get; }
        public abstract string FaceupArt { get; }
        public bool Faceup { get; set; }
    }
}