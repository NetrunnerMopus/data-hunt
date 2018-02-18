public class Faction
{
   public Side Side { get; private set; }

    public Faction(Side side)
    {
        Side = side;
    }
}