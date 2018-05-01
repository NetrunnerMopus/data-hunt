using model;

namespace view
{
    public interface IRunnerView
    {
        ActionCardView ActionCard { get; set; }
        void Display(Game game);
    }
}