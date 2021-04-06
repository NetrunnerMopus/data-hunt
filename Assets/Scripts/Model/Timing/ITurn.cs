namespace model.timing
{
    public interface ITurn : ITimingStructure<ITurn>
    {
        ClickPool Clicks { get; }
        Side Side { get; }
    }
}
