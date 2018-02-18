public interface ICard2
{
    bool Faceup { get; }
    Faction Faction { get; }
    string FaceupArt { get; }
    ICost PlayCost { get; }
    IEffect PlayEffect { get; }
    string Name { get; }
}