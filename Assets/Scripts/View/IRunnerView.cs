using model;

namespace view
{
    public interface IRunnerView
    {
        void Display(Game game);
        IRigView Rig { get; }
    }
}