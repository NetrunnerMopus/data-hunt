namespace model.cards
{
    public interface ICard
    {
        bool Faceup { get; }

        Faction Faction { get; }

        int InfluenceCost { get; }

        string FaceupArt { get; }

        ICost PlayCost { get; }

        IEffect PlayEffect { get; }

        string Name { get; }

        IType Type { get; }
    }
}