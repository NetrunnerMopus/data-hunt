using model;

namespace view
{
    public interface IRunnerView
    {
        void Display(Game game);
        IGripView Grip { get; }
        IStackView Stack { get; }
        IHeapView Heap { get; }
        IRigView Rig { get; }
    }
}