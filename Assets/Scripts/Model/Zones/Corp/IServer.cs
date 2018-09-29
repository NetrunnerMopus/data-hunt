namespace model.zones.corp
{
    public interface IServer
    {
        string Name { get; }
        IceColumn Ice { get; }
    }
}
