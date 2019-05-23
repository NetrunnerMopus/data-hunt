namespace model.zones.corp
{
    public interface IServer
    {
        Zone Zone { get; }
        IceColumn Ice { get; }
    }
}
