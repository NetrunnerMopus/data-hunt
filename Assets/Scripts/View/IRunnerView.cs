using model;

namespace view
{
    public interface IRunnerView
    {
        void Display(Game game);
        IGripView Grip { get; }
    }
}