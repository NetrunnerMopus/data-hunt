using model;

namespace view
{
    public interface IRunnerView
    {
        void Display(Game game);
        IStackView Stack { get; }
        IRigView Rig { get; }
    }
}